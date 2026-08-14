-- ============================================================
-- CLEAN ALL WORKFLOW DATA — fresh start
-- Preserves setup/reference tables:
--   new_Clusters, new_ImplementationInstitutions,
--   new_WorkingGroups, new_ProvincialDevelopmentPlans,
--   new_FinancialYears, new_Framework, new_Municipalities,
--   new_Provinces, i_PriorityClusterMap, i_Clusters,
--   i_Departments, i_Institutions, i_Roles, i_Users,
--   i_WorkingGroups, i_WorkflowStatus, i_SystemPeriods,
--   all auth tables
--
-- Run against MnE_Copy_2
-- ============================================================
USE MnE_Copy_2;
GO

BEGIN TRANSACTION;

-- ── Step 1: Disable ALL FK constraints in the database.
-- ─────────────────────────────────────────────────────────────
DECLARE @disableFK NVARCHAR(MAX) = N'';

SELECT @disableFK += N'ALTER TABLE '
    + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
    + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
    + N' NOCHECK CONSTRAINT ALL;' + CHAR(10)
FROM sys.foreign_keys fk;

IF LEN(@disableFK) > 0
    EXEC sp_executesql @disableFK;

-- ── Step 2: Delete workflow data ──────────────────────────────

-- Audit & workflow tracking (deepest — no children)
IF OBJECT_ID('dbo.i_AuditTrail',                  'U') IS NOT NULL DELETE FROM dbo.i_AuditTrail;
IF OBJECT_ID('dbo.i_WorkflowHistory',              'U') IS NOT NULL DELETE FROM dbo.i_WorkflowHistory;
IF OBJECT_ID('dbo.i_Notifications',                'U') IS NOT NULL DELETE FROM dbo.i_Notifications;
IF OBJECT_ID('dbo.i_ChangeRequests',               'U') IS NOT NULL DELETE FROM dbo.i_ChangeRequests;

-- Evidence & quarterly reporting
IF OBJECT_ID('dbo.i_EvidenceFiles',                'U') IS NOT NULL DELETE FROM dbo.i_EvidenceFiles;
IF OBJECT_ID('dbo.i_QuarterlyReports',             'U') IS NOT NULL DELETE FROM dbo.i_QuarterlyReports;
IF OBJECT_ID('dbo.new_QuarterlyReports',           'U') IS NOT NULL DELETE FROM dbo.new_QuarterlyReports;
IF OBJECT_ID('dbo.new_POA_Achieved_Targets_5Years','U') IS NOT NULL DELETE FROM dbo.new_POA_Achieved_Targets_5Years;
IF OBJECT_ID('dbo.new_InterventionDocuments',      'U') IS NOT NULL DELETE FROM dbo.new_InterventionDocuments;
IF OBJECT_ID('dbo.new_InterventionApprovals',      'U') IS NOT NULL DELETE FROM dbo.new_InterventionApprovals;

-- Targets & owners (leaf-level)
IF OBJECT_ID('dbo.i_IndicatorOwners',              'U') IS NOT NULL DELETE FROM dbo.i_IndicatorOwners;
IF OBJECT_ID('dbo.i_QuarterlyTargets',             'U') IS NOT NULL DELETE FROM dbo.i_QuarterlyTargets;
IF OBJECT_ID('dbo.new_Indicator_Targets',          'U') IS NOT NULL DELETE FROM dbo.new_Indicator_Targets;
IF OBJECT_ID('dbo.i_AnnualTargets',                'U') IS NOT NULL DELETE FROM dbo.i_AnnualTargets;

-- Indicators & working group maps
IF OBJECT_ID('dbo.i_IndicatorWorkingGroupMap',     'U') IS NOT NULL DELETE FROM dbo.i_IndicatorWorkingGroupMap;
IF OBJECT_ID('dbo.new_Intervention_Indicators',    'U') IS NOT NULL DELETE FROM dbo.new_Intervention_Indicators;
IF OBJECT_ID('dbo.i_ProgrammeInstitutions',        'U') IS NOT NULL DELETE FROM dbo.i_ProgrammeInstitutions;
IF OBJECT_ID('dbo.i_Indicators',                   'U') IS NOT NULL DELETE FROM dbo.i_Indicators;

-- Sub-outcomes
IF OBJECT_ID('dbo.new_SubOutcomes',                'U') IS NOT NULL DELETE FROM dbo.new_SubOutcomes;

