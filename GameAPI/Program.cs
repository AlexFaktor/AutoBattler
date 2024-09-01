using GameLogic.Tools;
using GameLogic.Tools.ShellImporters;
using Serilog;

namespace ContentManagementSystemAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        try
        {
            Log.Information("Starting up the service");
            var builder = WebApplication.CreateBuilder(args);

            // Add Serilog
            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register HttpClient
            builder.Services.AddHttpClient();

            // Load configurations from the appsettings.json file
            var googleSheetsSettings = new List<GoogleSheetsSettings>();
            builder.Configuration.GetSection("GoogleSheetsConfigs").Bind(googleSheetsSettings);

            // Register DownloadAllGameConfigService
            builder.Services.AddSingleton<DownloaderGameConfigService>(sp =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();
                return new DownloaderGameConfigService(googleSheetsSettings, httpClient);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // Initialize the download service
            var downloadService = app.Services.GetRequiredService<DownloaderGameConfigService>();
            await downloadService.InitializeAsync();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "The application failed to start correctly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
