using Game.Core.Database.Records.ScheduledTask;
using Game.Core.Resources.Interfraces.ScheduledTaskService;
using Game.Database.Context;
using Game.ScheduledTaskService.Executors;

namespace Game.Web;

public class TaskService : ITaskService
{
    private readonly GameDbContext _db;
    private readonly ILogger<TaskService> _logger;
    private readonly ExecutorGlobalTasks _executorGlobalTasks;
    private readonly ExecutorIndividualTasks _executorIndividualTasks;

    public TaskService(GameDbContext dbContext, ILogger<TaskService> logger)
    {
        _db = dbContext;
        _logger = logger;

        _executorGlobalTasks = new(_db);
        _executorIndividualTasks = new(_db);
    }

    public async Task ProcessPendingTasksAsync()
    {
        // Обробка глобальних задач
        var globalTasks = _db.GlobalTasks.ToList();
        foreach (var task in globalTasks)
        {
            await ProcessGlobalTaskAsync(task);
        }

        // Обробка індивідуальних задач
        var individualTasks = _db.IndividualTasks.ToList();
        foreach (var task in individualTasks)
        {
            await ProcessIndividualTaskAsync(task);
        }
    }

    private async Task ProcessGlobalTaskAsync(GlobalTask task)
    {
        var now = DateTime.UtcNow;
        var timeSinceLastExecution = now - task.LastExecutionTime;
        var executionsNeeded = (uint)(timeSinceLastExecution.TotalSeconds / task.FrequencyInSeconds);

        if (executionsNeeded > 0)
        {
            _logger.LogInformation($"Executing global task {task.Id} {executionsNeeded} times.");

            for (uint i = 0; i < executionsNeeded && task.CallsNeeded > 0; i++)
            {
                // Логіка виконання глобального завдання
                await _executorGlobalTasks.Execute(task);
                task.CallsNeeded--;
            }

            task.LastExecutionTime = now;
            _db.GlobalTasks.Update(task);
            await _db.SaveChangesAsync();
        }
    }

    private async Task ProcessIndividualTaskAsync(IndividualTask task)
    {
        var now = DateTime.UtcNow;
        var timeSinceLastExecution = now - task.LastExecutionTime;
        var executionsNeeded = (uint)(timeSinceLastExecution.TotalSeconds / task.FrequencyInSeconds);

        if (executionsNeeded > 0)
        {
            _logger.LogInformation($"Executing individual task {task.Id} for user {task.UserId} {executionsNeeded} times.");

            for (uint i = 0; i < executionsNeeded && task.CallsNeeded > 0; i++)
            {
                // Логіка виконання індивідуального завдання
                await _executorIndividualTasks.Execute(task);
                task.CallsNeeded--;
            }

            task.LastExecutionTime = now;
            _db.IndividualTasks.Update(task);
            await _db.SaveChangesAsync();
        }
    }
}

