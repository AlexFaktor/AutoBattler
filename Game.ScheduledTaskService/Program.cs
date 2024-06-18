using Game.Core.Resources.Interfraces.ScheduledTaskService;

namespace Game.Web;

public class Program
{
    public static void Main(string[] args)
    {
        // Створюємо і запускаємо хост
        var host = CreateHostBuilder(args).Build();

        // Виконуємо ініціалізаційні завдання
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                // Отримуємо сервіс для управління задачами
                var taskService = services.GetRequiredService<ITaskService>();
                // Запускаємо обробку задач
                taskService.ProcessPendingTasksAsync().Wait();
            }
            catch (Exception ex)
            {
                // Логування помилок
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while initializing the task processing.");
            }
        }

        // Запускаємо хост
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // Вказуємо клас Startup для конфігурації
                webBuilder.UseStartup<Startup>();
            });
}

