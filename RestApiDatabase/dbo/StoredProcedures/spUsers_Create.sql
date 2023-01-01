CREATE PROCEDURE [dbo].[spUsers_Create]
	@Username nvarchar(50),
	@Password nvarchar(50),
	@Role nvarchar(50)

AS
BEGIN
	INSERT INTO dbo.[Users] (Username, Password, Role)
	VALUES (@Username, @Password, @Role);
END
