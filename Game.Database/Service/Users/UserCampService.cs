using Game.Core.Database.Records.Users;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UserCampService
    {
        private readonly GameDbContext _db;

        public UserCampService(GameDbContext db)
        {
            _db = db;
        }

        public async Task<UserCamp?> AddAsync(UserCamp camp)
        {
            await _db.UserCamps.AddAsync(camp);
            await _db.SaveChangesAsync();
            return camp;
        }

        public async Task<List<UserCamp>> GetAllAsync() => await _db.UserCamps.ToListAsync();

        public async Task<UserCamp?> GetAsync(Guid userId)
        {
            return await _db.UserCamps.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<UserCamp?> UpdateAsync(Guid userId, UserCamp updatedCamp)
        {
            var camp = await _db.UserCamps.FirstOrDefaultAsync(c => c.UserId == userId);
            if (camp is not null)
            {
                camp.Name = updatedCamp.Name;
                await _db.SaveChangesAsync();
                return camp;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var camp = await _db.UserCamps.FirstOrDefaultAsync(c => c.UserId == userId);
            if (camp is not null)
            {
                _db.UserCamps.Remove(camp);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
