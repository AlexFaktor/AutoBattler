using Game.Core.Database.Records.Users;
using Dapper;
using Npgsql;
using System.Data;

namespace Game.Database.Service.Users;

public class UCampRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public UCampRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<UserCamp>> GetAllAsync()
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Camp\"";
        var result = await connection.QueryAsync<UserCamp>(query);
        connection.Close();
        return result.ToList();
    }

    public async Task<UserCamp?> GetAsync(Guid userId)
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Camp\" WHERE \"userId\" = @UserId";
        var result = await connection.QuerySingleOrDefaultAsync<UserCamp>(query, new { UserId = userId });
        connection.Close();
        return result;
    }

    public async Task<UserCamp?> UpdateAsync(Guid userId, UserCamp updatedCamp)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".Camp""
                SET ""name"" = @Name
                WHERE ""userId"" = @UserId
                RETURNING *";
        var result = await connection.QuerySingleOrDefaultAsync<UserCamp>(query, new
        {
            updatedCamp.Name,
            UserId = userId,
        });
        connection.Close();
        return result;
    }
}
