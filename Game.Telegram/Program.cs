﻿using Game.Core.Config;
using Game.Database.Service.Users;
using Game.Telegram.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Serilog;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace Game.Telegram;

internal class Program
{
    private static IHost _host;

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                var connectionString = configuration.GetConnectionString("DefaultConnection")!;

                services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(connectionString));

                services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(Config.telegram_token));
                services.AddTransient<UserRepository>(sp => new UserRepository(connectionString));
                services.AddTransient<UStatisticsRepository>(sp => new UStatisticsRepository(connectionString));
                services.AddTransient<UTelegramRepository>(sp => new UTelegramRepository(connectionString));
                services.AddTransient<UResourcesRepository>(sp => new UResourcesRepository(connectionString));
                services.AddSingleton<UpdateHandler>();
                services.AddSingleton<CommandHandler>();
            })
            .UseSerilog((context, config) =>
            {
                config.MinimumLevel.Warning()
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

        Log.Information("Starting bot...");
        await _host.RunAsync();
    }
}
