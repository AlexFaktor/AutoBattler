using App.GameCore.Tools.ConfigImporters;

namespace App.ContentManagementSystemAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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
        builder.Services.AddSingleton<DownloadAllGameConfigService>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient();
            return new DownloadAllGameConfigService(googleSheetsSettings, httpClient);
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
        var downloadService = app.Services.GetRequiredService<DownloadAllGameConfigService>();
        await downloadService.InitializeAsync();

        app.Run();
    }
}
