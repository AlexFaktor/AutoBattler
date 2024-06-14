using Game.Core.Resources.Enums.Game;

namespace Game.Core.Database.Records.Users
{
    public class UserCharacter
    {
        public Guid UserId { get; set; }
        public ECharacter CharacterId { get; set; }
    }
}
