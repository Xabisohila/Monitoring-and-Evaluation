-- ============================================================
-- POA Upload Staging Schema + Stored Procedures
-- Run against MnE_Copy_2
-- ============================================================

-- Staging: Upload Request header
IF OBJECT_ID('i_POA_UploadRequest','U') IS NULL
CREATE TABLE i_POA_UploadRequest (
    UploadRequestID      INT           IDENTITY(1,1) PRIMARY KEY,
    UploadedByUserID     INT           NOT NULL,
    UploadDate           DATETIME      NOT NULL DEFAULT GETDATE(),
    FilePath             NVARCHAR(500) NOT NULL,
    Status               NVARCHAR(20)  NOT NULL DEFAULT 'Pending',  -- Pending | Approved | Rejected
    -- POA header metadata extracted from Excel
    GoalName             NVARCHAR(500) NULL,
    PriorityFocus        NVARCHAR(500) NULL,
    IntegrationProgramme NVARCHAR(500) NULL,
    ImpactStatement      NVARCHAR(1000) NULL,
    -- Review
    ReviewedByUserID     INT           NULL,
    ReviewedDate         DATETIME      NULL,
    ReviewComment        NVARCHAR(1000) NULL
)
GO

-- Staging: one row per Excel data row
IF OBJECT_ID('i_POA_UploadData','U') IS NULL
CREATE TABLE i_POA_UploadData (
    UploadDataID            INT           IDENTITY(1,1) PRIMARY KEY,
    UploadRequestID         INT           NOT NULL,
    DesiredOutcome          NVARCHAR(500) NULL,
    OutcomeIndicator        NVARCHAR(500) NULL,
    IndicatorType           NVARCHAR(100) NULL,
    BaselinePDP             NVARCHAR(100) NULL,
    TargetPDP2030           NVARCHAR(100) NULL,
    ImplementingInstitution NVARCHAR(255) NULL,
    NumberIE                NVARCHAR(50)  NULL,
    InterventionName        NVARCHAR(500) NULL,
    InterventionID_Text     NVARCHAR(50)  NULL,
    InterventionIndicator   NVARCHAR(500) NULL,
    Baseline2023_24         NVARCHAR(100) NULL,
    TermTarget2025_2030     NVARCHAR(500) NULL,
    Target2026_27           NVARCHAR(100) NULL,
    AnnualBudget            NVARCHAR(100) NULL,
    SpatialReference        NVARCHAR(255) NULL
)
GO

-- ============================================================
-- SPs
-- ============================================================

CREATE OR ALTER PROCEDURE sp_POAUpload_CreateRequest
    @UploadedByUserID     INT,
    @FilePath             NVARCHAR(500),
    @GoalName             NVARCHAR(500),
    @PriorityFocus        NVARCHAR(500),
    @IntegrationProgramme NVARCHAR(500),
    @ImpactStatement      NVARCHAR(1000),
    @UploadRequestID      INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO i_POA_UploadRequest
        (UploadedByUserID, FilePath, GoalName, PriorityFocus, IntegrationProgramme, ImpactStatement)
    VALUES
        (@UploadedByUserID, @FilePath, @GoalName, @PriorityFocus, @IntegrationProgramme, @ImpactStatement)
    SET @UploadRequestID = SCOPE_IDENTITY()
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_InsertRow
    @UploadRequestID         INT,
    @DesiredOutcome          NVARCHAR(500),
    @OutcomeIndicator        NVARCHAR(500),
    @IndicatorType           NVARCHAR(100),
    @BaselinePDP             NVARCHAR(100),
    @TargetPDP2030           NVARCHAR(100),
    @ImplementingInstitution NVARCHAR(255),
    @NumberIE                NVARCHAR(50),
    @InterventionName        NVARCHAR(500),
    @InterventionID_Text     NVARCHAR(50),
    @InterventionIndicator   NVARCHAR(500),
    @Baseline2023_24         NVARCHAR(100),
    @TermTarget2025_2030     NVARCHAR(500),
    @Target2026_27           NVARCHAR(100),
    @AnnualBudget            NVARCHAR(100),
    @SpatialReference        NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON
    INSERT INTO i_POA_UploadData
        (UploadRequestID, DesiredOutcome, OutcomeIndicator, IndicatorType,
         BaselinePDP, TargetPDP2030, ImplementingInstitution, NumberIE,
         InterventionName, InterventionID_Text, InterventionIndicator,
         Baseline2023_24, TermTarget2025_2030, Target2026_27, AnnualBudget, SpatialReference)
    VALUES
        (@UploadRequestID, @DesiredOutcome, @OutcomeIndicator, @IndicatorType,
         @BaselinePDP, @TargetPDP2030, @ImplementingInstitution, @NumberIE,
         @InterventionName, @InterventionID_Text, @InterventionIndicator,
         @Baseline2023_24, @TermTarget2025_2030, @Target2026_27, @AnnualBudget, @SpatialReference)
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_GetMyUploads
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON
    SELECT r.UploadRequestID, r.UploadDate, r.Status, r.PriorityFocus,
           r.IntegrationProgramme, r.GoalName, r.ReviewComment,
           (SELECT COUNT(*) FROM i_POA_UploadData d WHERE d.UploadRequestID = r.UploadRequestID) AS DataRowCount
    FROM i_POA_UploadRequest r
    WHERE r.UploadedByUserID = @UserID
    ORDER BY r.UploadDate DESC
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_GetPendingUploads
    @CurrentUserID INT
