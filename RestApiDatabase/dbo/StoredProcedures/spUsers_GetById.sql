CREATE PROCEDURE [dbo].[spUsers_GetById]
	@Id int
AS
BEGIN
	SELECT * FROM dbo.[Users]
	WHERE Id = @Id;
END
