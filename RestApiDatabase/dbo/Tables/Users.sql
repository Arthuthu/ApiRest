CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Role] NVARCHAR(50) NOT NULL, 
    [PasswordHash] VARBINARY(400) NULL, 
    [PasswordSalt] VARBINARY(400) NULL,

)
