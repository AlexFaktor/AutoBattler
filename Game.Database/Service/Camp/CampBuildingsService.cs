using Game.Core.Database.Records.Camp;
using Game.Core.Resources.Enums.Game;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Camp
{
    public class CampBuildingsService
    {
        private readonly GameDbContext _db;

        public CampBuildingsService(GameDbContext db)
        {
            _db = db;
        }

        public async Task<CampBuilding?> AddAsync(CampBuilding campBuilding)
        {
            await _db.CampBuildings.AddAsync(campBuilding);
            await _db.SaveChangesAsync();
            return campBuilding;
        }

        public async Task<List<CampBuilding>> GetAllAsync() => await _db.CampBuildings.ToListAsync();

        public async Task<CampBuilding?> GetAsync(Guid userId, EBuildings buildingId)
        {
            return await _db.CampBuildings.FirstOrDefaultAsync(b =>
                                                                b.UserId == userId &&
                                                                b.BuildingId == buildingId);
        }

        // public async Task<CampBuilding?> UpdateAsync(Guid userId, EBuildings buildingId, )
    }
}
