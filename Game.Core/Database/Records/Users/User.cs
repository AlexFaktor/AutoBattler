namespace Game.Core.Database.Records.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Hashtag { get; set; } = string.Empty;

        public UserTelegram Telegram { get; set; } = new();

        public UserResources Resources { get; set; } = new();
        public UserStatistics Statistics { get; set; } = new();

        public UserCamp Camp { get; set; } = new();
        public List<UserCharacter> Characters { get; set; } = new();
        public List<UserItem> Items { get; set; } = new();
    }
}