AS
BEGIN
    SET NOCOUNT ON
    SELECT r.UploadRequestID, r.UploadDate, r.Status, r.PriorityFocus,
           r.IntegrationProgramme, r.GoalName,
           ISNULL(u.FirstName,'') + ' ' + ISNULL(u.Lastname,'') AS UploadedBy,
           (SELECT COUNT(*) FROM i_POA_UploadData d WHERE d.UploadRequestID = r.UploadRequestID) AS DataRowCount
    FROM i_POA_UploadRequest r
    JOIN apl_user u ON u.apl_user_id = r.UploadedByUserID
    WHERE r.Status = 'Pending'
      AND r.UploadedByUserID <> @CurrentUserID
    ORDER BY r.UploadDate ASC
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_GetAllUploads
AS
BEGIN
    SET NOCOUNT ON
    SELECT r.UploadRequestID, r.UploadDate, r.Status, r.PriorityFocus,
           r.IntegrationProgramme, r.GoalName,
           ISNULL(u.FirstName,'') + ' ' + ISNULL(u.Lastname,'') AS UploadedBy,
           (SELECT COUNT(*) FROM i_POA_UploadData d WHERE d.UploadRequestID = r.UploadRequestID) AS DataRowCount
    FROM i_POA_UploadRequest r
    JOIN apl_user u ON u.apl_user_id = r.UploadedByUserID
    ORDER BY r.UploadDate DESC
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_GetUploadHeader
    @UploadRequestID INT
AS
BEGIN
    SET NOCOUNT ON
    SELECT r.*,
           ISNULL(u.FirstName,'') + ' ' + ISNULL(u.Lastname,'') AS UploadedBy
    FROM i_POA_UploadRequest r
    JOIN apl_user u ON u.apl_user_id = r.UploadedByUserID
    WHERE r.UploadRequestID = @UploadRequestID
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_GetUploadData
    @UploadRequestID INT
AS
BEGIN
    SET NOCOUNT ON
    SELECT * FROM i_POA_UploadData
    WHERE UploadRequestID = @UploadRequestID
    ORDER BY UploadDataID
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_Reject
    @UploadRequestID INT,
    @ReviewerUserID  INT,
    @ReviewComment   NVARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE i_POA_UploadRequest
    SET Status           = 'Rejected',
        ReviewedByUserID = @ReviewerUserID,
        ReviewedDate     = GETDATE(),
        ReviewComment    = @ReviewComment
    WHERE UploadRequestID = @UploadRequestID
END
GO

CREATE OR ALTER PROCEDURE sp_POAUpload_MarkApproved
    @UploadRequestID INT,
    @ReviewerUserID  INT,
    @ReviewComment   NVARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON
    UPDATE i_POA_UploadRequest
    SET Status           = 'Approved',
        ReviewedByUserID = @ReviewerUserID,
        ReviewedDate     = GETDATE(),
        ReviewComment    = @ReviewComment
    WHERE UploadRequestID = @UploadRequestID
END
GO

-- Used by the approval view to populate the Priority dropdown
CREATE OR ALTER PROCEDURE sp_POAUpload_GetPrioritiesForMapping
AS
BEGIN
    SET NOCOUNT ON
    SELECT p.PMTDP_PriorityID, p.PriorityName,
           ISNULL(p.ClusterID, 0) AS ClusterID,
           ISNULL(c.ClusterName,'—') AS ClusterName
    FROM new_PMTDP_Priorities p
    LEFT JOIN new_Clusters c ON c.ClusterID = p.ClusterID
    ORDER BY p.PriorityName
END
GO

-- Clear staged data + reset a rejected request so it can be resubmitted
CREATE OR ALTER PROCEDURE sp_POAUpload_ClearAndReset
    @UploadRequestID      INT,
    @ResubmittedByUserID  INT,
    @FilePath             NVARCHAR(500),
    @GoalName             NVARCHAR(500),
    @PriorityFocus        NVARCHAR(500),
    @IntegrationProgramme NVARCHAR(500),
    @ImpactStatement      NVARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON
    -- Security: only the original submitter can resubmit their own rejected upload
    IF NOT EXISTS (
        SELECT 1 FROM i_POA_UploadRequest
        WHERE UploadRequestID = @UploadRequestID
          AND UploadedByUserID = @ResubmittedByUserID
          AND Status = 'Rejected'
    ) RETURN

    DELETE FROM i_POA_UploadData WHERE UploadRequestID = @UploadRequestID

    UPDATE i_POA_UploadRequest SET
        Status               = 'Pending',
        FilePath             = @FilePath,
        GoalName             = @GoalName,
        PriorityFocus        = @PriorityFocus,
        IntegrationProgramme = @IntegrationProgramme,
        ImpactStatement      = @ImpactStatement,
        UploadDate           = GETDATE(),
        ReviewedByUserID     = NULL,
        ReviewedDate         = NULL,
        ReviewComment        = NULL,
        CreatedPOA_ID        = NULL
    WHERE UploadRequestID = @UploadRequestID
END
GO
