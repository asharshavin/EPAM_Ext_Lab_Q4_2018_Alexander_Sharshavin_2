CREATE PROCEDURE dbo.SaveChat
	@Name varchar(50),
	@ID int, 
	@DateOfCreation datetime
AS
BEGIN

	BEGIN TRAN
	IF @ID != 0
	BEGIN
		UPDATE dbo.Chats
		SET 
			[Name] = @Name, 
			DateOfCreation = @DateOfCreation
		WHERE ChatID =  @ID 
	END 

	IF @@ROWCOUNT = 0
	BEGIN
		IF @ID != 0
		BEGIN
			SET IDENTITY_INSERT [dbo].Chats ON
			INSERT dbo.Chats ( [ChatId], [Name], [DateOfCreation]) VALUES ( @ID, @Name, @DateOfCreation)
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 
			SET IDENTITY_INSERT [dbo].Chats OFF
	
		END
		ELSE
		BEGIN
			INSERT dbo.Chats ( [Name], [DateOfCreation]) VALUES ( @Name, @DateOfCreation)
	
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 

		END
	END
	ELSE
	BEGIN
		SELECT @ID
	END

	COMMIT TRAN
END

