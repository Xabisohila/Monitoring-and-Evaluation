-- ============================================================
-- CLEAN ALL WORKFLOW DATA — fresh start
-- Preserves setup/reference tables:
--   new_Clusters, new_ImplementationInstitutions,
--   new_WorkingGroups, new_ProvincialDevelopmentPlans,
--   all user / auth tables
--
-- Run against MnE_Copy_2
-- ============================================================
USE MnE_Copy_2;
GO

BEGIN TRANSACTION;

-- ── Step 1: Disable ALL FK constraints in the database.
--   This handles any unknown depth of child tables
--   (i_EvidenceFiles → i_QuarterlyReports → i_QuarterlyTargets etc.)
--   without needing to enumerate them explicitly.
-- ─────────────────────────────────────────────────────────────
DECLARE @disableFK NVARCHAR(MAX) = N'';

SELECT @disableFK += N'ALTER TABLE '
    + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
    + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
    + N' NOCHECK CONSTRAINT ALL;' + CHAR(10)
FROM sys.foreign_keys fk;

IF LEN(@disableFK) > 0
    EXEC sp_executesql @disableFK;

-- ── Step 2: Delete in child-first order ───────────────────────
-- (explicit children revealed by error messages come first,
--  then the rest top-down)

-- Deepest known children (delete before their parents)
IF OBJECT_ID('dbo.i_EvidenceFiles',          'U') IS NOT NULL DELETE FROM dbo.i_EvidenceFiles;
IF OBJECT_ID('dbo.i_QuarterlyReports',       'U') IS NOT NULL DELETE FROM dbo.i_QuarterlyReports;
IF OBJECT_ID('dbo.new_InterventionDocuments','U') IS NOT NULL DELETE FROM dbo.new_InterventionDocuments;

-- Owners & targets (leaf-level)
IF OBJECT_ID('dbo.i_IndicatorOwners',         'U') IS NOT NULL DELETE FROM dbo.i_IndicatorOwners;
IF OBJECT_ID('dbo.i_QuarterlyTargets',         'U') IS NOT NULL DELETE FROM dbo.i_QuarterlyTargets;
IF OBJECT_ID('dbo.new_Indicator_Targets',      'U') IS NOT NULL DELETE FROM dbo.new_Indicator_Targets;
IF OBJECT_ID('dbo.i_AnnualTargets',            'U') IS NOT NULL DELETE FROM dbo.i_AnnualTargets;

-- Indicators & intervention indicators
IF OBJECT_ID('dbo.new_Intervention_Indicators','U') IS NOT NULL DELETE FROM dbo.new_Intervention_Indicators;
IF OBJECT_ID('dbo.i_ProgrammeInstitutions',    'U') IS NOT NULL DELETE FROM dbo.i_ProgrammeInstitutions;
IF OBJECT_ID('dbo.i_Indicators',               'U') IS NOT NULL DELETE FROM dbo.i_Indicators;

-- Interventions & POAs
IF OBJECT_ID('dbo.new_Interventions',          'U') IS NOT NULL DELETE FROM dbo.new_Interventions;
IF OBJECT_ID('dbo.new_ProgrammesOfAction',     'U') IS NOT NULL DELETE FROM dbo.new_ProgrammesOfAction;

-- Outcomes, priorities, programmes
IF OBJECT_ID('dbo.i_Outcomes',                 'U') IS NOT NULL DELETE FROM dbo.i_Outcomes;
IF OBJECT_ID('dbo.new_PMTDP_Priorities',       'U') IS NOT NULL DELETE FROM dbo.new_PMTDP_Priorities;
IF OBJECT_ID('dbo.i_IntegrationProgrammes',    'U') IS NOT NULL DELETE FROM dbo.i_IntegrationProgrammes;
IF OBJECT_ID('dbo.i_Priorities',               'U') IS NOT NULL DELETE FROM dbo.i_Priorities;

-- Upload staging + header (try all known naming patterns)
IF OBJECT_ID('dbo.i_PMTDP_UploadData',         'U') IS NOT NULL DELETE FROM dbo.i_PMTDP_UploadData;
IF OBJECT_ID('dbo.n_PMTDPUploadRequests',       'U') IS NOT NULL DELETE FROM dbo.n_PMTDPUploadRequests;
IF OBJECT_ID('dbo.i_PMTDPUploadRequests',       'U') IS NOT NULL DELETE FROM dbo.i_PMTDPUploadRequests;
IF OBJECT_ID('dbo.n_PMTDP_UploadRequests',      'U') IS NOT NULL DELETE FROM dbo.n_PMTDP_UploadRequests;

