-- =============================================================================
-- PHASE 1 — PMTDP UPLOAD EXTENSION
-- Run against: MnE_Copy_2
-- Purpose:
--   1. Extend staging table to hold the full PMTDP row (Intervention-level fields)
--   2. Extend new_Intervention_Indicators with missing columns
--   3. Replace n_sp_PMTDP_InsertUploadData to accept all new columns
--   4. Create n_sp_PMTDP_ApplyApprovedRow — bridges approved staging data into
--      both the i_* monitoring tables AND the new_PMTDP_Priorities reference table
--   5. Update new_SP_CreateInterventionIndicator to accept TermTarget, flags
-- =============================================================================

USE MnE_Copy_2;
GO

-- =============================================================================
-- STEP 1: ALTER TABLES
-- =============================================================================

-- 1a. Extend staging table i_PMTDP_UploadData
--     ImplementingInstitution and SupportingInstitutions already exist.
--     Add the Intervention-level columns that were missing.
-- -----------------------------------------------------------------------------
ALTER TABLE dbo.i_PMTDP_UploadData ADD
    InterventionName      NVARCHAR(500) NULL,
    InterventionIndicator NVARCHAR(500) NULL,
    Baseline2023_24       NVARCHAR(100) NULL,
    TermTarget2030        NVARCHAR(100) NULL,
    TermBudget            NVARCHAR(100) NULL,
    AnnualTarget          NVARCHAR(100) NULL,
    SpatialReference      NVARCHAR(255) NULL;
GO

-- 1b. Extend new_Intervention_Indicators
--     BaselineValue (decimal) and BaselineYear already exist.
--     Add TermTargetValue (text, not decimal — PMTDP uses "To be confirmed later")
--     and the two boolean flags needed for report calculation.
-- -----------------------------------------------------------------------------
ALTER TABLE dbo.new_Intervention_Indicators ADD
    TermTargetValue NVARCHAR(255) NULL,
    IsCumulative    BIT NOT NULL DEFAULT 0,
    IsPercentage    BIT NOT NULL DEFAULT 0;
GO


-- =============================================================================
-- STEP 2: ALTER n_sp_PMTDP_InsertUploadData
--         Accepts the full PMTDP row — all staging columns including the
--         previously-commented ImplementingInstitution/SupportingInstitutions
--         and the new Intervention-level fields.
-- =============================================================================
ALTER PROCEDURE dbo.n_sp_PMTDP_InsertUploadData
    @UploadRequestID        INT,
    -- Outcome-level fields (existing)
    @PriorityName           NVARCHAR(255),
    @ProgrammeName          NVARCHAR(255),
    @LeaderDeptName         NVARCHAR(255)  = NULL,
    @OutcomeName            NVARCHAR(500),
    @IndicatorName          NVARCHAR(500),
    @IndicatorType          NVARCHAR(100)  = NULL,
    @BaselineValue          NVARCHAR(100)  = NULL,
    @TermTargetValue        NVARCHAR(100)  = NULL,
    @AnnualBudget           DECIMAL(18,2)  = NULL,
    -- Institution fields (were commented out — now active)
    @ImplementingInstitution  NVARCHAR(500) = NULL,
    @SupportingInstitutions   NVARCHAR(500) = NULL,
    @IsCumulative           BIT            = 0,
    @IsPercentage           BIT            = 0,
    -- Intervention-level fields (new)
    @InterventionName       NVARCHAR(500)  = NULL,
    @InterventionIndicator  NVARCHAR(500)  = NULL,
    @Baseline2023_24        NVARCHAR(100)  = NULL,
    @TermTarget2030         NVARCHAR(100)  = NULL,
    @TermBudget             NVARCHAR(100)  = NULL,
    @AnnualTarget           NVARCHAR(100)  = NULL,
    @SpatialReference       NVARCHAR(255)  = NULL,
    @ProposedAction         NVARCHAR(10)   = 'Insert'
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.i_PMTDP_UploadData (
        UploadRequestID,
        PriorityName,
        ProgrammeName,
        LeaderDeptName,
        OutcomeName,
        IndicatorName,
        IndicatorType,
        BaselineValue,
        TermTargetValue,
        AnnualBudget,
        ImplementingInstitution,
        SupportingInstitutions,
        IsCumulative,
        IsPercentage,
        InterventionName,
        InterventionIndicator,
        Baseline2023_24,
        TermTarget2030,
        TermBudget,
        AnnualTarget,
        SpatialReference,
        ProposedAction
    )
    VALUES (
        @UploadRequestID,
        @PriorityName,
        @ProgrammeName,
        @LeaderDeptName,
        @OutcomeName,
        @IndicatorName,
        @IndicatorType,
        @BaselineValue,
        @TermTargetValue,
        @AnnualBudget,
        @ImplementingInstitution,
        @SupportingInstitutions,
        @IsCumulative,
        @IsPercentage,
        @InterventionName,
        @InterventionIndicator,
        @Baseline2023_24,
        @TermTarget2030,
        @TermBudget,
        @AnnualTarget,
        @SpatialReference,
        @ProposedAction
    );
