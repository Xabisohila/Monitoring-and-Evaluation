-- ============================================================
-- n_sp_PMTDP_GetUploadData  (run against MnE_Copy_2)
--
-- Returns all staging rows for a given UploadRequestID.
-- Internal columns (UploadDataID, ProposedAction) are included
-- so the C# approval handler can reference them; the UI layer
-- strips them before display.
-- ============================================================
USE MnE_Copy_2;
GO

CREATE OR ALTER PROCEDURE dbo.n_sp_PMTDP_GetUploadData
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
        IsCumulative,
        IsPercentage,
        InterventionName,
        InterventionIndicator,
        Baseline2023_24,
        TermTarget2030,
        TermBudget,
        SpatialReference,
        ProposedAction
    FROM dbo.i_PMTDP_UploadData
    WHERE UploadRequestID = @UploadRequestID
    ORDER BY UploadDataID;
END
GO
