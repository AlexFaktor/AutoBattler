using Game.Core.Database.Records.Users;
using Game.Core.Dtos.UserDtos.Telegrams;
using Game.Core.Resources.Enums.Telegram;
using Game.Database.Service.Users;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Game.Telegram.Service
{
    public class UpdateHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandHandler _commandHandler;

        public UpdateHandler(IServiceProvider serviceProvider, CommandHandler commandHandler)
        {
            _serviceProvider = serviceProvider;
            _commandHandler = commandHandler;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            Log.Information($"Received update from user {update.Message?.From?.FirstName} with message: {update.Message?.Text}");

            if (update.Message?.From == null)
            {
                Log.Warning("Update.Message.From is null");
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var telegramService = scope.ServiceProvider.GetRequiredService<UTelegramRepository>();
            var statisticsService = scope.ServiceProvider.GetRequiredService<UStatisticsRepository>();

            var message = update.Message;
            var userTelegram = await telegramService.GetAsync(message.From.Id.ToString());

            if (userTelegram == null)
            {
                var userService = scope.ServiceProvider.GetRequiredService<UserRepository>();
                var user = await userService.AddAsync(new GameUser(
                    new UserTelegramCreateDto
                    {
                        TelegramId = message.From.Id.ToString(),
                        Username = message.From.FirstName,
                        TelegramUsername = message.From.Username,
                        FirstName = message.From.FirstName,
                        LastName = message.From.LastName,
                        Phone = message.Contact?.PhoneNumber,
                        Language = message.From.LanguageCode
                    }));

                await telegramService.ChangeStatus(message.From.Id.ToString(), ETelegramUserStatus.UserRegistration, 0);
                userTelegram = user?.Telegram;
            }

            if (userTelegram == null)
            {
                Log.Error("UserTelegram is still null after attempting to create it.");
                return;
            }

            await statisticsService.AddInteraction(userTelegram.UserId);

            if (userTelegram is null)
            {
                await _commandHandler.HandleCommand(client, message);
                return;
            }

            switch (userTelegram!.Status)
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
}