-- Interventions & POAs
IF OBJECT_ID('dbo.new_Intervention_Budgets',       'U') IS NOT NULL DELETE FROM dbo.new_Intervention_Budgets;
IF OBJECT_ID('dbo.new_Interventions',              'U') IS NOT NULL DELETE FROM dbo.new_Interventions;
IF OBJECT_ID('dbo.new_ProgrammesOfAction',         'U') IS NOT NULL DELETE FROM dbo.new_ProgrammesOfAction;

-- Outcomes, priorities, programmes
IF OBJECT_ID('dbo.i_Outcomes',                     'U') IS NOT NULL DELETE FROM dbo.i_Outcomes;
IF OBJECT_ID('dbo.new_PMTDP_Priorities',           'U') IS NOT NULL DELETE FROM dbo.new_PMTDP_Priorities;
IF OBJECT_ID('dbo.i_IntegrationProgrammes',        'U') IS NOT NULL DELETE FROM dbo.i_IntegrationProgrammes;
IF OBJECT_ID('dbo.i_Priorities',                   'U') IS NOT NULL DELETE FROM dbo.i_Priorities;

-- Upload staging
IF OBJECT_ID('dbo.i_POA_UploadData',               'U') IS NOT NULL DELETE FROM dbo.i_POA_UploadData;
IF OBJECT_ID('dbo.i_POA_UploadRequest',            'U') IS NOT NULL DELETE FROM dbo.i_POA_UploadRequest;
IF OBJECT_ID('dbo.i_PMTDP_UploadData',             'U') IS NOT NULL DELETE FROM dbo.i_PMTDP_UploadData;
IF OBJECT_ID('dbo.i_PMTDP_UploadRequest',          'U') IS NOT NULL DELETE FROM dbo.i_PMTDP_UploadRequest;

-- ── Step 3: Re-enable all FK constraints ──────────────────────
DECLARE @enableFK NVARCHAR(MAX) = N'';

SELECT @enableFK += N'ALTER TABLE '
    + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
    + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
    + N' CHECK CONSTRAINT ALL;' + CHAR(10)
FROM sys.foreign_keys fk;

IF LEN(@enableFK) > 0
    EXEC sp_executesql @enableFK;

