using Game.Core.Database.Records.Things;
using Game.Core.Dtos.UserDtos.Telegrams;
using System.Security.Cryptography;
using System.Text;

namespace Game.Core.Database.Records.Users;
public class GameUser
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Hashtag { get; set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;



    public UserTelegram Telegram { get; set; } = new();

    public UserResources Resources { get; set; } = new();
    public UserStatistics Statistics { get; set; } = new();

    public UserCamp Camp { get; set; } = new();

    public List<CharacterRecord> Characters { get; set; } = new();
    public List<ItemRecord> Items { get; set; } = new();


    public GameUser() { }

    public GameUser(UserTelegramCreateDto dto)
    {
        Username = dto.Username;
        Hashtag = dto.Hashtag;

        Telegram = new()
        {
            TelegramId = dto.TelegramId,
            Username = dto.TelegramUsername,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Phone = dto.Phone,
            Language = dto.Language,
        };
    }

    // Метод для хешування пароля
    private static string HashPassword(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}
