using Game.Core.Dtos.UserDtos.Telegrams;
using Game.Core.Resources.Enums.Telegram;
using Game.Database.Service.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Game.Telegram.Service;

public class CommandHandler
{
    private readonly IServiceProvider _serviceProvider;

    private readonly UserRepository _userRepository;
    private readonly UTelegramRepository _telegramRepository;
    private readonly UStatisticsRepository _statisticsRepository;
    private readonly UResourcesRepository _resourcesRepository;

    public CommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var scope = _serviceProvider.CreateScope();
        _userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
        _telegramRepository = scope.ServiceProvider.GetRequiredService<UTelegramRepository>();
        _statisticsRepository = scope.ServiceProvider.GetRequiredService<UStatisticsRepository>();
        _resourcesRepository = scope.ServiceProvider.GetRequiredService<UResourcesRepository>();
    }

    public async Task HandleCommand(ITelegramBotClient bot, Message message)
    {
        var userTelegram = await _telegramRepository.GetAsync(message.From!.Id.ToString());

        if (userTelegram == null) return;
        var user = await _userRepository.GetAsync(userTelegram.UserId);
        if (message.Text == null) return;

        if (message.Text.StartsWith("/start"))
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Your profile is already in the database. Let's update your information...");

            var dto = new UserTelegramUpdateDto()
            {
                Username = message.Chat.Username,
                FirstName = message.Chat.FirstName,
                LastName = message.Chat.LastName,
                Phone = message.Contact?.PhoneNumber,
                Language = message.From?.LanguageCode,
            };

            await _telegramRepository.UpdateAsync(message.From!.Id.ToString(), dto);
            await _telegramRepository.ChangeStatus(message.From!.Id.ToString(), ETelegramUserStatus.Default, 0);
        }
        else if (message.Text.StartsWith("/setusername") && user != null)
        {
            await bot.SendTextMessageAsync(message.Chat.Id, $"Current name is {user.Username} \r\nEnter your new name:");
            await _telegramRepository.ChangeStatus(message.From!.Id.ToString(), ETelegramUserStatus.SetUsername, 0);
        }
        else if (message.Text.StartsWith("/sethashtag") && user != null)
        {
            await bot.SendTextMessageAsync(message.Chat.Id, $"Current hashtag is {user.Hashtag} \r\nEnter your new hashtag:");
            await _telegramRepository.ChangeStatus(message.From!.Id.ToString(), ETelegramUserStatus.SetHashtag, 0);
        }
        else if (message.Text.StartsWith("/profile") && user != null)
        {
            await bot.SendTextMessageAsync(message.Chat.Id,
                $"User data:\n" +
                $"Username - {user.Username}\n" +
                $"Hashtag - {user.Hashtag}\n" +
                $"\nStatistic:\n" +
                $"DateOfUserRegistration - {user}\n" +
                $"Interaction with the bot - {user}\n" +
                $"\nResoursec:\n" +
                $"RandomCoin - {user}\n" +
                $"Fackoins - {user}\n" +
                $"SoulValue - {user}\n" +
                $"Energy - {user}\n");
        }
        else
        {
            await bot.SendTextMessageAsync(message.Chat.Id, "Unknown command.");
        }
    }

    public async Task SetUsername(ITelegramBotClient bot, Message message)
    {
        var userTelegram = await _telegramRepository.GetAsync(message.From!.Id.ToString());

        if (userTelegram == null) return;
        var user = await _userRepository.GetAsync(userTelegram.UserId);
        if (user == null) return;

        var chatId = message.Chat.Id;
        var newUsername = message.Text!.Trim();

        if (newUsername.Length > 16)
        {
            await bot.SendTextMessageAsync(chatId, "Username must be 16 characters or less. Please enter a valid username:");
            return;
        }

        var existingUser = await _userRepository.GetAsync(userTelegram.UserId);
        if (existingUser != null)
        {
            await bot.SendTextMessageAsync(chatId, "This username and hashtag is already taken. Please enter a different username or change hashtag:");
            return;
        }

        user.Username = newUsername;
        await _telegramRepository.ChangeStatus(message.From!.Id.ToString(), ETelegramUserStatus.Default, 0);
        await bot.SendTextMessageAsync(chatId, $"Your name has been changed. Your new name: {newUsername}");
    }

    public async Task SetHashtag(ITelegramBotClient bot, Message message)
    {
        var userTelegram = await _telegramRepository.GetAsync(message.From!.Id.ToString());

        if (userTelegram == null) return;
        var user = await _userRepository.GetAsync(userTelegram.UserId);
        if (user == null) return;

        var chatId = message.Chat.Id;
        var newHashtag = message.Text!.Trim();

        if (newHashtag.Length < 1 || newHashtag.Length > 5)
        {
            await bot.SendTextMessageAsync(chatId, "The hashtag must consist of at least 1 character, no more than 5.");
            return;
        }

        if (!ContainsOnlyAsciiLettersAndDigits(newHashtag))
        {
            await bot.SendTextMessageAsync(chatId, "Symbols are available in the hashtag [a-zA-Z0-9].");
            return;
        }

        var existingUser = await _userRepository.GetAsync(user.Id);
        if (existingUser != null)
        {
            await bot.SendTextMessageAsync(chatId, "This hashtag and username is already taken. Please enter a different hashtag or change username:");
            return;
        }

        user.Hashtag = newHashtag;

        await _telegramRepository.ChangeStatus(message.From!.Id.ToString(), ETelegramUserStatus.Default, 0);
        await bot.SendTextMessageAsync(chatId, $"Your hashtag has been changed. Your new hashtag: {newHashtag}");

        static bool ContainsOnlyAsciiLettersAndDigits(string input) => Regex.IsMatch(input, @"^[a-zA-Z0-9]+$");
    }

    public async Task UserRegistration(ITelegramBotClient bot, Message message)
    {
        var userTelegram = await _telegramRepository.GetAsync(message.From!.Id.ToString());

        if (userTelegram == null) return;
        var user = await _userRepository.GetAsync(userTelegram.UserId);
        if (user == null) return;

        var chatId = message.Chat.Id;

        if (userTelegram.StatusLevel == 0)
        {
            await bot.SendTextMessageAsync(chatId, $"Hi {userTelegram.FirstName}. Enter your name to be used in the game:");
            await _telegramRepository.ChangeStatus(message.From!.Id.ToString(), ETelegramUserStatus.UserRegistration, 1);
        }
        else if (userTelegram.StatusLevel == 1)
        {
            var newUsername = message.Text!.Trim();

            if (newUsername.Length > 16)
            {
                await bot.SendTextMessageAsync(chatId, "Username must be 16 characters or less. Please enter a valid username:");
                return;
            }

            var existingUser = await _userRepository.GetAsync(userTelegram.UserId);
            if (existingUser != null)
            {
                await bot.SendTextMessageAsync(chatId, "This username and hashtag is already taken. Please enter a different username:");
                return;
            }

            user.Username = newUsername;
            await _telegramRepository.ChangeStatus(message.From!.Id.ToString(), ETelegramUserStatus.Default, 0);
            await bot.SendTextMessageAsync(chatId, $"Congratulations {newUsername} and have a great trip. You can view your information with the /profile command.");
        }
    }
}

