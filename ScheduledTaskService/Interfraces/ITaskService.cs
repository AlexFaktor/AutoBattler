namespace Core.Resources.Interfraces.ScheduledTaskService;

public interface ITaskService
{
    Task ProcessPendingTasksAsync();
}

