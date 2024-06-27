using Game.Core.DatabaseRecords.ScheduledTask;
using Game.Core.Resources.Enums.ScheduledTask;
using Game.Database.Repositorys.ScheduledTasks;
using Game.Database.Service.Users;
using System.Data;

namespace Game.ScheduledTaskService.Executors;

public class ExecutorGlobalTasks(IDbConnection connection)
{
    private readonly IDbConnection _connection = connection;
    private readonly UResourcesRepository _resourcesRepository = new(connection.ConnectionString);
    private readonly GlobalTaskRepository _globalTaskRepository = new(connection.ConnectionString);

    public async Task Execute(GlobalTask task)
    {
        switch (task.Type)
        {
            case EGlobalTask.RestoreEnergy:
                await RestoreEnergy(task, 20, 400);
                break;

            default:
                break;
        }
    }

    private async Task RestoreEnergy(GlobalTask task, uint energyAdded, uint maximumEnergy)
    {
        task.CallsNeeded += 1;
        await _globalTaskRepository.UpdateAsync(task.Id, task);

        var users = await _resourcesRepository.GetAllAsync();

        foreach (var user in users)
        {
            user.Energy = (int)Math.Min(user.Energy + energyAdded, maximumEnergy);
            await _resourcesRepository.UpdateAsync(user.UserId, user);
        }
    }
}
