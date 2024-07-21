using App.Core.Dtos.UserDtos.Telegrams;
using App.Core.Resources.Enums.Telegram;
using Dapper;
using Npgsql;
using System.Data;
using App.Core.DatabaseRecords.Users;

namespace App.Database.Service.Users;

public class UTelegramRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public UTelegramRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<UserTelegram>> GetAllAsync()
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Telegram\"";
        var result = await connection.QueryAsync<UserTelegram>(query);
        connection.Close();
        return result.ToList();
    }

    public async Task<UserTelegram?> GetAsync(Guid userId)
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Telegram\" WHERE \"userId\" = @UserId";
        var result = await connection.QuerySingleOrDefaultAsync<UserTelegram>(query, new { UserId = userId });
        connection.Close();
        return result;
    }

    public async Task<UserTelegram?> GetAsync(long telegramId)
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Telegram\" WHERE \"telegramId\" = @TelegramId";
        var result = await connection.QuerySingleOrDefaultAsync<UserTelegram>(query, new { TelegramId = telegramId });
        connection.Close();
        return result;
    }

    public async Task<UserTelegram?> UpdateAsync(Guid userId, UserTelegram updatedTelegram)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".""Telegram""
                SET ""telegramId"" = @TelegramId,
                    ""username"" = @Username,
                    ""firstName"" = @FirstName,
                    ""lastName"" = @LastName,
                    ""phone"" = @Phone,
                    ""language"" = @Language
                WHERE ""userId"" = @UserId
                RETURNING *";
        var result = await connection.QuerySingleOrDefaultAsync<UserTelegram>(query, new
        {
            updatedTelegram.TelegramId,
            updatedTelegram.Username,
            updatedTelegram.FirstName,
            updatedTelegram.LastName,
            updatedTelegram.Phone,
            updatedTelegram.Language,
            UserId = userId
        });
        connection.Close();
        return result;
    }

    public async Task<UserTelegram?> UpdateAsync(long telegramId, UserTelegramUpdateDto dto)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".""Telegram""
                SET ""username"" = @Username,
                    ""firstName"" = @FirstName,
                    ""lastName"" = @LastName,
                    ""phone"" = @Phone,
                    ""language"" = @Language
                WHERE ""telegramId"" = @TelegramId
                RETURNING *";
        var result = await connection.QuerySingleOrDefaultAsync<UserTelegram>(query, new
        {
            dto.Username,
            dto.FirstName,
            dto.LastName,
            dto.Phone,
            dto.Language,
            TelegramId = telegramId
        });
        connection.Close();
        return result;
    }

    public async Task<(ETelegramUserStatus, short)> GetStatus(long telegramId)
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT \"status\", \"statusLevel\" FROM \"user\".\"Telegram\" WHERE \"telegramId\" = @TelegramId";
        var result = await connection.QuerySingleOrDefaultAsync<(ETelegramUserStatus, short)>(query, new { TelegramId = telegramId });
        connection.Close();
        return result;
    }

    public async Task<bool> ChangeStatus(long telegramId, ETelegramUserStatus status, short statusLvl)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".""Telegram""
                SET ""status"" = @Status,
                    ""statusLevel"" = @StatusLevel
                WHERE ""telegramId"" = @TelegramId";
        var rowsAffected = await connection.ExecuteAsync(query, new
        {
            Status = (int)status,
            StatusLevel = statusLvl,
            TelegramId = telegramId
        });
        connection.Close();
        return rowsAffected > 0;
    }
}
