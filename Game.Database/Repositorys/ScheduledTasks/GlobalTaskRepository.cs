using Npgsql;
using System.Data;

namespace Game.Database.Repositorys.ScheduledTasks;

public class GlobalTaskRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public GlobalTaskRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // CRUD
}
