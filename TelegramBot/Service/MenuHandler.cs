using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Service;

public class MenuHandler
{
    private readonly IServiceProvider _serviceProvider;

    public MenuHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleCommand(ITelegramBotClient bot, Message message)
    {
        switch (message.Text)
        {
            case "Back to main menu":
                await ShowMainMenu(bot, message);
                break;
            case "Play":
                await ShowPlayMenu(bot, message);
                break;
            case "Profile":
                await ShowPlayMenu(bot, message);
                break;
            case "Collection":
                await ShowPlayMenu(bot, message);
                break;
            case "Settings":
                await ShowPlayMenu(bot, message);
                break;
            case "Wiki":
                await ShowPlayMenu(bot, message);
                break;
            case "Global statistics":
                await ShowPlayMenu(bot, message);
                break;
            case "Action":
                await ShowPlayMenu(bot, message);
                break;
            case "Camp":
                await ShowPlayMenu(bot, message);
                break;
            case "Inventory":
                await ShowPlayMenu(bot, message);
                break;
            case "Characters":
                await ShowPlayMenu(bot, message);
                break;
            case "Friends":
                await ShowPlayMenu(bot, message);
                break;
            default:
                await ShowMainMenu(bot, message);
                break;
        }
    }

    public async Task ShowMainMenu(ITelegramBotClient bot, Message message)
    {
        var mainMenuKeyboard = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton[] { "Play" }, // Go to play menu
            new KeyboardButton[] { "Profile" }, // Go to msg profile
            new KeyboardButton[] { "Collection" }, // Go to collection menu
            new KeyboardButton[] { "Settings" }, // Go to msg settings
            new KeyboardButton[] { "Wiki" }, // Go to msg wiki
            new KeyboardButton[] { "Global statistics" } // go to msg global statistics
        })
        {
            ResizeKeyboard = true
        };

        await bot.SendTextMessageAsync(message.Chat.Id, "\u2063", replyMarkup: mainMenuKeyboard);
    }

    public async Task ShowPlayMenu(ITelegramBotClient bot, Message message)
    {
        var playMenuKeyboard = new ReplyKeyboardMarkup(new[]
        {
            new KeyboardButton[] { "Action" },
            new KeyboardButton[] { "Camp" },
            new KeyboardButton[] { "Inventory" },
            new KeyboardButton[] { "Characters" },
            new KeyboardButton[] { "Friends" },
            new KeyboardButton[] { "Back to main menu" }
        })
        { 
            ResizeKeyboard = true 
        };

        await bot.SendTextMessageAsync(message.Chat.Id, "\u2063", replyMarkup: playMenuKeyboard);
    }
}
