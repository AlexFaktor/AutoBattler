namespace Game.Core.Database.Records.Users
{
    public class UserResources
    {
        public Guid UserId { get; set; }
        public virtual UserRecord User { get; set; }

        public decimal RandomCoin { get; set; } = 0;
        public decimal Fackoins { get; set; } = 0;
        public uint SoulValue { get; set; } = 0;

        public uint Energy { get; set; } = 0;
    }
}
