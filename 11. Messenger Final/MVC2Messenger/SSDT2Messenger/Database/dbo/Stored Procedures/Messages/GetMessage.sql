CREATE PROCEDURE dbo.GetMessage
	@ID int
AS
BEGIN
	SELECT  [MessageID], [Text], [UserID], [ChatID], [Date]  FROM dbo.Messages WHERE MessageID = @ID
END