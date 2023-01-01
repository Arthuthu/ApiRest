namespace ApiRest.Models;

public class UserModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}
