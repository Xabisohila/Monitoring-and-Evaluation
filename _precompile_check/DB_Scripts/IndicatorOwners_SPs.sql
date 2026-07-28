-- ============================================================
-- Indicator Owners: table + all stored procedures
-- Owner = Leading Department (Institution), NOT a system user
-- Run this once against MnE_Copy_2
-- ============================================================

-- Table: create fresh, or migrate existing UserID -> InstitutionID
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'i_IndicatorOwners'
)
BEGIN
    CREATE TABLE i_IndicatorOwners (
        OwnerID       INT IDENTITY(1,1) PRIMARY KEY,
        IndicatorID   INT NOT NULL,
        InstitutionID INT NOT NULL,
        CONSTRAINT UQ_IndicatorOwner UNIQUE (IndicatorID, InstitutionID)
    );
END
ELSE
BEGIN
    -- Drop old unique constraint if it references UserID
    IF EXISTS (SELECT 1 FROM sys.key_constraints WHERE name = 'UQ_IndicatorOwner')
        ALTER TABLE i_IndicatorOwners DROP CONSTRAINT UQ_IndicatorOwner;

    -- Swap UserID -> InstitutionID if the old column still exists
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
               WHERE TABLE_NAME = 'i_IndicatorOwners' AND COLUMN_NAME = 'UserID')
    BEGIN
        -- Clear any existing data tied to old UserID scheme
        DELETE FROM i_IndicatorOwners;

        -- Drop any foreign key constraints referencing UserID
        DECLARE @fkName NVARCHAR(200);
        SELECT @fkName = fk.name
        FROM sys.foreign_keys fk
        JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
        JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
        JOIN sys.tables t ON c.object_id = t.object_id
        WHERE t.name = 'i_IndicatorOwners' AND c.name = 'UserID';
        IF @fkName IS NOT NULL
            EXEC('ALTER TABLE i_IndicatorOwners DROP CONSTRAINT ' + @fkName);

        ALTER TABLE i_IndicatorOwners DROP COLUMN UserID;

        ALTER TABLE i_IndicatorOwners ADD InstitutionID INT NOT NULL DEFAULT 0;
        -- Remove the default constraint immediately (it was only needed for the ALTER)
        DECLARE @dfName NVARCHAR(200);
        SELECT @dfName = dc.name
        FROM sys.default_constraints dc
        JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
        JOIN sys.tables t ON c.object_id = t.object_id
        WHERE t.name = 'i_IndicatorOwners' AND c.name = 'InstitutionID';
        IF @dfName IS NOT NULL
            EXEC('ALTER TABLE i_IndicatorOwners DROP CONSTRAINT ' + @dfName);
    END

    -- Re-add unique constraint on new column pair (idempotent)
    IF NOT EXISTS (SELECT 1 FROM sys.key_constraints WHERE name = 'UQ_IndicatorOwner')
        ALTER TABLE i_IndicatorOwners
            ADD CONSTRAINT UQ_IndicatorOwner UNIQUE (IndicatorID, InstitutionID);
END
GO

-- ============================================================
-- sp_IndicatorOwner_Upsert
-- @OwnerID NULL  = insert (skips silently if pair already exists)
-- @OwnerID value = update that row
-- Returns OwnerID in both cases
-- ============================================================
IF OBJECT_ID('sp_IndicatorOwner_Upsert', 'P') IS NOT NULL
    DROP PROCEDURE sp_IndicatorOwner_Upsert;
GO
CREATE PROCEDURE sp_IndicatorOwner_Upsert
    @OwnerID       INT = NULL,
    @IndicatorID   INT,
    @InstitutionID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF @OwnerID IS NULL
    BEGIN
        IF EXISTS (
            SELECT 1 FROM i_IndicatorOwners
            WHERE IndicatorID = @IndicatorID AND InstitutionID = @InstitutionID
        )
        BEGIN
            -- Already assigned — return existing OwnerID
            SELECT OwnerID FROM i_IndicatorOwners
            WHERE IndicatorID = @IndicatorID AND InstitutionID = @InstitutionID;
        END
        ELSE
        BEGIN
            INSERT INTO i_IndicatorOwners (IndicatorID, InstitutionID)
            VALUES (@IndicatorID, @InstitutionID);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
        END
    END
    ELSE
    BEGIN
        UPDATE i_IndicatorOwners
        SET IndicatorID = @IndicatorID, InstitutionID = @InstitutionID
        WHERE OwnerID = @OwnerID;
        SELECT @OwnerID;
    END
END
GO

-- ============================================================
-- sp_IndicatorOwner_GetByID
-- ============================================================
IF OBJECT_ID('sp_IndicatorOwner_GetByID', 'P') IS NOT NULL
    DROP PROCEDURE sp_IndicatorOwner_GetByID;
GO
CREATE PROCEDURE sp_IndicatorOwner_GetByID
    @OwnerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT OwnerID, IndicatorID, InstitutionID
    FROM i_IndicatorOwners
    WHERE OwnerID = @OwnerID;
END
GO

-- ============================================================
-- sp_IndicatorOwner_List
-- ============================================================
IF OBJECT_ID('sp_IndicatorOwner_List', 'P') IS NOT NULL
    DROP PROCEDURE sp_IndicatorOwner_List;
GO
CREATE PROCEDURE sp_IndicatorOwner_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT OwnerID, IndicatorID, InstitutionID
    FROM i_IndicatorOwners
    ORDER BY IndicatorID, InstitutionID;
END
GO

-- ============================================================
-- ii_sp_IndicatorOwner_ListByIndicator
-- ============================================================
IF OBJECT_ID('ii_sp_IndicatorOwner_ListByIndicator', 'P') IS NOT NULL
    DROP PROCEDURE ii_sp_IndicatorOwner_ListByIndicator;
GO
CREATE PROCEDURE ii_sp_IndicatorOwner_ListByIndicator
    @IndicatorID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT OwnerID, IndicatorID, InstitutionID
    FROM i_IndicatorOwners
    WHERE IndicatorID = @IndicatorID
    ORDER BY InstitutionID;
END
GO

-- ============================================================
-- sp_IndicatorOwner_ListByInstitution
-- ============================================================
IF OBJECT_ID('sp_IndicatorOwner_ListByInstitution', 'P') IS NOT NULL
    DROP PROCEDURE sp_IndicatorOwner_ListByInstitution;
GO
CREATE PROCEDURE sp_IndicatorOwner_ListByInstitution
    @InstitutionID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT OwnerID, IndicatorID, InstitutionID
    FROM i_IndicatorOwners
    WHERE InstitutionID = @InstitutionID
    ORDER BY IndicatorID;
END
GO

-- ============================================================
-- sp_IndicatorOwner_Delete
-- Returns the deleted OwnerID
-- ============================================================
IF OBJECT_ID('sp_IndicatorOwner_Delete', 'P') IS NOT NULL
    DROP PROCEDURE sp_IndicatorOwner_Delete;
GO
CREATE PROCEDURE sp_IndicatorOwner_Delete
    @OwnerID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM i_IndicatorOwners WHERE OwnerID = @OwnerID;
    SELECT @OwnerID;
END
GO
