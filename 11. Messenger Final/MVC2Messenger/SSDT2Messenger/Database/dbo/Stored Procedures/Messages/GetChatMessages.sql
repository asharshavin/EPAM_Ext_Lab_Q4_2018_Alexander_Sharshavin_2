CREATE PROCEDURE dbo.GetChatMessages
	@Top int, 
	@ChatID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT top (@Top) [MessageID], [Text], [UserID], [ChatID], [Date] FROM dbo.[Messages] WHERE [ChatID] = @ChatID
END
