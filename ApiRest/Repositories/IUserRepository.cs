namespace ApiRest.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(UserModel user);
        Task DeleteUser(int id);
        Task<IEnumerable<UserModel>> GetAllUsers();
        UserModel GetUserById(int id);
        Task UpdateUser(UserModel user);

        UserModel GetUserByLogin(UserDto user);
    }
}