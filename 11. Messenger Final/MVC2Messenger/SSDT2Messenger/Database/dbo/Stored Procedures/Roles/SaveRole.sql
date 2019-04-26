CREATE PROCEDURE dbo.SaveRole
	@Name varchar(50),
	@ID int
AS
BEGIN

	BEGIN TRAN
	IF @ID != 0
	BEGIN
		UPDATE dbo.Roles
		SET [Name] = @Name
		WHERE RoleID=  @ID 
	END 

	IF @@ROWCOUNT = 0
	BEGIN
		IF @ID != 0
		BEGIN
			SET IDENTITY_INSERT [dbo].Roles ON
			INSERT dbo.Roles ( [RoleId], [Name]) VALUES ( @ID, @Name)
			SELECT scope_identity()
			WHERE @@ROWCOUNT > 0 
			SET IDENTITY_INSERT [dbo].Roles OFF
	
		END
		ELSE
		BEGIN
			INSERT dbo.Roles ( Name) VALUES ( @Name)
	
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

