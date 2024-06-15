using Game.Core.Database.Records.Users;
using Game.Core.Dtos.UserDtos.Telegrams;
using Game.Core.Resources.Enums.Telegram;
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
        
        public async Task<UserTelegram?> GetAsync(string userId)
        {
            return await _db.UserTelegrams.FirstOrDefaultAsync(t => t.TelegramId == userId);
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

        public async Task<UserTelegram?> UpdateAsync(string userId, UserTelegramUpdateDto dto)
        {
            var telegram = await _db.UserTelegrams.FirstOrDefaultAsync(t => t.TelegramId == userId);
            if (telegram is not null)
            {
                telegram.Username = dto.Username;
                telegram.FirstName = dto.FirstName;
                telegram.LastName = dto.LastName;
                telegram.Phone = dto.Phone;
                telegram.Language = dto.Language;

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

        public async Task<(ETelegramUserStatus, ushort)> GetStatus(string userId)
        {
            var telegram = await _db.UserTelegrams.FirstOrDefaultAsync(t => t.TelegramId == userId);

            return (telegram!.Status, telegram.StatusLevel);
        }

        public async Task<bool> ChangeStatus(string userId, ETelegramUserStatus status, ushort statusLvl)
        {
            var telegram = await _db.UserTelegrams.FirstOrDefaultAsync(t => t.TelegramId == userId);
            telegram!.Status = status;
            telegram!.StatusLevel = statusLvl;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