END
GO


-- =============================================================================
-- STEP 3: CREATE n_sp_PMTDP_ApplyApprovedRow
--
--         Called once per staging row when an upload is approved.
--         Takes a single UploadDataID, reads the staged row, then:
--
--         A. Upserts into i_* monitoring tables (Priority → Programme →
--            Outcome → Indicator) — preserving all existing behaviour.
--
--         B. Upserts into new_PMTDP_Priorities reference table so the POA
--            builder has the correct priority to link against. ClusterID is
--            left NULL here — the PU assigns it via the admin pages before
--            creating a POA.
--
--         Returns a single row with the IDs created/found at each level so
--         the C# ApplyRow() method can log or extend further if needed.
-- =============================================================================
CREATE OR ALTER PROCEDURE dbo.n_sp_PMTDP_ApplyApprovedRow
    @UploadDataID   INT,
    @ApprovedByUserID INT = NULL    -- for future audit trail use
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- -------------------------------------------------------------------------
    -- Read the staged row
    -- -------------------------------------------------------------------------
    DECLARE
        @PriorityName           NVARCHAR(255),
        @ProgrammeName          NVARCHAR(255),
        @LeaderDeptName         NVARCHAR(255),
        @OutcomeName            NVARCHAR(500),
        @IndicatorName          NVARCHAR(500),
        @IndicatorType          NVARCHAR(100),
        @BaselineValue          NVARCHAR(100),
        @TermTargetValue        NVARCHAR(100),
        @AnnualBudget           DECIMAL(18,2),
        @ImplementingInstitution NVARCHAR(500),
        @SupportingInstitutions  NVARCHAR(500),
        @IsCumulative           BIT,
        @IsPercentage           BIT,
        @InterventionName       NVARCHAR(500),
        @InterventionIndicator  NVARCHAR(500),
        @Baseline2023_24        NVARCHAR(100),
        @TermTarget2030         NVARCHAR(100),
        @TermBudget             NVARCHAR(100),
        @AnnualTarget           NVARCHAR(100),
        @SpatialReference       NVARCHAR(255);

    SELECT
        @PriorityName            = PriorityName,
        @ProgrammeName           = ProgrammeName,
        @LeaderDeptName          = LeaderDeptName,
        @OutcomeName             = OutcomeName,
        @IndicatorName           = IndicatorName,
        @IndicatorType           = IndicatorType,
        @BaselineValue           = BaselineValue,
        @TermTargetValue         = TermTargetValue,
        @AnnualBudget            = AnnualBudget,
        @ImplementingInstitution = ImplementingInstitution,
        @SupportingInstitutions  = SupportingInstitutions,
        @IsCumulative            = IsCumulative,
        @IsPercentage            = IsPercentage,
        @InterventionName        = InterventionName,
        @InterventionIndicator   = InterventionIndicator,
        @Baseline2023_24         = Baseline2023_24,
        @TermTarget2030          = TermTarget2030,
        @TermBudget              = TermBudget,
        @AnnualTarget            = AnnualTarget,
        @SpatialReference        = SpatialReference
    FROM dbo.i_PMTDP_UploadData
    WHERE UploadDataID = @UploadDataID;

    IF @PriorityName IS NULL OR @OutcomeName IS NULL OR @IndicatorName IS NULL
    BEGIN
        -- Row not found or missing required fields — skip silently
        SELECT
            NULL AS i_PriorityID,
            NULL AS i_ProgrammeID,
            NULL AS i_OutcomeID,
            NULL AS i_IndicatorID,
            NULL AS new_PMTDP_PriorityID,
            'Skipped — missing required fields' AS ApplyStatus;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE
            @i_PriorityID       INT,
            @i_ProgrammeID      INT,
            @i_OutcomeID        INT,
            @i_IndicatorID      INT,
            @new_PriorityID     INT;

        -- =====================================================================
        -- PART A: i_* monitoring tables (existing behaviour, preserved exactly)
        -- =====================================================================

        -- A1. Priority
        SELECT @i_PriorityID = PriorityID
        FROM dbo.i_Priorities
        WHERE PriorityName = @PriorityName;

        IF @i_PriorityID IS NULL
        BEGIN
            INSERT INTO dbo.i_Priorities (PriorityName, Description)
            VALUES (@PriorityName, NULL);
            SET @i_PriorityID = SCOPE_IDENTITY();
        END

        -- A2. Integration Programme
        SELECT @i_ProgrammeID = ProgrammeID
        FROM dbo.i_IntegrationProgrammes
        WHERE ProgrammeName = @ProgrammeName;

        IF @i_ProgrammeID IS NULL
        BEGIN
            INSERT INTO dbo.i_IntegrationProgrammes (ProgrammeName, LeaderDeptID)
            VALUES (@ProgrammeName, NULL);
            SET @i_ProgrammeID = SCOPE_IDENTITY();
        END

        -- A3. Outcome
        SELECT @i_OutcomeID = OutcomeID
        FROM dbo.i_Outcomes
        WHERE OutcomeName = @OutcomeName
          AND PriorityID  = @i_PriorityID
          AND ProgrammeID = @i_ProgrammeID;

        IF @i_OutcomeID IS NULL
        BEGIN
            INSERT INTO dbo.i_Outcomes (OutcomeName, PriorityID, ProgrammeID)
            VALUES (@OutcomeName, @i_PriorityID, @i_ProgrammeID);
            SET @i_OutcomeID = SCOPE_IDENTITY();
        END

        -- A4. Outcome Indicator (upsert)
        SELECT @i_IndicatorID = IndicatorID
        FROM dbo.i_Indicators
        WHERE IndicatorName = @IndicatorName
          AND OutcomeID     = @i_OutcomeID;

        IF @i_IndicatorID IS NULL
        BEGIN
            INSERT INTO dbo.i_Indicators (
                IndicatorName, IndicatorType, OutcomeID,
                BaselineValue, TermTargetValue, AnnualBudget,
                ImplementingInstitution, SupportingInstitutions,
                IsCumulative, IsPercentage
            )
            VALUES (
                @IndicatorName, @IndicatorType, @i_OutcomeID,
                @BaselineValue, @TermTargetValue, @AnnualBudget,
                @ImplementingInstitution, @SupportingInstitutions,
                @IsCumulative, @IsPercentage
            );
            SET @i_IndicatorID = SCOPE_IDENTITY();
        END
        ELSE
        BEGIN
            -- Update existing indicator if re-uploaded
            UPDATE dbo.i_Indicators SET
                IndicatorType            = @IndicatorType,
                BaselineValue            = @BaselineValue,
                TermTargetValue          = @TermTargetValue,
                AnnualBudget             = ISNULL(@AnnualBudget, AnnualBudget),
                ImplementingInstitution  = @ImplementingInstitution,
                SupportingInstitutions   = @SupportingInstitutions,
                IsCumulative             = @IsCumulative,
                IsPercentage             = @IsPercentage
            WHERE IndicatorID = @i_IndicatorID;
        END

        -- =====================================================================
        -- PART B: new_PMTDP_Priorities reference table
        --
        -- Upsert the Priority so the POA builder has it available.
        -- ClusterID left NULL — PU assigns this via the cluster admin page
        -- before creating a POA. PDP_ID defaults to 1 (the active PDP).
        -- Update this default if your new_ProvincialDevelopmentPlans table
        -- has a different active PDP_ID.
        -- =====================================================================
        DECLARE @activePDP_ID INT;
        SELECT TOP 1 @activePDP_ID = PDP_ID
        FROM dbo.new_ProvincialDevelopmentPlans
        ORDER BY PDP_StartYear DESC;   -- use most recent PDP

        IF @activePDP_ID IS NULL
            SET @activePDP_ID = 1;     -- fallback if no PDP row exists yet

        SELECT @new_PriorityID = PMTDP_PriorityID
        FROM dbo.new_PMTDP_Priorities
        WHERE PriorityName = @PriorityName
          AND PDP_ID       = @activePDP_ID;

        IF @new_PriorityID IS NULL
        BEGIN
            INSERT INTO dbo.new_PMTDP_Priorities
                (PDP_ID, PriorityName, PriorityDescription, DesiredOutcome, ClusterID)
            VALUES
                (@activePDP_ID, @PriorityName, NULL, @OutcomeName, NULL);
            SET @new_PriorityID = SCOPE_IDENTITY();
        END
        -- (No update on existing — PU may have already assigned a ClusterID)

        COMMIT TRANSACTION;

        -- Return what was created/found
        SELECT
            @i_PriorityID   AS i_PriorityID,
            @i_ProgrammeID  AS i_ProgrammeID,
            @i_OutcomeID    AS i_OutcomeID,
            @i_IndicatorID  AS i_IndicatorID,
            @new_PriorityID AS new_PMTDP_PriorityID,
            'Applied'       AS ApplyStatus;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO


