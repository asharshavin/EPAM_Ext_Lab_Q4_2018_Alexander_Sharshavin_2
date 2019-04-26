-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.GetAllUsers
	@Top int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP(@Top) [UserID], [Username], [DateOfCreation], [Role], [Password] FROM dbo.[Users]
END
