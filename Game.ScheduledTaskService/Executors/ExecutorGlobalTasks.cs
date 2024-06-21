using Game.Core.Database.Records.ScheduledTask;
using Game.Core.Resources.Enums.ScheduledTask;
using Game.Database.Service.Users;
using System.Data;

namespace Game.ScheduledTaskService.Executors
{
    public class ExecutorGlobalTasks(IDbConnection connection)
    {
        private readonly IDbConnection _connection = connection;
        private readonly UResourcesRepository _resourcesRepository = new(connection.ConnectionString);

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
            var users = await _resourcesRepository.GetAllAsync();

            foreach (var user in users)
            {
                // Додаємо енергію, але не більше ніж MaxEnergy
                user.Energy = Math.Min(user.Energy + energyAdded, maximumEnergy);
                await _resourcesRepository.UpdateAsync(user.UserId ,user);
            }
        }
    }
}
