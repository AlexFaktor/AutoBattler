using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Service.Keyboards;

internal class InlineKeyboards
{
    public static InlineKeyboardMarkup GetProfile()
    {
        return new(new[]
    {
        [InlineKeyboardButton.WithCallbackData("Info", "profile_info")],
        [InlineKeyboardButton.WithCallbackData("Statistics", "profile_stats")],
        [InlineKeyboardButton.WithCallbackData("Resources", "profile_resources")],
        new[] { InlineKeyboardButton.WithCallbackData("Possessions", "profile_possessions") },
    });
    }
}
