﻿CREATE PROCEDURE dbo.DeleteMessage
	@ID int
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM dbo.Messages WHERE MessageID =  @ID
END