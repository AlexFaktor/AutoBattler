using Game.Core.Database.Records.Camp;

namespace Game.Core.Database.Records.Users
{
    public class UserCamp
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<CampBuilding> Buildings { get; set; } = new();
    }
}
