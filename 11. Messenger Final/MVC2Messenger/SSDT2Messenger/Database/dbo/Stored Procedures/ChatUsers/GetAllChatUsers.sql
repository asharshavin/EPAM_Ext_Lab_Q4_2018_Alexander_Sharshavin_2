CREATE PROCEDURE dbo.GetAllChatUsers
	@ChatID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [UserID], [ChatID] FROM dbo.ChatUsers WHERE ChatID = @ChatID
END
