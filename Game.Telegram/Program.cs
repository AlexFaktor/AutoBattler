using Game.Core.Config;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Game.Telegram
{
    internal class Program
    {
        private static readonly ITelegramBotClient Bot = new TelegramBotClient(Config.telegram_token);

        static async Task Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting bot...");

            // Get the latest update
            var updates = await Bot.GetUpdatesAsync();
            var lastUpdateId = updates.Length != 0 ? updates.Select(u => u.Id).Max() : 0;

            // Start receiving from the next update
            Bot.StartReceiving(new DefaultUpdateHandler(Update, Error),
                new ReceiverOptions
                {
                    AllowedUpdates = [UpdateType.Message],
                    Offset = lastUpdateId + 1
                });
            Console.ReadKey();

            Console.WriteLine("Bot is running...");
            Console.ReadLine();
        }

        private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;
            var chatId = message!.Chat.Id;
            using CancellationTokenSource cts = new();

            if (message?.Text is null)
            {
                await Bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Text is null",
                    cancellationToken: cts.Token);
                cts.Cancel();
                return;
            }

            if (message?.Text.Trim()[0] == '/')
            {

            }
        }

        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            try
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while handling an error: {ex.Message}");
            }
        }
    }
}
