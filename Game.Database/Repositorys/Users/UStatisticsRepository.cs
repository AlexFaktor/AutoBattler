using Game.Core.Database.Records.Users;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Database.Service.Users
{
    public class UStatisticsRepository
    {
        private readonly GameDbContext _db;

        public UStatisticsRepository(GameDbContext db)
        {
            _db = db;
        }

        public async Task<UserStatistics?> AddAsync(UserStatistics statistics)
        {
            await _db.UserStatistics.AddAsync(statistics);
            await _db.SaveChangesAsync();
            return statistics;
        }

        public async Task<List<UserStatistics>> GetAllAsync() => await _db.UserStatistics.ToListAsync();

        public async Task<UserStatistics?> GetAsync(Guid userId)
        {
            return await _db.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<UserStatistics?> UpdateAsync(Guid userId, UserStatistics updatedStatistics)
        {
            var statistics = await _db.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId);
            if (statistics is not null)
            {
                statistics.DateOfUserRegistration = updatedStatistics.DateOfUserRegistration;
                statistics.NumberInteractionsWithBot = updatedStatistics.NumberInteractionsWithBot;

                await _db.SaveChangesAsync();
                return statistics;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var statistics = await _db.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId);
            if (statistics is not null)
            {
                _db.UserStatistics.Remove(statistics);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool?> AddInteraction(Guid userId)
        {
            var statistics = await _db.UserStatistics.FirstOrDefaultAsync(s => s.UserId == userId);
            if (statistics is not null)
            {
                statistics.NumberInteractionsWithBot += 1;

                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
