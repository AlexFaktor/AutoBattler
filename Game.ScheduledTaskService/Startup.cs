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

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(connectionString));

            services.AddScoped<ITaskService, TaskService>();
            services.AddSingleton<IHostedService, TaskSchedulerHostedService>();
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
