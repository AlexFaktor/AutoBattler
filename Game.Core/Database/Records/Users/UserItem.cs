using Game.Core.Resources.Enums.Game;

namespace Game.Core.Database.Records.Users
{
    public class UserItem
    {
        public Guid UserId { get; set; }
        public virtual UserRecord User { get; set; }
        public EItem ItemId { get; set; }
    }
}
