using Game.Core.Database.Records.Users;
using Game.Core.Resources.Enums.Game;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UserItemService
    {
        private readonly GameDbContext _db;

        public UserItemService(GameDbContext db)
        {
            _db = db;
        }

        public async Task<UserItem?> AddAsync(UserItem item)
        {
            await _db.UserItems.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<List<UserItem>> GetAllAsync() => await _db.UserItems.ToListAsync();

        public async Task<UserItem?> GetAsync(Guid userId, EItem itemId)
        {
            return await _db.UserItems.FirstOrDefaultAsync(i => i.UserId == userId && i.ItemId == itemId);
        }

        public async Task<bool> DeleteAsync(Guid userId, EItem itemId)
        {
            var item = await _db.UserItems.FirstOrDefaultAsync(i => i.UserId == userId && i.ItemId == itemId);
            if (item is not null)
            {
                _db.UserItems.Remove(item);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
