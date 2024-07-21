using Telegram.Bot.Types.ReplyMarkups;

namespace App.Telegram.Service.Keyboards
{
    internal class InlineKeyboards
    {
        public static InlineKeyboardMarkup GetProfile()
        {
            return new(new[]
        {
            new[] { InlineKeyboardButton.WithCallbackData("Info", "profile_info") },
            new[] { InlineKeyboardButton.WithCallbackData("Statistics", "profile_stats") },
            new[] { InlineKeyboardButton.WithCallbackData("Resources", "profile_resources") },
            new[] { InlineKeyboardButton.WithCallbackData("Possessions", "profile_possessions") },
        });
        }
    }
}
