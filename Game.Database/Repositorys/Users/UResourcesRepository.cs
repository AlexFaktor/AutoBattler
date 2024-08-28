using Dapper;
using Npgsql;
using System.Data;
using App.Core.DatabaseRecords.Users;

namespace App.Database.Service.Users;

public class UResourcesRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public UResourcesRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<UserResources>> GetAllAsync()
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Resources\"";
        var result = await connection.QueryAsync<UserResources>(query);
        connection.Close();
        return result.ToList();
    }

    public async Task<UserResources?> GetAsync(Guid userId)
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Resources\" WHERE \"userId\" = @UserId";
        var result = await connection.QuerySingleOrDefaultAsync<UserResources>(query, new { UserId = userId });
        connection.Close();
        return result;
    }

    public async Task<UserResources?> UpdateAsync(Guid userId, UserResources updatedResources)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".""Resources""
                SET ""randomCoin"" = @RandomCoin,
                    ""fackoins"" = @Fackoins,
                    ""soulValue"" = @SoulValue,
                    ""energy"" = @Energy
                WHERE ""userId"" = @UserId
                RETURNING *";
        var result = await connection.QuerySingleOrDefaultAsync<UserResources>(query, new
        {
            updatedResources.RandomCoin,
            updatedResources.Fackoins,
            updatedResources.SoulValue,
            updatedResources.Energy,
            UserId = userId
        });
        connection.Close();
        return result;
    }
}
