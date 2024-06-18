namespace Game.Core.Resources.Interfraces.ScheduledTaskService;

public interface ITaskService
{
    Task ProcessPendingTasksAsync();
}

