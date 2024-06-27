using Dapper;
using Game.Core.DatabaseRecords.Things;
using Npgsql;
using System.Data;

namespace Game.Database.Repositorys.Things;

public class UCharacterRepository
{
    private readonly string _connectionString;
    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public UCharacterRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<CharacterRecord?> AddAsync(ItemRecord character)
    {
        using var connection = Connection;
        var query = @"
                INSERT INTO ""things"".""Characters"" (""id"", ""userId"", ""characterId"")
                VALUES (@Id, @UserId, @CharacterId)
                RETURNING *;
            ";
        return await connection.QuerySingleOrDefaultAsync<CharacterRecord>(query, character);
    }

    public async Task<List<CharacterRecord>> GetAllAsync()
    {
        using var connection = Connection;
        var query = "SELECT * FROM \"things\".\"Characters\"";
        var result = await connection.QueryAsync<CharacterRecord>(query);
        return result.AsList();
    }

    public async Task<List<CharacterRecord>> GetCharactersAsync(Guid userId)
    {
        using var connection = Connection;
        var query = "SELECT * FROM \"things\".\"Characters\" WHERE \"userId\" = @UserId";
        var result = await connection.QueryAsync<CharacterRecord>(query, new { UserId = userId });
        return result.AsList();
    }

    public async Task<CharacterRecord?> GetItemAsync(Guid id)
    {
        using var connection = Connection;
        var query = "SELECT * FROM \"things\".\"Characters\" WHERE \"id\" = @Id";
        return await connection.QuerySingleOrDefaultAsync<CharacterRecord>(query, new { Id = id });
    }

    public async Task<CharacterRecord?> UpdateAsync(Guid id, CharacterRecord character)
    {
        using var connection = Connection;
        var query = @"
                UPDATE ""things"".""Characters""
                SET ""user_id"" = @UserId, ""itemId"" = @CharacterId
                WHERE ""id"" = @Id
                RETURNING *;
            ";
        return await connection.QuerySingleOrDefaultAsync<CharacterRecord>(query, new
        {
            character.UserId,
            ItemId = (int)character.CharacterId,
            Id = id
        });
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = Connection;
        var query = "DELETE FROM \"things\".\"Characters\" WHERE \"id\" = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }
}
