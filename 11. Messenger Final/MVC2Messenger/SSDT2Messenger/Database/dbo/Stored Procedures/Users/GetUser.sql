CREATE PROCEDURE dbo.GetUser
	@UserID int
AS
BEGIN
	SELECT  [UserID], [Username], [DateOfCreation], [Role], [Password] FROM dbo.[Users] WHERE UserID = @UserID
END