-- ── Step 4: Reset IDENTITY seeds ──────────────────────────────
IF OBJECT_ID('dbo.i_AuditTrail',                   'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_AuditTrail',                   RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_WorkflowHistory',              'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_WorkflowHistory',              RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_Notifications',                'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_Notifications',                RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_ChangeRequests',               'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_ChangeRequests',               RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_EvidenceFiles',                'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_EvidenceFiles',                RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_QuarterlyReports',             'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_QuarterlyReports',             RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_QuarterlyReports',           'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_QuarterlyReports',           RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_POA_Achieved_Targets_5Years','U') IS NOT NULL DBCC CHECKIDENT('dbo.new_POA_Achieved_Targets_5Years',RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_InterventionDocuments',      'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_InterventionDocuments',      RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_InterventionApprovals',      'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_InterventionApprovals',      RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_IndicatorOwners',              'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_IndicatorOwners',              RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_QuarterlyTargets',             'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_QuarterlyTargets',             RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_Indicator_Targets',          'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_Indicator_Targets',          RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_AnnualTargets',                'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_AnnualTargets',                RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_IndicatorWorkingGroupMap',     'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_IndicatorWorkingGroupMap',     RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_Intervention_Indicators',    'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_Intervention_Indicators',    RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_ProgrammeInstitutions',        'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_ProgrammeInstitutions',        RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_Indicators',                   'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_Indicators',                   RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_SubOutcomes',                'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_SubOutcomes',                RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_Intervention_Budgets',       'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_Intervention_Budgets',       RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_Interventions',              'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_Interventions',              RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_ProgrammesOfAction',         'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_ProgrammesOfAction',         RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_Outcomes',                     'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_Outcomes',                     RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_PMTDP_Priorities',           'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_PMTDP_Priorities',           RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_IntegrationProgrammes',        'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_IntegrationProgrammes',        RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_Priorities',                   'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_Priorities',                   RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_POA_UploadData',               'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_POA_UploadData',               RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_POA_UploadRequest',            'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_POA_UploadRequest',            RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_PMTDP_UploadData',             'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_PMTDP_UploadData',             RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_PMTDP_UploadRequest',          'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_PMTDP_UploadRequest',          RESEED, 0) WITH NO_INFOMSGS;

-- ── Step 5: Verify — all counts must be 0 ─────────────────────
SELECT 'i_AuditTrail'                   AS TableName, COUNT(*) AS Rows FROM dbo.i_AuditTrail                   UNION ALL
SELECT 'i_WorkflowHistory',                           COUNT(*)         FROM dbo.i_WorkflowHistory               UNION ALL
SELECT 'i_Notifications',                             COUNT(*)         FROM dbo.i_Notifications                 UNION ALL
SELECT 'i_ChangeRequests',                            COUNT(*)         FROM dbo.i_ChangeRequests                UNION ALL
SELECT 'i_EvidenceFiles',                             COUNT(*)         FROM dbo.i_EvidenceFiles                 UNION ALL
SELECT 'i_QuarterlyReports',                          COUNT(*)         FROM dbo.i_QuarterlyReports              UNION ALL
SELECT 'new_QuarterlyReports',                        COUNT(*)         FROM dbo.new_QuarterlyReports            UNION ALL
SELECT 'new_POA_Achieved_Targets_5Years',             COUNT(*)         FROM dbo.new_POA_Achieved_Targets_5Years UNION ALL
SELECT 'new_InterventionDocuments',                   COUNT(*)         FROM dbo.new_InterventionDocuments       UNION ALL
SELECT 'new_InterventionApprovals',                   COUNT(*)         FROM dbo.new_InterventionApprovals       UNION ALL
SELECT 'i_IndicatorOwners',                           COUNT(*)         FROM dbo.i_IndicatorOwners               UNION ALL
SELECT 'i_QuarterlyTargets',                          COUNT(*)         FROM dbo.i_QuarterlyTargets              UNION ALL
SELECT 'new_Indicator_Targets',                       COUNT(*)         FROM dbo.new_Indicator_Targets           UNION ALL
SELECT 'i_AnnualTargets',                             COUNT(*)         FROM dbo.i_AnnualTargets                 UNION ALL
SELECT 'i_IndicatorWorkingGroupMap',                  COUNT(*)         FROM dbo.i_IndicatorWorkingGroupMap      UNION ALL
SELECT 'new_Intervention_Indicators',                 COUNT(*)         FROM dbo.new_Intervention_Indicators     UNION ALL
SELECT 'i_ProgrammeInstitutions',                     COUNT(*)         FROM dbo.i_ProgrammeInstitutions         UNION ALL
SELECT 'i_Indicators',                                COUNT(*)         FROM dbo.i_Indicators                    UNION ALL
SELECT 'new_SubOutcomes',                             COUNT(*)         FROM dbo.new_SubOutcomes                 UNION ALL
SELECT 'new_Intervention_Budgets',                    COUNT(*)         FROM dbo.new_Intervention_Budgets        UNION ALL
SELECT 'new_Interventions',                           COUNT(*)         FROM dbo.new_Interventions               UNION ALL
SELECT 'new_ProgrammesOfAction',                      COUNT(*)         FROM dbo.new_ProgrammesOfAction          UNION ALL
SELECT 'i_Outcomes',                                  COUNT(*)         FROM dbo.i_Outcomes                      UNION ALL
SELECT 'new_PMTDP_Priorities',                        COUNT(*)         FROM dbo.new_PMTDP_Priorities            UNION ALL
SELECT 'i_IntegrationProgrammes',                     COUNT(*)         FROM dbo.i_IntegrationProgrammes         UNION ALL
SELECT 'i_Priorities',                                COUNT(*)         FROM dbo.i_Priorities                    UNION ALL
SELECT 'i_POA_UploadData',                            COUNT(*)         FROM dbo.i_POA_UploadData                UNION ALL
SELECT 'i_POA_UploadRequest',                         COUNT(*)         FROM dbo.i_POA_UploadRequest             UNION ALL
SELECT 'i_PMTDP_UploadData',                          COUNT(*)         FROM dbo.i_PMTDP_UploadData              UNION ALL
SELECT 'i_PMTDP_UploadRequest',                       COUNT(*)         FROM dbo.i_PMTDP_UploadRequest;

-- ── If every row above shows 0 → COMMIT. Otherwise → ROLLBACK ─
-- ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
