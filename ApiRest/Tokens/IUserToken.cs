namespace ApiRest.Tokens;

public interface IUserToken
{
    string CreateToken(UserModel user);
}