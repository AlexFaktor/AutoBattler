using Game.Core.Database.Records.Users;
using Dapper;
using Npgsql;
using System.Data;

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
        var query = "SELECT * FROM user.Statistics";
        var result = await connection.QueryAsync<UserStatistics>(query);
        return result.ToList();
    }

    public async Task<UserStatistics?> GetAsync(Guid userId)
    {
        using var connection = Connection;
        var query = "SELECT * FROM user.Statistics WHERE userId = @UserId";
        var result = await connection.QuerySingleOrDefaultAsync<UserStatistics>(query, new { UserId = userId });
        return result;
    }

    public async Task<UserStatistics?> UpdateAsync(Guid userId, UserStatistics updatedStatistics)
    {
        using var connection = Connection;
        var query = @"
                UPDATE user.Statistics
                SET dateOfUserRegistration = @DateOfUserRegistration,
                    numberInteractionsWithBot = @NumberInteractionsWithBot
                WHERE userId = @UserId
                RETURNING *";
        var result = await connection.QuerySingleOrDefaultAsync<UserStatistics>(query, new
        {
            updatedStatistics.DateOfUserRegistration,
            updatedStatistics.NumberInteractionsWithBot,
            UserId = userId
        });
        return result;
    }

    public async Task<bool> AddInteraction(Guid userId)
    {
        using var connection = Connection;
        var query = @"
                UPDATE user.Statistics
                SET numberInteractionsWithBot = numberInteractionsWithBot + 1
                WHERE userId = @UserId
                RETURNING 1";
        var result = await connection.ExecuteScalarAsync<int>(query, new { UserId = userId });
        return result == 1;
    }
}
