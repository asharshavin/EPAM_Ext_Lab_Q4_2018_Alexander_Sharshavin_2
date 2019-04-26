-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.DeleteUser
	@UserID int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM dbo.[Users] WHERE UserID =  @UserID
END