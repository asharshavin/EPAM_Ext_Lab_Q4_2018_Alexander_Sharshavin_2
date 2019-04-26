CREATE PROCEDURE dbo.SaveChatUser
	@ChatID int, 
	@UserID int 
AS
BEGIN

	BEGIN TRAN
	IF @ChatID != 0 AND @UserID != 0 
	BEGIN
		UPDATE dbo.ChatUsers
		SET 
			[UserID] = @UserID,
			[ChatID] = @ChatID
		WHERE ChatID=  @ChatID AND UserID = @UserID
	END 

	IF @@ROWCOUNT = 0
	BEGIN
		IF @ChatID != 0 AND @UserID != 0
		BEGIN
			INSERT dbo.ChatUsers ( [UserID], [ChatID]) VALUES ( @UserID, @ChatID)
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 
		END
		ELSE
		BEGIN
			INSERT dbo.ChatUsers ( [UserID], [ChatID]) VALUES ( @UserID, @ChatID)
	
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 

		END
	END
	ELSE
	BEGIN
		SELECT @ChatID
	END

	COMMIT TRAN
END

