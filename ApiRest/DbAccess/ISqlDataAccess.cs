namespace ApiRest.DbAccess
{
    public interface ISqlDataAccess
    {
        IEnumerable<T> LoadDataUnsynchronous<T, U>(string storedProcedure, U parameters, string connectionId = "RestApiDatabase");
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "RestApiDatabase");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "RestApiDatabase");
    }
}