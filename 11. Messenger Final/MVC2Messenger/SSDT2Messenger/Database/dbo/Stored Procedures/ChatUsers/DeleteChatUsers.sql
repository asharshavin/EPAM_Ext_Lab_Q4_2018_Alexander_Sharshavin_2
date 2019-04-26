CREATE PROCEDURE dbo.DeleteChatUser
	@ChatID int,
	@UserID int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM dbo.ChatUsers WHERE ChatID =  @ChatID AND  UserID =  @UserID 
END