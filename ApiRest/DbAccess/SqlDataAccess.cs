namespace ApiRest.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public IEnumerable<T> LoadDataUnsynchronous<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "RestApiDatabase")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        return connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    //Async Methods

    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "RestApiDatabase")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        string connectionId = "RestApiDatabase")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
