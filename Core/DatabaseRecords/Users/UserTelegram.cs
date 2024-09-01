using Core.Resources.Enums.Telegram;

namespace Core.DatabaseRecords.Users;

public class UserTelegram
{
    public Guid UserId { get; set; }
    public long TelegramId { get; set; }

    public TelegramUserStatuses Status { get; set; } = TelegramUserStatuses.UserRegistration;
    public short StatusLevel { get; set; } = 0;

    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}
