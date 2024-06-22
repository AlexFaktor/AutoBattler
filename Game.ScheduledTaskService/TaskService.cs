using Dapper;
using Game.Core.DatabaseRecords.ScheduledTask;
using Game.Core.Resources.Interfraces.ScheduledTaskService;
using Game.Database.Repositorys.ScheduledTasks;
using Game.ScheduledTaskService.Executors;
using Npgsql;
using System.Data;

namespace Game.Web;

public class TaskService : ITaskService
{
    private readonly ILogger<TaskService> _logger;
    private readonly IDbConnection _connection;
    private readonly ExecutorGlobalTasks _executorGlobalTasks;
    private readonly ExecutorIndividualTasks _executorIndividualTasks;

    private readonly GlobalTaskRepository _globalTaskRepository;
    private readonly IndividuaTaskRepository _individuaTaskRepository;

    public TaskService(ILogger<TaskService> logger, IDbConnection connection)
    {
        _logger = logger;
        _connection = connection;

        _executorGlobalTasks = new ExecutorGlobalTasks(connection);
        _executorIndividualTasks = new ExecutorIndividualTasks(connection);
        _globalTaskRepository = new(connection.ConnectionString);
        _individuaTaskRepository = new(connection.ConnectionString);
    }

    public async Task ProcessPendingTasksAsync()
    {
        using (IDbConnection db = _connection)
        {
            // Обробка глобальних задач
            var globalTasks = await _globalTaskRepository.GetAllAsync();
            foreach (var task in globalTasks)
            {
                await ProcessGlobalTaskAsync(task, db);
            }

            // Обробка індивідуальних задач
            var individualTasks = await _individuaTaskRepository.;
            foreach (var task in individualTasks)
            {
                await ProcessIndividualTaskAsync(task, db);
            }
        }
    }

    private async Task ProcessGlobalTaskAsync(GlobalTask task, IDbConnection db)
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
            await db.ExecuteAsync("UPDATE scheduledTask.GlobalTasks " +
                                  "SET LastExecutionTime = @LastExecutionTime, CallsNeeded = @CallsNeeded " +
                                  "WHERE Id = @Id", task);
        }
    }

    private async Task ProcessIndividualTaskAsync(IndividualTask task, IDbConnection db)
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
            await db.ExecuteAsync("UPDATE scheduledTask.IndividualTasks " +
                                  "SET LastExecutionTime = @LastExecutionTime, CallsNeeded = @CallsNeeded " +
                                  "WHERE Id = @Id", task);
        }
    }
}
