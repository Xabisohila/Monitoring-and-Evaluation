-- ============================================================
-- Creates / replaces n_sp_PMTDP_GetUploadHeader
--
-- Finds the upload-request table automatically by looking for
-- a table that has BOTH UploadRequestID and UploadedByUserID
-- columns — no guessing of table names required.
-- Also handles the status column being called either Status
-- or Decision (both naming conventions exist in this codebase).
--
-- Run once against MnE_Copy_2
-- ============================================================
USE MnE_Copy_2;
GO

CREATE OR ALTER PROCEDURE dbo.n_sp_PMTDP_GetUploadHeader
    @UploadRequestID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- ── 1. Locate the upload-request table by its key columns ─
    DECLARE @tbl NVARCHAR(256) = NULL;
    DECLARE @oid INT            = NULL;

    SELECT TOP 1
        @oid = t.object_id,
        @tbl = QUOTENAME(s.name) + N'.' + QUOTENAME(t.name)
    FROM sys.tables  t
    JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE EXISTS (
        SELECT 1 FROM sys.columns c
        WHERE c.object_id = t.object_id AND c.name = 'UploadRequestID'
    )
    AND EXISTS (
        SELECT 1 FROM sys.columns c
        WHERE c.object_id = t.object_id AND c.name = 'UploadedByUserID'
    )
    ORDER BY t.name;    -- deterministic if >1 match

    IF @tbl IS NULL
    BEGIN
        -- Table not found — return no rows so C# GetUploadHeader returns null
        -- instead of throwing a "column not found" error.
        SELECT CAST(NULL AS INT)           AS UploadedByUserID,
               CAST(NULL AS NVARCHAR(50))  AS Status
        WHERE  1 = 0;
        RETURN;
    END

    -- ── 2. Decide which column holds the status ────────────────
    DECLARE @statusExpr NVARCHAR(256);

    IF EXISTS (
        SELECT 1 FROM sys.columns c
        WHERE  c.object_id = @oid AND c.name = 'Status'
    )
        SET @statusExpr = N'ISNULL([Status],   ''Pending'') AS Status';
    ELSE IF EXISTS (
        SELECT 1 FROM sys.columns c
        WHERE  c.object_id = @oid AND c.name = 'Decision'
    )
        SET @statusExpr = N'ISNULL([Decision], ''Pending'') AS Status';
    ELSE
        SET @statusExpr = N'''Pending'' AS Status';

    -- ── 3. Fetch the row ───────────────────────────────────────
    DECLARE @sql NVARCHAR(MAX) =
        N'SELECT UploadedByUserID, ' + @statusExpr
      + N' FROM '  + @tbl
      + N' WHERE UploadRequestID = @id;';

    EXEC sp_executesql @sql, N'@id INT', @id = @UploadRequestID;
END
GO

-- ── Quick sanity check — should return the table name found ───
SELECT TOP 1
    QUOTENAME(s.name) + '.' + QUOTENAME(t.name) AS UploadRequestTable
FROM sys.tables  t
JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE EXISTS (SELECT 1 FROM sys.columns c WHERE c.object_id = t.object_id AND c.name = 'UploadRequestID')
AND   EXISTS (SELECT 1 FROM sys.columns c WHERE c.object_id = t.object_id AND c.name = 'UploadedByUserID')
ORDER BY t.name;
GO
