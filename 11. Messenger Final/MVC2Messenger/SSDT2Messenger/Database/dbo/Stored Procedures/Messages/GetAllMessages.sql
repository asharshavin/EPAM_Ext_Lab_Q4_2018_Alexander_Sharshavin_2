CREATE PROCEDURE dbo.GetAllMessages
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [MessageID], [Text], [UserID], [ChatID], [Date] FROM dbo.[Messages]
END
