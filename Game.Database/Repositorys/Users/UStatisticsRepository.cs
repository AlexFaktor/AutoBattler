using Dapper;
using Npgsql;
using System.Data;
using Game.Core.DatabaseRecords.Users;

namespace Game.Database.Service.Users;

public class UStatisticsRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public UStatisticsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<UserStatistics>> GetAllAsync()
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Statistics\"";
        var result = await connection.QueryAsync<UserStatistics>(query);
        connection.Close();
        return result.ToList();
    }

    public async Task<UserStatistics?> GetAsync(Guid userId)
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Statistics\" WHERE \"userId\" = @UserId";
        var result = await connection.QuerySingleOrDefaultAsync<UserStatistics>(query, new { UserId = userId });
        connection.Close();
        return result;
    }

    public async Task<UserStatistics?> UpdateAsync(Guid userId, UserStatistics updatedStatistics)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".""Statistics""
                SET ""dateOfUserRegistration"" = @DateOfUserRegistration,
                    ""numberInteractionsWithBot"" = @NumberInteractionsWithBot
                WHERE ""userId"" = @UserId
                RETURNING *";
        var result = await connection.QuerySingleOrDefaultAsync<UserStatistics>(query, new
        {
            updatedStatistics.DateOfUserRegistration,
            updatedStatistics.NumberInteractionsWithBot,
            UserId = userId
        });
        connection.Close();
        return result;
    }

    public async Task<bool> AddInteraction(Guid userId)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".""Statistics""
                SET ""numberInteractionsWithBot"" = ""numberInteractionsWithBot"" + 1
                WHERE ""userId"" = @UserId
                RETURNING 1";
        var result = await connection.ExecuteScalarAsync<int>(query, new { UserId = userId });
        connection.Close();
        return result == 1;
    }
}
