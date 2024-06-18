using Game.Core.Resources.Interfraces.ScheduledTaskService;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Game.Web;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Підключення бази даних
        services.AddDbContext<GameDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Реєстрація сервісів
        services.AddScoped<ITaskService, TaskService>();
        services.AddHostedService<TaskSchedulerHostedService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}


