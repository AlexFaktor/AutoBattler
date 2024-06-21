using Npgsql;
using System.Data;

namespace Game.Database.Repositorys.ScheduledTasks;

public class IndividuaTaskRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public IndividuaTaskRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // CRUD
}
