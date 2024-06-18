using Game.Core.Database.Records.ScheduledTask;
using Game.Database.Context;

namespace Game.ScheduledTaskService.Executors
{
    public class ExecutorIndividualTasks(GameDbContext db)
    {
        private readonly GameDbContext _db = db;

        public async Task Execute(IndividualTask task)
        {
            
        }
    }
}
