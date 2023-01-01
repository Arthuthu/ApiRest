CREATE PROCEDURE [dbo].[spUsers_Create]
	@Username nvarchar(50),
	@Password nvarchar(50),
	@Role nvarchar(50),
	@PasswordHash varbinary(400),
	@PasswordSalt varbinary(400)


AS
BEGIN
	INSERT INTO dbo.[Users] (Username, Password, Role, PasswordHash, PasswordSalt)
	VALUES (@Username, @Password, @Role, @PasswordHash, @PasswordSalt);
END
