CREATE PROCEDURE dbo.SaveMessage
	@Text varchar(max),
	@ID int, 
	@ChatID int, 
	@UserID int, 
	@Date DateTime 
AS
BEGIN

	BEGIN TRAN
	IF @ID != 0
	BEGIN
		UPDATE dbo.Messages
		SET 
			[Text] = @Text, 
			[UserID] = @UserID,
			[ChatID] = @ChatID,
			[Date]  = @Date 
		WHERE MessageID =  @ID 
	END 

	IF @@ROWCOUNT = 0
	BEGIN
		IF @ID != 0
		BEGIN
			SET IDENTITY_INSERT [dbo].Messages ON
			INSERT dbo.Messages ( [MessageId], [Text], [UserID], [ChatID], [Date]) VALUES ( @ID, @Text, @UserID, @ChatID, @Date)
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 
			SET IDENTITY_INSERT [dbo].Messages OFF
	
		END
		ELSE
		BEGIN
			INSERT dbo.Messages ( [Text], [UserID], [ChatID], [Date] ) VALUES ( @Text, @UserID, @ChatID, @Date )
	
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

