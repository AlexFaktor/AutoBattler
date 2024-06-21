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

    private readonly UserRepository _userRepository;
    private readonly UTelegramRepository _telegramRepository;
    private readonly UStatisticsRepository _statisticsRepository;

    public UpdateHandler(IServiceProvider serviceProvider, CommandHandler commandHandler)
    {
        _serviceProvider = serviceProvider;
        _commandHandler = commandHandler;

        var scope = _serviceProvider.CreateScope();
        _userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        _telegramRepository = scope.ServiceProvider.GetRequiredService<UTelegramRepository>();
        _statisticsRepository = scope.ServiceProvider.GetRequiredService<UStatisticsRepository>();
    }

    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
    {
        Log.Information($"Received update from user {update.Message?.From?.FirstName} with message: {update.Message?.Text}");

        if (update.Message?.From == null)
        {
            Log.Warning("Update.Message.From is null");
            return;
        }

        var message = update.Message;
        var uTelegram = await _telegramRepository.GetAsync(message.From.Id.ToString());

        if (uTelegram == null)
        {
            var user = await _userRepository.AddAsync(
                new UserTelegramCreateDto
                {
                    TelegramId = message.From.Id.ToString(),
                    Username = message.From.FirstName,
                    TelegramUsername = message.From.Username,
                    FirstName = message.From.FirstName,
                    LastName = message.From.LastName,
                    Phone = message.Contact?.PhoneNumber,
                    Language = message.From.LanguageCode
                });

            await _telegramRepository.ChangeStatus(message.From.Id.ToString(), ETelegramUserStatus.UserRegistration, 0);
            uTelegram = await _telegramRepository.GetAsync(user!.Id);
        }

        if (uTelegram == null)
        {
            Log.Error("UserTelegram is still null after attempting to create it.");
            return;
        }

        await _statisticsRepository.AddInteraction(uTelegram.UserId);

        if (uTelegram is null)
        {
            await _commandHandler.HandleCommand(client, message);
            return;
        }

        switch (uTelegram!.Status)
        {
            case ETelegramUserStatus.UserRegistration:
                await _commandHandler.UserRegistration(client, message);
                break;
            case ETelegramUserStatus.SetUsername:
                await _commandHandler.SetUsername(client, message);
                break;
            case ETelegramUserStatus.SetHashtag:
                await _commandHandler.SetHashtag(client, message);
                break;
            default:
                await _commandHandler.HandleCommand(client, message);
                break;
        }
    }

    public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Log.Error(exception, "An error occurred");
        return Task.CompletedTask;
    }
}
