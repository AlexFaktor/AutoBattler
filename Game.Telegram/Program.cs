using Game.Core.Config;
using Game.Database.Context;
using Game.Database.Service.Users;
using Game.Telegram.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Logging;

namespace Game.Telegram
{
    internal class Program
    {
        private static IHost _host;

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<GameDbContext>(options =>
                        options.UseSqlServer(Config.database_connection_string));
                    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(Config.telegram_token));
                    services.AddTransient<UserService>();
                    services.AddTransient<UserStatisticsService>();
                    services.AddTransient<UserTelegramService>();
                    services.AddSingleton<UpdateHandler>();
                    services.AddSingleton<CommandHandler>();
                })
                .UseSerilog((context, config) =>
                {
                    config.MinimumLevel.Warning() // Установіть мінімальний рівень логування на Warning
                        .WriteTo.Console();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                });

        static async Task Main(string[] args)
        {
            _host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting bot...");

            var bot = _host.Services.GetRequiredService<ITelegramBotClient>();
            var updateHandler = _host.Services.GetRequiredService<UpdateHandler>();

            var updates = await bot.GetUpdatesAsync();
            var lastUpdateId = updates.Length != 0 ? updates.Select(u => u.Id).Max() : 0;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message],
                Offset = lastUpdateId + 1
            };

            bot.StartReceiving(
                new DefaultUpdateHandler(updateHandler.HandleUpdateAsync, updateHandler.HandleErrorAsync),
                receiverOptions
            );

            await _host.RunAsync();
        }
    }
}
