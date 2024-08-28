using App.Core.DatabaseRecords.ScheduledTask;
using System.Data;

namespace App.ScheduledTaskService.Executors;

public class ExecutorIndividualTasks(IDbConnection connection)
{
    private readonly IDbConnection _connection = connection;

    public async Task Execute(IndividualTask task)
    {

    }
}
