-- =============================================================================
-- PRIORITY → CLUSTER MAPPING TABLE
-- Run against: MnE_Copy_2
--
-- What this does:
--   1. Creates dbo.i_PriorityClusterMap — maps priority name keywords → ClusterID
--   2. Pre-populates with P-MTDP priority → cluster mappings from the document
--   3. Adds ApprovedByUserID / ApprovedDate / ClusterAssignmentMethod /
--      SuggestedClusterID to dbo.new_PMTDP_Priorities
--   4. Updates n_sp_PMTDP_ApplyApprovedRow to:
--        - resolve ClusterID via an exact match first, falling back to the
--          old CHARINDEX substring match only as a Fuzzy suggestion that is
--          never auto-written to ClusterID
--        - persist who applied/approved each row and when
--      Existing manually-assigned ClusterIDs are never overwritten.
-- =============================================================================

USE MnE_Copy_2;
GO

-- =============================================================================
-- STEP 1: Create mapping table
-- =============================================================================
IF OBJECT_ID('dbo.i_PriorityClusterMap', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.i_PriorityClusterMap (
        MapID            INT IDENTITY(1,1) PRIMARY KEY,
        PriorityKeyword  NVARCHAR(300)  NOT NULL,
        ClusterID        INT            NOT NULL REFERENCES dbo.new_Clusters(ClusterID),
        Notes            NVARCHAR(500)  NULL
    );
    PRINT 'Created dbo.i_PriorityClusterMap';
END
ELSE
    PRINT 'dbo.i_PriorityClusterMap already exists — skipping CREATE';
GO

-- =============================================================================
-- STEP 2: Pre-populate with known P-MTDP → Cluster mappings
-- =============================================================================
IF NOT EXISTS (SELECT 1 FROM dbo.i_PriorityClusterMap)
BEGIN
    INSERT INTO dbo.i_PriorityClusterMap (PriorityKeyword, ClusterID, Notes) VALUES

    -- Priority 1: Drive Inclusive Growth and Job Creation  → Economic Sector (403)
    ('Drive Inclusive Growth and Job Creation',      403, 'P-MTDP Priority 1 — full name'),
    ('Drive Inclusive Growth',                       403, 'P-MTDP Priority 1 — partial'),
    ('Inclusive Growth and Job Creation',            403, 'P-MTDP Priority 1 — partial'),
    ('Job Creation',                                 403, 'P-MTDP Priority 1 — keyword'),
    ('Economic Growth',                              403, 'P-MTDP Priority 1 — short form used in Priority Focus field'),
    ('Inclusive Economy',                            403, 'P-MTDP Priority 1 — short form variant'),
    ('Economic',                                     403, 'P-MTDP Priority 1 — fallback keyword'),

    -- Priority 2: Reduce Poverty and Tackle the High Cost of Living → Social Protection (402)
    ('Reduce Poverty and Tackle the High Cost of Living', 402, 'P-MTDP Priority 2 — full name'),
    ('Reduce Poverty and Tackle',                    402, 'P-MTDP Priority 2 — partial'),
    ('Reduce Poverty',                               402, 'P-MTDP Priority 2 — keyword'),
    ('High Cost of Living',                          402, 'P-MTDP Priority 2 — keyword'),

    -- Priority 3: Build a Capable, Ethical and Developmental State
    -- → Governance (401) is the PRIMARY cluster.
    --   Justice cluster (404) is secondary; assign manually via admin page if needed.
    ('Build a Capable, Ethical and Developmental State', 401, 'P-MTDP Priority 3 — full name; primary=Governance (401). Secondary Justice (404) must be assigned manually.'),
    ('Capable, Ethical and Developmental',           401, 'P-MTDP Priority 3 — partial'),
    ('Developmental State',                          401, 'P-MTDP Priority 3 — keyword'),
    ('Capable and Ethical',                          401, 'P-MTDP Priority 3 — keyword');

    PRINT 'Inserted mapping rows';
END
ELSE
    PRINT 'i_PriorityClusterMap already has rows — skipping INSERT';
GO

-- =============================================================================
-- STEP 2b: Extend new_PMTDP_Priorities with approval + cluster-match audit columns
--   ApprovedByUserID       — who last applied/approved this priority row
--   ApprovedDate           — when
--   ClusterAssignmentMethod — 'Exact', 'Fuzzy', or NULL (no match found)
--   SuggestedClusterID     — a Fuzzy match is stored here for review, never
--                            auto-written to ClusterID — a PU must confirm it
--                            via the admin page before it becomes authoritative.
-- =============================================================================
IF COL_LENGTH('dbo.new_PMTDP_Priorities', 'ApprovedByUserID') IS NULL
    ALTER TABLE dbo.new_PMTDP_Priorities ADD ApprovedByUserID INT NULL;
GO
IF COL_LENGTH('dbo.new_PMTDP_Priorities', 'ApprovedDate') IS NULL
    ALTER TABLE dbo.new_PMTDP_Priorities ADD ApprovedDate DATETIME NULL;
GO
IF COL_LENGTH('dbo.new_PMTDP_Priorities', 'ClusterAssignmentMethod') IS NULL
    ALTER TABLE dbo.new_PMTDP_Priorities ADD ClusterAssignmentMethod NVARCHAR(10) NULL;
GO
IF COL_LENGTH('dbo.new_PMTDP_Priorities', 'SuggestedClusterID') IS NULL
    ALTER TABLE dbo.new_PMTDP_Priorities ADD SuggestedClusterID INT NULL REFERENCES dbo.new_Clusters(ClusterID);
GO

-- =============================================================================
-- STEP 3: Update n_sp_PMTDP_ApplyApprovedRow
--
-- Changes from the previous version:
--   1. PART B now resolves ClusterID in two tiers: an exact match against
--      i_PriorityClusterMap first, falling back to the old CHARINDEX substring
--      match only if no exact match exists. Only an EXACT match is written to
--      ClusterID automatically — a Fuzzy (substring) match is written to
--      SuggestedClusterID instead, so it never silently misclassifies a
--      priority whose free-text name happens to contain a short keyword.
--   2. ApprovedByUserID / ApprovedDate are now persisted on new_PMTDP_Priorities
--      instead of being accepted and discarded — closes the "for future audit
--      trail use" gap.
-- Existing rows keep their manually-assigned ClusterID untouched; only the
-- approver stamp is refreshed on re-apply.
-- =============================================================================
ALTER PROCEDURE dbo.n_sp_PMTDP_ApplyApprovedRow
    @UploadDataID     INT,
    @ApprovedByUserID INT = NULL    -- for future audit trail use
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE
        @PriorityName            NVARCHAR(255),
        @ProgrammeName           NVARCHAR(255),
        @LeaderDeptName          NVARCHAR(255),
        @OutcomeName             NVARCHAR(500),
        @IndicatorName           NVARCHAR(500),
        @IndicatorType           NVARCHAR(100),
        @BaselineValue           NVARCHAR(100),
        @TermTargetValue         NVARCHAR(100),
        @AnnualBudget            DECIMAL(18,2),
        @ImplementingInstitution NVARCHAR(500),
        @SupportingInstitutions  NVARCHAR(500),
        @IsCumulative            BIT,
        @IsPercentage            BIT,
        @InterventionName        NVARCHAR(500),
        @InterventionIndicator   NVARCHAR(500),
        @Baseline2023_24         NVARCHAR(100),
        @TermTarget2030          NVARCHAR(100),
        @TermBudget              NVARCHAR(100),
        @AnnualTarget            NVARCHAR(100),
        @SpatialReference        NVARCHAR(255);

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
        SELECT
            NULL AS i_PriorityID,
            NULL AS i_ProgrammeID,
            NULL AS i_OutcomeID,
            NULL AS i_IndicatorID,
            NULL AS new_PMTDP_PriorityID,
            NULL AS ClusterAutoAssigned,
            'Skipped — missing required fields' AS ApplyStatus;
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE
            @i_PriorityID  INT,
            @i_ProgrammeID INT,
            @i_OutcomeID   INT,
            @i_IndicatorID INT,
            @new_PriorityID INT;

        -- ── PART A: i_* monitoring tables (unchanged) ─────────────────────

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

        -- ── PART B: new_PMTDP_Priorities ─────────────────────────────────

        DECLARE @activePDP_ID INT;
        SELECT TOP 1 @activePDP_ID = PDP_ID
        FROM dbo.new_ProvincialDevelopmentPlans
        ORDER BY PDP_StartYear DESC;

        IF @activePDP_ID IS NULL
            SET @activePDP_ID = 1;

        -- Resolve ClusterID in two tiers:
        --   Tier 1 — Exact match: PriorityKeyword equals the full @PriorityName.
        --            Safe to auto-assign.
        --   Tier 2 — Fuzzy match: CHARINDEX substring match, longest keyword
        --            wins (most specific wins). NOT auto-assigned to ClusterID —
        --            stored as a suggestion only; a PU must confirm it manually.
        DECLARE
            @autoClusterID      INT = NULL,
            @suggestedClusterID INT = NULL,
            @clusterMatchType   NVARCHAR(10) = NULL;

        SELECT TOP 1 @autoClusterID = ClusterID
        FROM dbo.i_PriorityClusterMap
        WHERE LTRIM(RTRIM(PriorityKeyword)) = LTRIM(RTRIM(@PriorityName));

        IF @autoClusterID IS NOT NULL
            SET @clusterMatchType = 'Exact';
        ELSE
        BEGIN
            SELECT TOP 1 @suggestedClusterID = ClusterID
            FROM dbo.i_PriorityClusterMap
            WHERE CHARINDEX(PriorityKeyword, @PriorityName) > 0
            ORDER BY LEN(PriorityKeyword) DESC;

            IF @suggestedClusterID IS NOT NULL
                SET @clusterMatchType = 'Fuzzy';
        END

        SELECT @new_PriorityID = PMTDP_PriorityID
        FROM dbo.new_PMTDP_Priorities
        WHERE PriorityName = @PriorityName
          AND PDP_ID       = @activePDP_ID;

        IF @new_PriorityID IS NULL
        BEGIN
            INSERT INTO dbo.new_PMTDP_Priorities
                (PDP_ID, PriorityName, PriorityDescription, DesiredOutcome, ClusterID,
                 ClusterAssignmentMethod, SuggestedClusterID, ApprovedByUserID, ApprovedDate)
            VALUES
                (@activePDP_ID, @PriorityName, NULL, @OutcomeName, @autoClusterID,
                 @clusterMatchType, @suggestedClusterID, @ApprovedByUserID, GETDATE());
            SET @new_PriorityID = SCOPE_IDENTITY();
        END
        ELSE
        BEGIN
            -- Existing row: ClusterID, ClusterAssignmentMethod and SuggestedClusterID
            -- may already reflect a PU's manual decision — never overwritten here.
            -- Only refresh who last (re)applied this row and when.
            UPDATE dbo.new_PMTDP_Priorities SET
                ApprovedByUserID = @ApprovedByUserID,
                ApprovedDate     = GETDATE()
            WHERE PMTDP_PriorityID = @new_PriorityID;
        END

        COMMIT TRANSACTION;

        SELECT
            @i_PriorityID       AS i_PriorityID,
            @i_ProgrammeID      AS i_ProgrammeID,
            @i_OutcomeID        AS i_OutcomeID,
            @i_IndicatorID      AS i_IndicatorID,
            @new_PriorityID     AS new_PMTDP_PriorityID,
            ISNULL(CAST(@autoClusterID AS VARCHAR), 'No exact match') AS ClusterAutoAssigned,
            ISNULL(CAST(@suggestedClusterID AS VARCHAR), 'None') AS ClusterSuggested,
            ISNULL(@clusterMatchType, 'None') AS ClusterMatchType,
            'Applied'           AS ApplyStatus;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- =============================================================================
-- STEP 5: Verify
-- =============================================================================
SELECT
    m.MapID,
    m.PriorityKeyword,
    c.ClusterName,
    m.Notes
FROM dbo.i_PriorityClusterMap m
JOIN dbo.new_Clusters c ON c.ClusterID = m.ClusterID
ORDER BY c.ClusterID, LEN(m.PriorityKeyword) DESC;
GO

-- Rows applied since this script ran, with their resolution + approver stamp.
-- SuggestedClusterID populated but ClusterID still NULL == needs a PU's manual
-- confirmation before it's authoritative.
SELECT
    p.PMTDP_PriorityID,
    p.PriorityName,
    p.ClusterID,
    p.ClusterAssignmentMethod,
    p.SuggestedClusterID,
    p.ApprovedByUserID,
    p.ApprovedDate
FROM dbo.new_PMTDP_Priorities p
WHERE p.ApprovedDate IS NOT NULL
ORDER BY p.ApprovedDate DESC;
GO
