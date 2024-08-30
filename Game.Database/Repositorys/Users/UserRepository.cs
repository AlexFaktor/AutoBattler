using Dapper;
using App.Core.DatabaseRecords.Users;
using App.Core.Dtos.UserDtos.Telegrams;
using Npgsql;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace App.Database.Service.Users;

public class UserRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<GameUser?> AddAsync(UserTelegramCreateDto userDto)
    {
        using var connection = Connection;
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            var userId = Guid.NewGuid();
            var insertUserQuery = @"
                    INSERT INTO ""user"".""Users"" (""id"", ""username"", ""hashtag"", ""password_hash"")
                    VALUES (@Id, @Username, @Hashtag, @PasswordHash)
                    RETURNING *;
                ";
            var user = new GameUser
            {
                Id = userId,
                Username = userDto.Username,
                Hashtag = userDto.Hashtag,
                PasswordHash = HashPassword("default_password") // Замість цього додайте реальну логіку для хешування пароля
            };
            await connection.ExecuteAsync(insertUserQuery, user, transaction);

            // Вставка в таблицю user.Telegram
            var insertTelegramQuery = @"
                    INSERT INTO ""user"".""Telegram"" (""userId"", ""telegramId"", ""username"", ""firstName"", ""lastName"", ""phone"", ""language"")
                    VALUES (@UserId, @TelegramId, @Username, @FirstName, @LastName, @Phone, @Language);
                ";
            var userTelegram = new UserTelegram
            {
                UserId = userId,
                TelegramId = userDto.TelegramId,
                Username = userDto.TelegramUsername,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Phone = userDto.Phone,
                Language = userDto.Language
            };
            await connection.ExecuteAsync(insertTelegramQuery, userTelegram, transaction);

            // Вставка в таблицю user.Resources
            var insertResourcesQuery = @"
                    INSERT INTO ""user"".""Resources"" (""userId"", ""randomCoin"", ""fackoins"", ""soulValue"", ""energy"")
                    VALUES (@UserId, @RandomCoin, @Fackoins, @SoulValue, @Energy);
                ";
            var userResources = new UserResources
            {
                UserId = userId,
                RandomCoin = 0,
                Fackoins = 0,
                SoulValue = 0,
                Energy = 0
            };
            await connection.ExecuteAsync(insertResourcesQuery, userResources, transaction);

            // Вставка в таблицю user.Statistics
            var insertStatisticsQuery = @"
                    INSERT INTO ""user"".""Statistics"" (""userId"", ""dateOfUserRegistration"", ""numberInteractionsWithBot"")
                    VALUES (@UserId, @DateOfUserRegistration, @NumberInteractionsWithBot);
                ";
            var userStatistics = new UserStatistics
            {
                UserId = userId,
                DateOfUserRegistration = DateTime.Now,
                NumberInteractionsWithBot = 0
            };
            await connection.ExecuteAsync(insertStatisticsQuery, userStatistics, transaction);

            transaction.Commit();
            connection.Close();
            return user;
        }
        catch
        {
            transaction.Rollback();
            connection.Close();
            throw;
        }
    }

    public async Task<List<GameUser>> GetAllAsync()
    {
        using var connection = Connection;
        connection.Open();
        var query = "SELECT * FROM \"user\".\"Users\";";
        var users = await connection.QueryAsync<GameUser>(query);
        connection.Close();
        return users.ToList();
    }

    public async Task<GameUser?> GetAsync(Guid id)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                SELECT * FROM ""user"".""Users"" WHERE id = @Id;
                -- Add more queries to include related entities if necessary
            ";
        var user = await connection.QueryFirstOrDefaultAsync<GameUser>(query, new { Id = id });
        connection.Close();
        return user;
    }

    public async Task<GameUser?> GetAsync(string username, string hashtag)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                SELECT *
                FROM ""user"".""Users""
                WHERE username = @Username AND hashtag = @Hashtag
            ";
        var user = await connection.QueryFirstOrDefaultAsync<GameUser>(query, new
        {
            Username = username,
            Hashtag = hashtag
        });
        connection.Close();
        return user;
    }

    public async Task<GameUser?> UpdateAsync(Guid id, GameUser updatedUser)
    {
        using var connection = Connection;
        connection.Open();
        var query = @"
                UPDATE ""user"".""Users"" SET
                    ""username"" = @Username,
                    ""hashtag"" = @Hashtag,
                    ""password_hash"" = @PasswordHash
                WHERE id = @Id
                RETURNING *;
            ";
        var result = await connection.QuerySingleOrDefaultAsync<GameUser>(query, new
        {
            Id = id,
            updatedUser.Username,
            updatedUser.Hashtag,
            updatedUser.PasswordHash
        });
        connection.Close();
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = Connection;
        connection.Open();
        var query = "DELETE FROM \"user\".\"Telegram\" WHERE \"userId\" = @Id;" +
                    "DELETE FROM \"user\".\"Statistics\" WHERE \"userId\" = @Id;" +
                    "DELETE FROM \"user\".\"Resources\" WHERE \"userId\" = @Id;" +
                    "DELETE FROM \"user\".\"Camp\" WHERE \"userId\" = @Id;" +
                    "DELETE FROM \"user\".\"Users\" WHERE id = @Id;" +
                    "";
        var result = await connection.ExecuteAsync(query, new { Id = id });
        connection.Close();
        return result > 0;
    }

    private string HashPassword(string password)
    {
        // Реальна логіка хешування пароля
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}
