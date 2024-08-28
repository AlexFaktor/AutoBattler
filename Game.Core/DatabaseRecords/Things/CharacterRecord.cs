using App.Core.Resources.Enums.Game;

namespace App.Core.DatabaseRecords.Things;

public class CharacterRecord
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ECharacter CharacterId { get; set; }
}
