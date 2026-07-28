-- ============================================================
-- Notifications — table + stored procedures
-- Run once against MnE_Copy_2
-- ============================================================
USE MnE_Copy_2;
GO

-- ── Table ─────────────────────────────────────────────────────
IF OBJECT_ID('dbo.Notifications', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Notifications
    (
        NotificationID INT          IDENTITY(1,1) NOT NULL PRIMARY KEY,
        ReportID       INT          NULL,
        UserID         INT          NULL,
        Message        NVARCHAR(MAX) NULL,
        SentDate       DATETIME     NOT NULL CONSTRAINT DF_Notifications_SentDate DEFAULT GETDATE(),
        IsRead         BIT          NOT NULL CONSTRAINT DF_Notifications_IsRead   DEFAULT 0
    );
END
GO

-- ── sp_Notification_Upsert ─────────────────────────────────────
-- Insert when @NotificationID IS NULL / 0; update otherwise.
-- Returns the NotificationID.
CREATE OR ALTER PROCEDURE dbo.sp_Notification_Upsert
    @NotificationID INT          = NULL,
    @ReportID       INT          = NULL,
    @UserID         INT          = NULL,
    @Message        NVARCHAR(MAX)= NULL,
    @IsRead         BIT          = 0
AS
BEGIN
    SET NOCOUNT ON;

    IF ISNULL(@NotificationID, 0) = 0
    BEGIN
        INSERT INTO dbo.Notifications (ReportID, UserID, Message, SentDate, IsRead)
        VALUES (@ReportID, @UserID, @Message, GETDATE(), @IsRead);

        SELECT SCOPE_IDENTITY() AS NotificationID;
    END
    ELSE
    BEGIN
        UPDATE dbo.Notifications
        SET    ReportID = @ReportID,
               UserID   = @UserID,
               Message  = @Message,
               IsRead   = @IsRead
        WHERE  NotificationID = @NotificationID;

        SELECT @NotificationID AS NotificationID;
    END
END
GO

-- ── sp_Notification_GetByID ────────────────────────────────────
CREATE OR ALTER PROCEDURE dbo.sp_Notification_GetByID
    @NotificationID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NotificationID, ReportID, UserID, Message, SentDate, IsRead
    FROM   dbo.Notifications
    WHERE  NotificationID = @NotificationID;
END
GO

-- ── sp_Notification_List ───────────────────────────────────────
CREATE OR ALTER PROCEDURE dbo.sp_Notification_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NotificationID, ReportID, UserID, Message, SentDate, IsRead
    FROM   dbo.Notifications
    ORDER  BY SentDate DESC;
END
GO

-- ── sp_Notification_ListByUser ─────────────────────────────────
CREATE OR ALTER PROCEDURE dbo.sp_Notification_ListByUser
    @UserID     INT,
    @OnlyUnread BIT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NotificationID, ReportID, UserID, Message, SentDate, IsRead
    FROM   dbo.Notifications
    WHERE  UserID = @UserID
      AND  (@OnlyUnread = 0 OR IsRead = 0)
    ORDER  BY SentDate DESC;
END
GO

-- ── sp_Notification_MarkRead ───────────────────────────────────
CREATE OR ALTER PROCEDURE dbo.sp_Notification_MarkRead
    @NotificationID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Notifications SET IsRead = 1 WHERE NotificationID = @NotificationID;
    SELECT @NotificationID AS NotificationID;
END
GO

-- ── sp_Notification_MarkAllReadForUser ─────────────────────────
CREATE OR ALTER PROCEDURE dbo.sp_Notification_MarkAllReadForUser
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Notifications SET IsRead = 1 WHERE UserID = @UserID;
    SELECT @UserID AS UserID;
END
GO

-- ── sp_Notification_UnreadCount ────────────────────────────────
CREATE OR ALTER PROCEDURE dbo.sp_Notification_UnreadCount
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) AS UnreadCount
    FROM   dbo.Notifications
    WHERE  UserID = @UserID
      AND  IsRead = 0;
END
GO

-- ── sp_Notification_Delete ─────────────────────────────────────
CREATE OR ALTER PROCEDURE dbo.sp_Notification_Delete
    @NotificationID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM dbo.Notifications WHERE NotificationID = @NotificationID;
    SELECT @NotificationID AS NotificationID;
END
GO
