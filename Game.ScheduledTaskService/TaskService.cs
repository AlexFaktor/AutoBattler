using App.Core.DatabaseRecords.ScheduledTask;
using App.Core.Resources.Interfraces.ScheduledTaskService;
using App.Database.Repositorys.ScheduledTasks;
using App.ScheduledTaskService.Executors;
using System.Data;

namespace App.ScheduledTaskService;

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
        _globalTaskRepository = new GlobalTaskRepository(connection.ConnectionString);
        _individuaTaskRepository = new IndividuaTaskRepository(connection.ConnectionString);
    }

    public async Task ProcessPendingTasksAsync()
    {
        using (IDbConnection db = _connection)
        {
            try
            {
                // Обробка глобальних задач
                var globalTasks = await _globalTaskRepository.GetAllAsync();
                foreach (var task in globalTasks)
                {
                    await ProcessGlobalTaskAsync(task, db);
                }

                // Обробка індивідуальних задач
                var individualTasks = await _individuaTaskRepository.GetAllAsync();
                foreach (var task in individualTasks)
                {
                    await ProcessIndividualTaskAsync(task, db);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing tasks.");
            }
        }
    }

    private async Task ProcessGlobalTaskAsync(GlobalTask task, IDbConnection db)
    {
        CalculateTime(out int executionsNeeded, out int timeToNextExecution, task);

        if (Math.Abs(executionsNeeded) > 0)
        {
            _logger.LogInformation($"Executing global task {task.Id} {executionsNeeded} times.");

            for (uint i = 0; i < Math.Abs(executionsNeeded) && task.CallsNeeded > 0; i++)
            {
                await _executorGlobalTasks.Execute(task);
                task.CallsNeeded--;
            }

            task.LastExecutionTime = DateTime.UtcNow;
            await _globalTaskRepository.UpdateAsync(task.Id, task);
        }
    }

    private async Task ProcessIndividualTaskAsync(IndividualTask task, IDbConnection db)
    {
        CalculateTime(out int executionsNeeded, out int timeToNextExecution, task);

        if (executionsNeeded > 0)
        {
            _logger.LogInformation($"Executing individual task {task.Type.ToString()} for user {task.UserId} {executionsNeeded} times.");

            for (int i = 0; i < executionsNeeded && task.CallsNeeded > 0; i++)
            {
                await _executorIndividualTasks.Execute(task);
                task.CallsNeeded--;
            }
            task.LastExecutionTime = DateTime.UtcNow;
            await _individuaTaskRepository.UpdateAsync(task.Id, task);
        }
    }

    private static void CalculateTime(out int executionsNeeded, out int timeToNextExecution, AbstractTask task)
    {
        var timeSinceLastExecution = DateTime.UtcNow - task.LastExecutionTime; // Скільки часу прошло з останього виклику
        executionsNeeded = (int)timeSinceLastExecution.TotalSeconds / task.FrequencyInSeconds; // Скільки викликів потрібно зробити
        timeToNextExecution = (int)timeSinceLastExecution.TotalSeconds - (executionsNeeded * task.FrequencyInSeconds); // Час до наступного виклику
    }
}
