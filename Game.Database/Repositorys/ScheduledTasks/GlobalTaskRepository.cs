using Dapper;
using Game.Core.DatabaseRecords.ScheduledTask;
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

    public async Task<GlobalTask?> AddAsync(GlobalTask globalTask)
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            INSERT INTO ""scheduledTask"".""GlobalTasks"" (""id"",""type"",""frequencyInSeconds"",""callsNeeded"",""lastExecutionTime"")
            VALUES (@Id,@Type,@FrequencyInSeconds,@CallsNeeded,@LastExecutionTime)
            RETURNING *;
            ";

        var result = await connection.QuerySingleOrDefaultAsync<GlobalTask>(query, new
        {
            globalTask.Id,
            Type = (short)globalTask.Type,
            globalTask.FrequencyInSeconds,
            globalTask.CallsNeeded,
            globalTask.LastExecutionTime,
        });

        connection.Close();
        return result;
    }

    public async Task<GlobalTask?> GetAsync(Guid id)
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            SELECT * FROM ""scheduledTask"".""GlobalTasks""
            WHERE ""id"" = @Id;
            ";

        var result = await connection.QuerySingleOrDefaultAsync<GlobalTask>(query, new { Id = id });

        connection.Close();
        return result;
    }

    public async Task<List<GlobalTask>> GetAllAsync()
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            SELECT * FROM ""scheduledTask"".""GlobalTasks"";
            ";

        var result = await connection.QueryAsync<GlobalTask>(query);

        connection.Close();
        return result.ToList();
    }

    public async Task<GlobalTask?> UpdateAsync(Guid id, GlobalTask task)
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            UPDATE ""scheduledTask"".""GlobalTasks""
            SET ""type"" = @Type, 
                ""frequencyInSeconds"" = @FrequencyInSeconds, 
                ""callsNeeded"" = @CallsNeeded, 
                ""lastExecutionTime"" = @LastExecutionTime
            WHERE ""id"" = @Id
            RETURNING *;
            ";

        var result = await connection.QuerySingleOrDefaultAsync<GlobalTask>(query, new
        {
            Id = id,
            Type = (short)task.Type,
            task.FrequencyInSeconds,
            task.CallsNeeded,
            task.LastExecutionTime
        });

        connection.Close();
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            DELETE FROM ""scheduledTask"".""GlobalTasks""
            WHERE ""id"" = @Id;
            ";

        var result = await connection.ExecuteAsync(query, new { Id = id });

        connection.Close();
        return result > 0;
    }
}
