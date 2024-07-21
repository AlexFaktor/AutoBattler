using App.ContentManagementSystemAPI;
using App.GameCore.Tools.ConfigImporters.ConfigReaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.ContentManagementSystemAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Google Sheets downloader service
            builder.Services.AddSingleton<GoogleSheetsDownloader>();
            builder.Services.AddSingleton<CharacterConfigReader>(provider =>
            {
                var googleSheetsDownloader = provider.GetRequiredService<GoogleSheetsDownloader>();
                googleSheetsDownloader.DownloadSheetAsCsv().Wait(); // Ensure the config is downloaded before creating the reader
                return new CharacterConfigReader("path/to/your/csv/file.csv"); // Path to your CSV file
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
            app.Run();
        }
    }
}
