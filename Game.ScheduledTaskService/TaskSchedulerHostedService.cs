using Game.Core.Resources.Interfraces.ScheduledTaskService;

namespace Game.Web;

public class TaskSchedulerHostedService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskSchedulerHostedService> _logger;

    public TaskSchedulerHostedService(ITaskService taskService, ILogger<TaskSchedulerHostedService> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Task Scheduler Hosted Service running.");
        _timer = new Timer(ProcessTasks, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        return Task.CompletedTask;
    }

    private void ProcessTasks(object state)
    {
        _taskService.ProcessPendingTasksAsync().Wait();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Task Scheduler Hosted Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
