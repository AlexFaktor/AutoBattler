using Game.Database.Repositorys.Things;
using Game.Database.Service.Users;
using Game.Telegram.Service.Keyboards;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Game.Telegram.Service;

public class CallbackQueryHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly MenuHandler _menuHandler;

    private readonly UserRepository _userRepository;
    private readonly UTelegramRepository _telegramRepository;
    private readonly UStatisticsRepository _statisticsRepository;
    private readonly UResourcesRepository _resourcesRepository;

    private readonly UCharacterRepository _characterRepository;
    private readonly UItemRepository _itemRepository;

    public CallbackQueryHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _menuHandler = new(serviceProvider);

        var scope = _serviceProvider.CreateScope();
        _userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        _telegramRepository = scope.ServiceProvider.GetRequiredService<UTelegramRepository>();
        _statisticsRepository = scope.ServiceProvider.GetRequiredService<UStatisticsRepository>();
        _resourcesRepository = scope.ServiceProvider.GetRequiredService<UResourcesRepository>();
        _characterRepository = scope.ServiceProvider.GetRequiredService<UCharacterRepository>();
        _itemRepository = scope.ServiceProvider.GetRequiredService<UItemRepository>();
    }
    public async Task HandleCallbackQueryAsync(ITelegramBotClient bot, CallbackQuery callbackQuery)
    {
        if (callbackQuery?.Data == null) return;

        var chatId = callbackQuery.Message!.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        var callbackData = callbackQuery.Data;

        try
        {
            switch (callbackData)
            {
                case "profile_info":
                    await EditToProfileInfo(bot, callbackQuery);
                    break;
                case "profile_stats":
                    await EditToProfileStats(bot, callbackQuery);
                    break;
                case "profile_resources":
                    await EditToProfileResources(bot, callbackQuery);
                    break;
                case "profile_possessions":
                    await EditToProfilePossessions(bot, callbackQuery);
                    break;
                default:
                    await bot.AnswerCallbackQueryAsync(callbackQuery.Id, "Невідома команда.");
                    break;
            }


            // Відправка повідомлення про обробку callback
            await bot.AnswerCallbackQueryAsync(callbackQuery.Id);
        }
        catch (Exception ex)
        {
            Log.Information($"{ex.Message}");
        }
    }

    private async Task EditToProfileInfo(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        var chatId = callbackQuery.Message!.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        var telegramId = callbackQuery.From.Id;

        var uTelegram = await _telegramRepository.GetAsync(telegramId);
        var user = await _userRepository.GetAsync(uTelegram!.UserId);

        var text = $"Profile Info\n" +
                   $"Username - {user.Username}\n" +
                   $"Hashtag - {user.Hashtag}\n";

        await client.EditMessageTextAsync(chatId, messageId, text, replyMarkup: InlineKeyboards.GetProfile());
    }

    private async Task EditToProfileStats(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        var chatId = callbackQuery.Message!.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        var telegramId = callbackQuery.From.Id;

        var uTelegram = await _telegramRepository.GetAsync(telegramId);
        var uStatistics = await _statisticsRepository.GetAsync(uTelegram!.UserId);

        var text = $"Your statistics\n" +
                   $"Date registration - {uStatistics.DateOfUserRegistration}\n" +
                   $"Interact with the bot - {uStatistics.NumberInteractionsWithBot}\n";

        await client.EditMessageTextAsync(chatId, messageId, text, replyMarkup: InlineKeyboards.GetProfile());
    }

    private async Task EditToProfileResources(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        var chatId = callbackQuery.Message!.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        var telegramId = callbackQuery.From.Id;

        var uTelegram = await _telegramRepository.GetAsync(telegramId);
        var uResources = await _resourcesRepository.GetAsync(uTelegram!.UserId);

        var text = $"Your resources\n" +
                   $"Energy - {uResources.Energy}\n" +
                   $"Fackoins - {uResources.Fackoins}\n" +
                   $"Soul Value - {uResources.SoulValue}\n" +
                   $"Random Coin - {uResources.RandomCoin}\n" +
                   $"";

        await client.EditMessageTextAsync(chatId, messageId, text, replyMarkup: InlineKeyboards.GetProfile());
    }

    private async Task EditToProfilePossessions(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        var chatId = callbackQuery.Message!.Chat.Id;
        var messageId = callbackQuery.Message.MessageId;
        var telegramId = callbackQuery.From.Id;

        var uTelegram = await _telegramRepository.GetAsync(telegramId);

        var uCharacters = await _characterRepository.GetCharactersAsync(uTelegram!.UserId);
        var uItems = await _itemRepository.GetItemsAsync(uTelegram!.UserId);

        var text = $"Your ownership\n" +
                   $"Сharacters - {uCharacters.Count}\n" +
                   $"Items - {uItems.Count}\n" +
                   $"";

        await client.EditMessageTextAsync(chatId, messageId, text, replyMarkup: InlineKeyboards.GetProfile());
    }
}

