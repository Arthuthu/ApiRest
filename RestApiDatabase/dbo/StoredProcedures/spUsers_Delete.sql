CREATE PROCEDURE [dbo].[spUsers_Delete]
	@Id int
AS
BEGIN
	DELETE FROM dbo.[Users]
	WHERE Id = @Id;
END
