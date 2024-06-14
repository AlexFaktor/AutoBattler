using Game.Core.Database.Records.Users;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UserTelegramService
    {
        private readonly GameDbContext _db;

        public UserTelegramService(GameDbContext db)
        {
            _db = db;
        }

        public async Task<UserTelegram?> AddAsync(UserTelegram telegram)
        {
            await _db.UserTelegrams.AddAsync(telegram);
            await _db.SaveChangesAsync();
            return telegram;
        }

        public async Task<List<UserTelegram>> GetAllAsync() => await _db.UserTelegrams.ToListAsync();

        public async Task<UserTelegram?> GetAsync(Guid userId)
        {
            return await _db.UserTelegrams.FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<UserTelegram?> UpdateAsync(Guid userId, UserTelegram updatedTelegram)
        {
            var telegram = await _db.UserTelegrams.FirstOrDefaultAsync(t => t.UserId == userId);
            if (telegram is not null)
            {
                telegram.TelegramId = updatedTelegram.TelegramId;
                telegram.Username = updatedTelegram.Username;
                telegram.FirstName = updatedTelegram.FirstName;
                telegram.LastName = updatedTelegram.LastName;
                telegram.Phone = updatedTelegram.Phone;
                telegram.Language = updatedTelegram.Language;

                await _db.SaveChangesAsync();
                return telegram;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var telegram = await _db.UserTelegrams.FirstOrDefaultAsync(t => t.UserId == userId);
            if (telegram is not null)
            {
                _db.UserTelegrams.Remove(telegram);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