-- =============================================================================
-- STEP 4: Update n_sp_PMTDP_GetUploadData
--         Include the new columns in the result set returned to the approval
--         preview grid so the reviewer can see the full row before approving.
-- =============================================================================
ALTER PROCEDURE dbo.n_sp_PMTDP_GetUploadData
    @UploadRequestID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        UploadDataID,
        PriorityName,
        ProgrammeName,
        LeaderDeptName,
        OutcomeName,
        IndicatorName,
        IndicatorType,
        BaselineValue,
        TermTargetValue,
        ImplementingInstitution,
        SupportingInstitutions,
        IsCumulative,
        IsPercentage,
        -- Intervention-level columns (new)
        InterventionName,
        InterventionIndicator,
        Baseline2023_24,
        TermTarget2030,
        TermBudget,
        AnnualTarget,
        SpatialReference,
        ProposedAction
    FROM dbo.i_PMTDP_UploadData
    WHERE UploadRequestID = @UploadRequestID
    ORDER BY UploadDataID;
END
GO


-- =============================================================================
-- STEP 5: Update new_SP_CreateInterventionIndicator
--         Add TermTargetValue, IsCumulative, IsPercentage so that when PU
--         manually creates an Intervention Indicator via the UI, these fields
--         are stored correctly. Also inserts into new_Indicator_Targets.
-- =============================================================================
ALTER PROCEDURE dbo.new_SP_CreateInterventionIndicator
    @InterventionID     INT,
    @IndicatorName      VARCHAR(255),
    @IndicatorType      VARCHAR(50),
    @UnitOfMeasure      VARCHAR(50)     = NULL,
    @BaselineValue      DECIMAL(18,2)   = 0,
    @BaselineYear       INT,
    @TermTargetValue    NVARCHAR(255)   = NULL,   -- NEW: e.g. "15%" or "To be confirmed"
    @IsCumulative       BIT             = 0,       -- NEW
    @IsPercentage       BIT             = 0,       -- NEW
    @TargetValue        DECIMAL(18,2)   = NULL,
    @TargetYear         INT             = NULL,
    @Target2030_TermTarget TEXT         = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @NewIndicatorID INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO dbo.new_Intervention_Indicators (
            InterventionID,
            IndicatorName,
            IndicatorType,
            UnitOfMeasure,
            BaselineValue,
            BaselineYear,
            SubOutcomeID,
            TermTargetValue,   -- new column
            IsCumulative,      -- new column
            IsPercentage       -- new column
        )
        VALUES (
            @InterventionID,
            @IndicatorName,
            @IndicatorType,
            @UnitOfMeasure,
            @BaselineValue,
            @BaselineYear,
            (SELECT SubOutcomeID FROM dbo.new_Interventions WHERE InterventionID = @InterventionID),
            @TermTargetValue,
            @IsCumulative,
            @IsPercentage
        );

        SET @NewIndicatorID = SCOPE_IDENTITY();

        -- Insert annual target + 2030 term target if provided
        IF @TargetValue IS NOT NULL AND @TargetYear IS NOT NULL
        BEGIN
            INSERT INTO dbo.new_Indicator_Targets
                (IndicatorID, TargetYear, TargetValue, Target2030_TermTarget)
            VALUES
                (@NewIndicatorID, @TargetYear, @TargetValue, @Target2030_TermTarget);
        END;

        COMMIT TRANSACTION;

        SELECT @NewIndicatorID AS NewIndicatorID;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO


-- =============================================================================
-- VERIFICATION — run these SELECTs to confirm the changes applied cleanly
-- =============================================================================

-- Check new staging columns exist
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'i_PMTDP_UploadData'
ORDER BY ORDINAL_POSITION;

-- Check new_Intervention_Indicators new columns
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'new_Intervention_Indicators'
ORDER BY ORDINAL_POSITION;

-- Confirm all 5 SPs exist
SELECT ROUTINE_NAME, ROUTINE_TYPE, LAST_ALTERED
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_NAME IN (
    'n_sp_PMTDP_InsertUploadData',
    'n_sp_PMTDP_ApplyApprovedRow',
    'n_sp_PMTDP_GetUploadData',
    'new_SP_CreateInterventionIndicator',
    'new_SP_CreateIntervention'
)
ORDER BY ROUTINE_NAME;
GO
