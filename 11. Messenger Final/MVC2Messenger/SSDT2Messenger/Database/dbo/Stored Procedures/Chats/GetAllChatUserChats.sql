CREATE PROCEDURE dbo.GetAllChatUserChats
	@UserID int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT C.[ChatID], [Name] FROM dbo.[Chats] AS C
	INNER JOIN dbo.ChatUsers AS CU
	ON C.ChatID = CU.ChatID AND CU.UserID = @UserID

END
