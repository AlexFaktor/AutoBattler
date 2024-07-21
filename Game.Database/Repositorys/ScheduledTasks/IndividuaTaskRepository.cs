using Dapper;
using App.Core.DatabaseRecords.ScheduledTask;
using Npgsql;
using System.Data;

namespace App.Database.Repositorys.ScheduledTasks;

public class IndividuaTaskRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public IndividuaTaskRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IndividualTask?> AddAsync(IndividualTask individualTask)
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            INSERT INTO ""scheduledTask"".""IndividualTasks"" (""id"",""userId"",""type"",""frequencyInSeconds"",""callsNeeded"",""lastExecutionTime"")
            VALUES (@Id,@UserId,@Type,@FrequencyInSeconds,@CallsNeeded,@LastExecutionTime)
            RETURNING *;
            ";

        var result = await connection.QuerySingleOrDefaultAsync<IndividualTask>(query, new
        {
            individualTask.Id,
            individualTask.UserId,
            Type = (short)individualTask.Type,
            individualTask.FrequencyInSeconds,
            individualTask.CallsNeeded,
            individualTask.LastExecutionTime,
        });

        connection.Close();
        return result;
    }

    public async Task<IndividualTask?> GetAsync(Guid id)
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            SELECT * FROM ""scheduledTask"".""IndividualTasks""
            WHERE ""id"" = @Id;
            ";

        var result = await connection.QuerySingleOrDefaultAsync<IndividualTask>(query, new { Id = id });

        connection.Close();
        return result;
    }

    public async Task<List<IndividualTask>> GetAllAsync()
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            SELECT * FROM ""scheduledTask"".""IndividualTasks"";
            ";

        var result = await connection.QueryAsync<IndividualTask>(query);

        connection.Close();
        return result.ToList();
    }

    public async Task<IndividualTask?> UpdateAsync(Guid id, IndividualTask task)
    {
        using var connection = Connection;
        connection.Open();

        var query = @"
            UPDATE ""scheduledTask"".""IndividualTasks""
            SET ""type"" = @Type,
                ""frequencyInSeconds"" = @FrequencyInSeconds, 
                ""callsNeeded"" = @CallsNeeded, 
                ""lastExecutionTime"" = @LastExecutionTime
            WHERE ""id"" = @Id
            RETURNING *;
            ";

        var result = await connection.QuerySingleOrDefaultAsync<IndividualTask>(query, new
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
            DELETE FROM ""scheduledTask"".""IndividualTasks""
            WHERE ""id"" = @Id;
            ";

        var result = await connection.ExecuteAsync(query, new { Id = id });

        connection.Close();
        return result > 0;
    }
}
