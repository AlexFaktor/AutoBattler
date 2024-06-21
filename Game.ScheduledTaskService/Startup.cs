using Game.Core.Resources.Interfraces.ScheduledTaskService;
using Npgsql;
using System.Data;

namespace Game.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add configuration for PostgreSQL connection
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(connectionString));

            // Реєстрація сервісів
            services.AddScoped<ITaskService, TaskService>(); // Scoped service
            services.AddSingleton<IHostedService, TaskSchedulerHostedService>(); // Singleton service
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
}
