CREATE PROCEDURE [dbo].[spUsers_Update]
	@Id int,
	@Username nvarchar(50),
	@Password nvarchar(50),
	@Role nvarchar(50)
AS
BEGIN
	UPDATE dbo.[Users] SET Username = @Username, Password = @Password, Role = @Role	
	WHERE Id = @Id;
END
