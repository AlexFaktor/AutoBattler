using Game.Core.Database.Records.Users;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UserResourcesService
    {
        private readonly GameDbContext _db;

        public UserResourcesService(GameDbContext db)
        {
            _db = db;
        }

        public async Task<UserResources?> AddAsync(UserResources resources)
        {
            await _db.UserResources.AddAsync(resources);
            await _db.SaveChangesAsync();
            return resources;
        }

        public async Task<List<UserResources>> GetAllAsync() => await _db.UserResources.ToListAsync();

        public async Task<UserResources?> GetAsync(Guid userId)
        {
            return await _db.UserResources.FirstOrDefaultAsync(r => r.UserId == userId);
        }

        public async Task<UserResources?> UpdateAsync(Guid userId, UserResources updatedResources)
        {
            var resources = await _db.UserResources.FirstOrDefaultAsync(r => r.UserId == userId);
            if (resources is not null)
            {
                resources.RandomCoin = updatedResources.RandomCoin;
                resources.Fackoins = updatedResources.Fackoins;
                resources.SoulValue = updatedResources.SoulValue;
                resources.Energy = updatedResources.Energy;

                await _db.SaveChangesAsync();
                return resources;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var resources = await _db.UserResources.FirstOrDefaultAsync(r => r.UserId == userId);
            if (resources is not null)
            {
                _db.UserResources.Remove(resources);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
