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

        public async Task<User?> AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllAsync() => await _db.Users.ToListAsync();

        public async Task<User?> GetAsync(Guid id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> UpdateAsync(Guid id, User updatedUser)
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
