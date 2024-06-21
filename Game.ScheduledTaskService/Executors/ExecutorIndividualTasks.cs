using Game.Core.Database.Records.ScheduledTask;
using System.Data;

namespace Game.ScheduledTaskService.Executors
{
    public class ExecutorIndividualTasks(IDbConnection connection)
    {
        private readonly IDbConnection _connection = connection;

        public async Task Execute(IndividualTask task)
        {

        }
    }
}