-- ── Step 3: Re-enable all FK constraints (no WITH CHECK —
--   tables are empty so validation is unnecessary and avoids
--   any edge-case failures on is_not_trusted flags).
-- ─────────────────────────────────────────────────────────────
DECLARE @enableFK NVARCHAR(MAX) = N'';

SELECT @enableFK += N'ALTER TABLE '
    + QUOTENAME(OBJECT_SCHEMA_NAME(fk.parent_object_id)) + N'.'
    + QUOTENAME(OBJECT_NAME(fk.parent_object_id))
    + N' CHECK CONSTRAINT ALL;' + CHAR(10)
FROM sys.foreign_keys fk;

IF LEN(@enableFK) > 0
    EXEC sp_executesql @enableFK;

-- ── Step 4: Reset IDENTITY seeds ──────────────────────────────
IF OBJECT_ID('dbo.i_EvidenceFiles',             'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_EvidenceFiles',             RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_QuarterlyReports',          'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_QuarterlyReports',          RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_InterventionDocuments',    'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_InterventionDocuments',    RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_IndicatorOwners',           'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_IndicatorOwners',           RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_QuarterlyTargets',          'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_QuarterlyTargets',          RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_Indicator_Targets',       'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_Indicator_Targets',       RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_AnnualTargets',             'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_AnnualTargets',             RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_Intervention_Indicators', 'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_Intervention_Indicators', RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_ProgrammeInstitutions',     'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_ProgrammeInstitutions',     RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_Indicators',                'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_Indicators',                RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_Interventions',           'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_Interventions',           RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_ProgrammesOfAction',      'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_ProgrammesOfAction',      RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_Outcomes',                  'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_Outcomes',                  RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.new_PMTDP_Priorities',        'U') IS NOT NULL DBCC CHECKIDENT('dbo.new_PMTDP_Priorities',        RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_IntegrationProgrammes',     'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_IntegrationProgrammes',     RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_Priorities',                'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_Priorities',                RESEED, 0) WITH NO_INFOMSGS;
IF OBJECT_ID('dbo.i_PMTDP_UploadData',          'U') IS NOT NULL DBCC CHECKIDENT('dbo.i_PMTDP_UploadData',          RESEED, 0) WITH NO_INFOMSGS;

-- ── Step 5: Verify — all counts must be 0 ─────────────────────
SELECT 'i_EvidenceFiles'              AS TableName, COUNT(*) AS Rows FROM dbo.i_EvidenceFiles             UNION ALL
SELECT 'i_IndicatorOwners',                         COUNT(*)         FROM dbo.i_IndicatorOwners            UNION ALL
SELECT 'i_QuarterlyReports',                        COUNT(*)         FROM dbo.i_QuarterlyReports           UNION ALL
SELECT 'i_QuarterlyTargets',                        COUNT(*)         FROM dbo.i_QuarterlyTargets            UNION ALL
SELECT 'i_AnnualTargets',                           COUNT(*)         FROM dbo.i_AnnualTargets               UNION ALL
SELECT 'new_InterventionDocuments',                 COUNT(*)         FROM dbo.new_InterventionDocuments      UNION ALL
SELECT 'new_Intervention_Indicators',               COUNT(*)         FROM dbo.new_Intervention_Indicators    UNION ALL
SELECT 'i_Indicators',                              COUNT(*)         FROM dbo.i_Indicators                   UNION ALL
SELECT 'new_Interventions',                         COUNT(*)         FROM dbo.new_Interventions              UNION ALL
SELECT 'new_ProgrammesOfAction',                    COUNT(*)         FROM dbo.new_ProgrammesOfAction         UNION ALL
SELECT 'i_Outcomes',                                COUNT(*)         FROM dbo.i_Outcomes                     UNION ALL
SELECT 'new_PMTDP_Priorities',                      COUNT(*)         FROM dbo.new_PMTDP_Priorities           UNION ALL
SELECT 'i_IntegrationProgrammes',                   COUNT(*)         FROM dbo.i_IntegrationProgrammes        UNION ALL
SELECT 'i_Priorities',                              COUNT(*)         FROM dbo.i_Priorities                   UNION ALL
SELECT 'i_PMTDP_UploadData',                        COUNT(*)         FROM dbo.i_PMTDP_UploadData;

-- ── If every row above shows 0 → COMMIT. Otherwise → ROLLBACK ─
-- ROLLBACK TRANSACTION;
COMMIT TRANSACTION;
