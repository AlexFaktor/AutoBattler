using Game.Core.Resources.Enums.Game;

namespace Game.Core.DatabaseRecords.Things;

public class CharacterRecord
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ECharacter CharacterId { get; set; }
}
