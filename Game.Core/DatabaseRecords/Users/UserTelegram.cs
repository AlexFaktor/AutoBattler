﻿using Game.Core.Resources.Enums.Telegram;

namespace Game.Core.DatabaseRecords.Users;

public class UserTelegram
{
    public Guid UserId { get; set; }
    public long TelegramId { get; set; }

    public ETelegramUserStatus Status { get; set; } = ETelegramUserStatus.UserRegistration;
    public short StatusLevel { get; set; } = 0;

    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}