using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Game;

namespace Game.Core.Database.Records.Things
{
    public class CharacterRecord
    {
        public Guid UserId { get; set; }
        public virtual GameUser User { get; set; }
        public ECharacter CharacterId { get; set; }
    }
}
