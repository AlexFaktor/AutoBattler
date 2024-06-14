using Game.Core.Resources.Enums.Game;

namespace Game.Core.Database.Records.Camp
{
    public class CampBuilding
    {
        public Guid UserId { get; set; }
        public EBuildings BuildingId { get; set; }
    }
}
