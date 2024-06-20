using Game.Core.Database.Records.Things;
using Game.Core.Resources.Enums.Game;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UItemRepository
    {
        private readonly GameDbContext _db;

        public UItemRepository(GameDbContext db)
        {
            _db = db;
        }

        public async Task<ItemRecord?> AddAsync(ItemRecord item)
        {
            await _db.UserItems.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<List<ItemRecord>> GetAllAsync() => await _db.UserItems.ToListAsync();

        public async Task<ItemRecord?> GetAsync(Guid userId, EItem itemId)
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
