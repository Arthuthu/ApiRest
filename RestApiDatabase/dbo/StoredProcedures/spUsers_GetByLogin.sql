CREATE PROCEDURE [dbo].[spUsers_GetByLogin]
	@Username nvarchar(50),
	@Password nvarchar(50)
AS
BEGIN
	SELECT * FROM dbo.[Users]
	WHERE Username = @Username and Password = @Password;
END

