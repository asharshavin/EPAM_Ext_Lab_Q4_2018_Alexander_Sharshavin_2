CREATE PROCEDURE dbo.GetAll
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [UserID], [Username], [DateOfCreation], [Role], [Password] FROM dbo.[Users]
END
