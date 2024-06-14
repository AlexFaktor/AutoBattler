using Game.Core.Config;
using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Telegram;
using Game.Database.Context;
using Game.Database.Service.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Game.Telegram
{
    internal class Program
    {
        private static IHost _host;

        static async Task Main(string[] args)
        {
            _host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting bot...");

            var bot = _host.Services.GetRequiredService<ITelegramBotClient>();

            var updates = await bot.GetUpdatesAsync();
            var lastUpdateId = updates.Length != 0 ? updates.Select(u => u.Id).Max() : 0;

            bot.StartReceiving(new DefaultUpdateHandler(Update, Error),
                new ReceiverOptions
                {
                    AllowedUpdates = new[] { UpdateType.Message },
                    Offset = lastUpdateId + 1
                });

            await _host.RunAsync();

            Console.WriteLine("Bot is running...");
            Console.ReadLine();
        }

        private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            using var scope = _host.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();
            var userService = scope.ServiceProvider.GetRequiredService<UserService>();
            var userTelegramService = scope.ServiceProvider.GetRequiredService<UserTelegramService>();

            var message = update.Message;
            if (message == null || message.From == null) return;

            var chatId = message.Chat.Id;
            var userId = message.From.Id;

            var userTelegram = await userTelegramService.GetAsync(userId.ToString());

            if (userTelegram == null)
            {
                var userRecord = new UserRecord();

                userTelegram = new UserTelegram
                {
                    TelegramId = userId.ToString(),
                    Status = ETelegramUserStatus.Default
                };

                userRecord.Telegram = userTelegram;

                await userService.AddAsync(userRecord);
            }

            switch (userTelegram.Status)
            {
                case ETelegramUserStatus.InputNewUsername:
                    await InputNewUsername(message, userTelegram, dbContext);
                    break;

                case ETelegramUserStatus.InputNewHashtag:
                    await InputNewHashtag(message, userTelegram, dbContext);
                    break;

                default:
                    await HandleCommand(message, userTelegram, dbContext);
                    break;
            }
        }

        private static async Task HandleCommand(Message message, UserTelegram userTelegram, GameDbContext dbContext)
        {
            var chatId = message.Chat.Id;

            if (message.Text!.StartsWith("/start"))
            {
                await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, "Welcome! Please enter your username (up to 16 characters):");
                userTelegram.Status = ETelegramUserStatus.InputNewUsername;
                await dbContext.SaveChangesAsync();
            }
            else if (message.Text.StartsWith("/setusername"))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userTelegram.UserId);
                await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, $"Your current username is: {user.Username}. Please enter your new username (up to 16 characters):");
                userTelegram.Status = ETelegramUserStatus.InputNewUsername;
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InputNewUsername(Message message, UserTelegram userTelegram, GameDbContext dbContext)
        {
            var chatId = message.Chat.Id;
            var newUsername = message.Text!.Trim();

            if (newUsername.Length > 16)
            {
                await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, "Username must be 16 characters or less. Please enter a valid username:");
                return;
            }

            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == newUsername);
            if (existingUser != null)
            {
                await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, "This username is already taken. Please enter a different username:");
                return;
            }

            userTelegram.Status = ETelegramUserStatus.InputNewHashtag;
            await dbContext.SaveChangesAsync();
            await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, "Please enter your hashtag (6 characters starting with #):");
        }

        private static async Task InputNewHashtag(Message message, UserTelegram userTelegram, GameDbContext dbContext)
        {
            var chatId = message.Chat.Id;
            var newHashtag = message.Text!.Trim();

            if (newHashtag.Length != 6 || !newHashtag.StartsWith("#") || !IsValidHashtag(newHashtag))
            {
                await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, "Invalid hashtag. It must be 6 characters, start with # and contain only alphanumeric characters. Please enter a valid hashtag:");
                return;
            }

            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Hashtag == newHashtag);
            if (existingUser != null)
            {
                await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, "This hashtag is already taken. Please enter a different hashtag:");
                return;
            }

            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userTelegram.UserId);
            if (user == null)
            {
                user = new UserRecord
                {
                    Id = Guid.NewGuid(),
                    Username = newHashtag.Substring(1),
                    Hashtag = newHashtag
                };
                dbContext.Users.Add(user);
            }
            else
            {
                user.Username = newHashtag.Substring(1);
                user.Hashtag = newHashtag;
            }

            userTelegram.Status = ETelegramUserStatus.Default;
            userTelegram.Username = message.Chat.Username;
            userTelegram.FirstName = message.Chat.FirstName;
            userTelegram.LastName = message.Chat.LastName;
            userTelegram.Phone = message.Contact?.PhoneNumber;
            userTelegram.Language = message.From?.LanguageCode;

            await dbContext.SaveChangesAsync();
            await _host.Services.GetRequiredService<ITelegramBotClient>().SendTextMessageAsync(chatId, "Your username and hashtag have been updated successfully.");
        }

        private static bool IsValidHashtag(string hashtag)
        {
            return hashtag.Skip(1).All(char.IsLetterOrDigit);
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

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<GameDbContext>(options =>
                        options.UseSqlServer(Config.database_connection_string));
                    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(Config.telegram_token));
                    services.AddTransient<UserService>();
                    services.AddTransient<UserTelegramService>();
                });
    }
}
