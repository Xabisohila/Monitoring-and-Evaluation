-- ============================================================
-- Creates / replaces n_sp_PMTDP_GetMyUploads
-- Uses the same column-based table detection as
-- n_sp_PMTDP_GetUploadHeader.
--
-- Run once against MnE_Copy_2
-- ============================================================
USE MnE_Copy_2;
GO

CREATE OR ALTER PROCEDURE dbo.n_sp_PMTDP_GetMyUploads
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @tbl NVARCHAR(256) = NULL;

    SELECT TOP 1
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
    ORDER BY t.name;

    IF @tbl IS NULL RETURN;

    DECLARE @sql NVARCHAR(MAX) =
        N'SELECT * FROM ' + @tbl
      + N' WHERE UploadedByUserID = @uid'
      + N' ORDER BY UploadRequestID DESC;';

    EXEC sp_executesql @sql, N'@uid INT', @uid = @UserID;
END
GO
