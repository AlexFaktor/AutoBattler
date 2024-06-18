namespace Game.Core.Database.Records.Users
{
    public class UserStatistics
    {
        public Guid UserId { get; set; }
        public virtual GameUser User { get; set; }
        public DateTime DateOfUserRegistration { get; set; } = DateTime.Now;

        public uint NumberInteractionsWithBot { get; set; } = 0;
    }
}
