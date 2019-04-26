CREATE PROCEDURE dbo.SaveUser
	@Username varchar(50),
	@Role  int,
	@UserID int,
	@DateOfCreation datetime, 
	@Password varchar(50)
AS
BEGIN

	BEGIN TRAN
	IF @UserID != 0
	BEGIN
		UPDATE dbo.Users
		SET [Username] = @Username, Role = @Role, DateOfCreation = @DateOfCreation, Password = @Password
		WHERE UserID =  @UserID 
	END 

	IF @@ROWCOUNT = 0
	BEGIN
		IF @UserID != 0
		BEGIN
			SET IDENTITY_INSERT [dbo].[Users] ON
			INSERT dbo.[Users] ( [UserId], [Username],  [Role], DateOfCreation, [Password]) VALUES ( @UserId, @Username, @Role, @DateOfCreation, @Password)
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 
			SET IDENTITY_INSERT [dbo].[Users] OFF
	
		END
		ELSE
		BEGIN
			INSERT dbo.[Users] ( [Username], [Role], DateOfCreation, [Password]) VALUES ( @Username, @Role, @DateOfCreation, @Password)
	
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 

		END
	END
	ELSE
	BEGIN
		SELECT @UserID
	END

	COMMIT TRAN
END

