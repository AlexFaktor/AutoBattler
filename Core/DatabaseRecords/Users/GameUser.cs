using Core.Dtos.UserDtos.Telegrams;

namespace Core.DatabaseRecords.Users;
public class GameUser
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Hashtag { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public GameUser() { }

    public GameUser(UserTelegramCreateDto dto)
    {
        Username = dto.Username;
        Hashtag = dto.Hashtag;
    }
}
