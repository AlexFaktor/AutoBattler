using Dapper;
using Core.DatabaseRecords.Things;
using Npgsql;
using System.Data;

namespace Database.Repositorys.Things;

public class UItemRepository
{
    private readonly string _connectionString;
    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public UItemRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<ItemRecord?> AddAsync(ItemRecord item)
    {
        using var connection = Connection;
        var query = @"
                INSERT INTO ""things"".""Items"" (""id"", ""userId"", ""itemId"")
                VALUES (@Id, @UserId, @ItemId)
                RETURNING *;
            ";
        return await connection.QuerySingleOrDefaultAsync<ItemRecord>(query, item);
    }

    public async Task<List<ItemRecord>> GetAllAsync()
    {
        using var connection = Connection;
        var query = "SELECT * FROM \"things\".\"Items\"";
        var result = await connection.QueryAsync<ItemRecord>(query);
        return result.AsList();
    }

    public async Task<List<ItemRecord>> GetItemsAsync(Guid userId)
    {
        using var connection = Connection;
        var query = "SELECT * FROM \"things\".\"Items\" WHERE \"userId\" = @UserId";
        var result = await connection.QueryAsync<ItemRecord>(query, new { UserId = userId });
        return result.AsList();
    }

    public async Task<ItemRecord?> GetItemAsync(Guid id)
    {
        using var connection = Connection;
        var query = "SELECT * FROM \"things\".\"Items\" WHERE \"id\" = @Id";
        return await connection.QuerySingleOrDefaultAsync<ItemRecord>(query, new { Id = id });
    }

    public async Task<ItemRecord?> UpdateAsync(Guid id, ItemRecord item)
    {
        using var connection = Connection;
        var query = @"
                UPDATE ""things"".""Items""
                SET ""user_id"" = @UserId, ""itemId"" = @ItemId
                WHERE ""id"" = @Id
                RETURNING *;
            ";
        return await connection.QuerySingleOrDefaultAsync<ItemRecord>(query, new
        {
            item.UserId,
            ItemId = (int)item.ItemId,
            Id = id
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = Connection;
        var query = "DELETE FROM \"things\".\"Items\" WHERE \"id\" = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }
}
