using Game.Core.Database.Records.ScheduledTask;
using Game.Core.Resources.Enums.ScheduledTask;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.ScheduledTaskService.Executors
{
    public class ExecutorGlobalTasks(GameDbContext db)
    {
        private readonly GameDbContext _db = db;

        public async Task Execute(GlobalTask task)
        {
            switch (task.Type)
            {
                case EGlobalTask.RestoreEnergy:
                    await RestoreEnergy(40, 200);
                    break;

                default:
                    break;
            }
        }

        private async Task RestoreEnergy(uint energyAdded, uint maximumEnergy)
        {
            // Зчитуємо всіх користувачів
            var users = await _db.UserResources.ToListAsync();

            foreach (var user in users)
            {
                // Додаємо енергію, але не більше ніж MaxEnergy
                user.Energy = Math.Min(user.Energy + energyAdded, maximumEnergy);
            }

            // Зберігаємо зміни в базі даних
            await _db.SaveChangesAsync();
        }
    }
}
