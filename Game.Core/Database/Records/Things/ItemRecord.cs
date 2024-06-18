using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Game;

namespace Game.Core.Database.Records.Things
{
    public class ItemRecord
    {
        public Guid UserId { get; set; }
        public virtual UserRecord User { get; set; }
        public EItem ItemId { get; set; }
    }
}
