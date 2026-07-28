-- ============================================================
-- Workflow Status SP
-- Returns one row with completion counts for all 8 planning stages.
-- Uses OBJECT_ID guards so it runs safely even if some tables
-- have not been created yet.
-- Run once against MnE_Copy_2
-- ============================================================
USE MnE_Copy_2;
GO

CREATE OR ALTER PROCEDURE dbo.sp_GetWorkflowStatus
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @UploadCount        INT = 0,
        @TotalPriorities    INT = 0,
        @AssignedPriorities INT = 0,
        @POACount           INT = 0,
        @InterventionCount  INT = 0,
        @IndicatorCount     INT = 0,
        @QTargetCount       INT = 0,
        @OwnerCount         INT = 0;

    -- Stage 1: any rows in upload staging = a file was uploaded
    IF OBJECT_ID('dbo.i_PMTDP_UploadData', 'U') IS NOT NULL
        SET @UploadCount = (SELECT COUNT(DISTINCT UploadRequestID) FROM dbo.i_PMTDP_UploadData);

    -- Stage 2 (Approved) is inferred in C# by comparing UploadCount vs TotalPriorities.
    -- If priorities exist the apply-approved step ran = upload was approved.

    -- Stage 3: cluster assignment
    IF OBJECT_ID('dbo.new_PMTDP_Priorities', 'U') IS NOT NULL
    BEGIN
        SET @TotalPriorities    = (SELECT COUNT(*) FROM dbo.new_PMTDP_Priorities);
        SET @AssignedPriorities = (SELECT COUNT(*) FROM dbo.new_PMTDP_Priorities
                                    WHERE ISNULL(ClusterID, 0) > 0);
    END

    -- Stage 4: POAs
    IF OBJECT_ID('dbo.new_ProgrammesOfAction', 'U') IS NOT NULL
        SET @POACount = (SELECT COUNT(*) FROM dbo.new_ProgrammesOfAction);

    -- Stage 5: Interventions
    IF OBJECT_ID('dbo.new_Interventions', 'U') IS NOT NULL
        SET @InterventionCount = (SELECT COUNT(*) FROM dbo.new_Interventions);

    -- Stage 6: Indicators
    IF OBJECT_ID('dbo.new_Intervention_Indicators', 'U') IS NOT NULL
        SET @IndicatorCount = (SELECT COUNT(*) FROM dbo.new_Intervention_Indicators);

    -- Stage 7: Quarterly targets (via annual targets join)
    IF OBJECT_ID('dbo.i_QuarterlyTargets', 'U') IS NOT NULL
        SET @QTargetCount = (SELECT COUNT(*) FROM dbo.i_QuarterlyTargets);

    -- Stage 8: Indicator owners
    IF OBJECT_ID('dbo.i_IndicatorOwners', 'U') IS NOT NULL
        SET @OwnerCount = (SELECT COUNT(*) FROM dbo.i_IndicatorOwners);

    SELECT
        @UploadCount        AS UploadCount,
        @TotalPriorities    AS TotalPriorities,
        @AssignedPriorities AS AssignedPriorities,
        @POACount           AS POACount,
        @InterventionCount  AS InterventionCount,
        @IndicatorCount     AS IndicatorCount,
        @QTargetCount       AS QTargetCount,
        @OwnerCount         AS OwnerCount;
END
GO
