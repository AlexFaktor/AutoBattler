using App.Core.Resources.Interfraces.ScheduledTaskService;
using Npgsql;
using System.Data;

namespace App.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));

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
}
