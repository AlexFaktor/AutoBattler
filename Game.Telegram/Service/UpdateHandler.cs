using Game.Core.Dtos.UserDtos.Telegrams;
using Game.Core.Resources.Enums.Telegram;
using Game.Database.Service.Users;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Game.Telegram.Service;

public class UpdateHandler
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CommandHandler _commandHandler;
    private readonly CallbackQueryHandler _callbackQueryHandler;

    private readonly BotAnswer _answer;

    private readonly UserRepository _userRepository;
    private readonly UTelegramRepository _telegramRepository;
    private readonly UStatisticsRepository _statisticsRepository;

    public UpdateHandler(IServiceProvider serviceProvider, CommandHandler commandHandler)
    {
        _serviceProvider = serviceProvider;
        _commandHandler = commandHandler;
        _callbackQueryHandler = new(serviceProvider);

        _answer = new(serviceProvider);

        var scope = _serviceProvider.CreateScope();
        _userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        _telegramRepository = scope.ServiceProvider.GetRequiredService<UTelegramRepository>();
        _statisticsRepository = scope.ServiceProvider.GetRequiredService<UStatisticsRepository>();
    }

    public async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        if (update.CallbackQuery != null)
        {
            Log.Information($"Received \"{update.CallbackQuery?.From.FirstName}\" callbackQuery: {update.CallbackQuery.Data}");

            await _callbackQueryHandler.HandleCallbackQueryAsync(bot, update.CallbackQuery);
            return;
        }
        if (update.Message?.From == null)
        {
            Log.Warning("Update.Message.From is null");
            return;
        }
        Log.Information($"Received \"{update.Message?.From?.FirstName}\" message: {update.Message?.Text}");

        var message = update.Message;
        var uTelegram = await _telegramRepository.GetAsync(message.From.Id);

        if (uTelegram == null)
        {
            var user = await _userRepository.AddAsync(
                new UserTelegramCreateDto
                {
                    TelegramId = message.From.Id,
                    Username = message.From.FirstName,
                    TelegramUsername = message.From.Username,
                    FirstName = message.From.FirstName,
                    LastName = message.From.LastName,
                    Phone = message.Contact?.PhoneNumber,
                    Language = message.From.LanguageCode
                });

            await _telegramRepository.ChangeStatus(message.From.Id, ETelegramUserStatus.UserRegistration, 0);
            uTelegram = await _telegramRepository.GetAsync(user!.Id);
        }
        await _statisticsRepository.AddInteraction(uTelegram.UserId);

        switch (uTelegram!.Status)
        {
            case ETelegramUserStatus.Default:
                await _commandHandler.HandleCommand(bot, message);
                break;
            case ETelegramUserStatus.UserRegistration:
                await _commandHandler.UserRegistration(bot, message);
                break;
            case ETelegramUserStatus.SetUsername:
                await _commandHandler.SetUsername(bot, message);
                break;
            case ETelegramUserStatus.SetHashtag:
                await _commandHandler.SetHashtag(bot, message);
                break;
            default:
                await _commandHandler.HandleCommand(bot, message);
                break;
        }
    }

    public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Log.Error(exception, "An error occurred");
        return Task.CompletedTask;
    }

    internal async Task HandleCallbackQueryAsync(ITelegramBotClient client, CallbackQuery? callbackQuery)
    {
        throw new NotImplementedException();
    }
}
