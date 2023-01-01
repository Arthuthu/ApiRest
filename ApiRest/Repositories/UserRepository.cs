using ApiRest.DbAccess;
using ApiRest.Dto;

namespace ApiRest.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ISqlDataAccess _db;

    public UserRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetAllUsers()
    {
        return _db.LoadData<UserModel, dynamic>("dbo.spUsers_GetAll", new { });
    }

    public UserModel GetUserById(int id)
    {
        var results = _db.LoadDataUnsynchronous<UserModel, dynamic>(
            "dbo.spUsers_GetById",
            new { Id = id });

        return results.FirstOrDefault();
    }

    public Task CreateUser(UserModel user)
    {
        return _db.SaveData("dbo.spUsers_Create", new { 
            user.Username,
            user.Password,
            user.Role,
            user.PasswordHash,
            user.PasswordSalt});
    }

    public Task UpdateUser(UserModel user)
    {
        return _db.SaveData("dbo.spUsers_Update", user);
    }

    public Task DeleteUser(int id)
    {
        return _db.SaveData("dbo.spUsers_Delete", new { Id = id });
    }
}
