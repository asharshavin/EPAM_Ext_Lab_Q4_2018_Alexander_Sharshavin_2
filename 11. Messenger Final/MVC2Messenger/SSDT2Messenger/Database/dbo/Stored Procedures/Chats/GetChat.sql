﻿CREATE PROCEDURE dbo.GetChat
	@ID int
AS
BEGIN
	SELECT  ChatID, Name FROM dbo.Chats WHERE ChatID = @ID
END