using Game.Core.Database.Records.Users;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UserService
    {
        private readonly GameDbContext _db;

        public UserService(GameDbContext db)
        {
            _db = db;
        }

        public async Task<UserRecord?> AddAsync(UserRecord user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<List<UserRecord>> GetAllAsync() => await _db.Users.ToListAsync();

        public async Task<UserRecord?> GetAsync(Guid id)
        {
            return await _db.Users
                .Include(ur => ur.Telegram)
                .Include(ur => ur.Resources)
                .Include(ur => ur.Statistics)
                .Include(ur => ur.Camp)
                .Include(ur => ur.Characters)
                .Include(ur => ur.Items)
                .FirstOrDefaultAsync(ur => ur.Id == id);
        }

        public async Task<UserRecord?> UpdateAsync(Guid id, UserRecord updatedUser)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is not null)
            {
                user.Username = updatedUser.Username;
                user.Hashtag = updatedUser.Hashtag;
                user.Telegram = updatedUser.Telegram;
                user.Resources = updatedUser.Resources;
                user.Statistics = updatedUser.Statistics;
                user.Camp = updatedUser.Camp;
                user.Characters = updatedUser.Characters;
                user.Items = updatedUser.Items;

                await _db.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is not null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
