using App.Core.Resources.Enums.Game;

namespace App.Core.DatabaseRecords.Camp;

public class CampBuilding
{
    public Guid UserId { get; set; }
    public EBuildings BuildingId { get; set; }
}
