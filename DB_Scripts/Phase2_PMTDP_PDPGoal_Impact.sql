-- =============================================================================
-- PHASE 2 — PMTDP UPLOAD EXTENSION: PDPGoal + ImpactStatement
-- Run against: MnE_Copy_2
-- Purpose:
--   1. Add PDPGoal and ImpactStatement columns to i_PMTDP_UploadData
--   2. Update n_sp_PMTDP_InsertUploadData to accept and store both new fields
--   3. Update n_sp_PMTDP_GetUploadData to return them in the approval preview
-- =============================================================================

USE MnE_Copy_2;
GO

-- =============================================================================
-- STEP 1: Add new columns to staging table
-- =============================================================================
ALTER TABLE dbo.i_PMTDP_UploadData ADD
    PDPGoal        NVARCHAR(500) NULL,
    ImpactStatement NVARCHAR(500) NULL;
GO

-- =============================================================================
-- STEP 2: Update n_sp_PMTDP_InsertUploadData
-- =============================================================================
ALTER PROCEDURE dbo.n_sp_PMTDP_InsertUploadData
    @UploadRequestID         INT,
    -- New top-level context fields
    @PDPGoal                 NVARCHAR(500)  = NULL,
    @ImpactStatement         NVARCHAR(500)  = NULL,
    -- Existing fields
    @PriorityName            NVARCHAR(255),
    @ProgrammeName           NVARCHAR(255),
    @LeaderDeptName          NVARCHAR(255)  = NULL,
    @OutcomeName             NVARCHAR(500),
    @IndicatorName           NVARCHAR(500),
    @IndicatorType           NVARCHAR(100)  = NULL,
    @BaselineValue           NVARCHAR(100)  = NULL,
    @TermTargetValue         NVARCHAR(100)  = NULL,
    @AnnualBudget            DECIMAL(18,2)  = NULL,
    @ImplementingInstitution NVARCHAR(500)  = NULL,
    @SupportingInstitutions  NVARCHAR(500)  = NULL,
    @IsCumulative            BIT            = 0,
    @IsPercentage            BIT            = 0,
    @InterventionName        NVARCHAR(500)  = NULL,
    @InterventionIndicator   NVARCHAR(500)  = NULL,
    @Baseline2023_24         NVARCHAR(100)  = NULL,
    @TermTarget2030          NVARCHAR(100)  = NULL,
    @TermBudget              NVARCHAR(100)  = NULL,
    @AnnualTarget            NVARCHAR(100)  = NULL,
    @SpatialReference        NVARCHAR(255)  = NULL,
    @ProposedAction          NVARCHAR(10)   = 'Insert'
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.i_PMTDP_UploadData (
        UploadRequestID,
        PDPGoal,
        ImpactStatement,
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
        @PDPGoal,
        @ImpactStatement,
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
-- STEP 3: Update n_sp_PMTDP_GetUploadData (approval preview)
-- =============================================================================
ALTER PROCEDURE dbo.n_sp_PMTDP_GetUploadData
    @UploadRequestID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        UploadDataID,
        PDPGoal,
        ImpactStatement,
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
-- VERIFICATION
-- =============================================================================
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'i_PMTDP_UploadData'
ORDER BY ORDINAL_POSITION;
GO
