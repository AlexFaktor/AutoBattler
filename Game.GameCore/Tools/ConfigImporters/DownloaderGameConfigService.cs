using GameLogic.Tools.ShellImporters.ConfigReaders;
using Serilog;

namespace GameLogic.Tools.ShellImporters;

public class DownloaderGameConfigService
{
    private readonly List<GoogleSheetsSettings> _sheetsSettings;
    private readonly HttpClient _httpClient;
    public CharacterConfigReader characterConfigs { get; private set; } = null;

    public DownloaderGameConfigService(List<GoogleSheetsSettings> sheetsSettings, HttpClient httpClient)
    {
        _sheetsSettings = sheetsSettings;
        _httpClient = httpClient;
    }

    public async Task InitializeAsync()
    {
        Log.Information("Start loading game configs...");
        foreach (GoogleSheetsSettings settings in _sheetsSettings)
        {
            var downloader = new GoogleSheetsDownloader(settings, _httpClient);
            await downloader.DownloadGoogleSheetAsync();
        }
        Log.Information("Download completed successfully ☻");

        // Add other configs
        var characterSheetsSettings = _sheetsSettings.First(s => s.CsvFilePath.EndsWith("characters.csv"));
        characterConfigs = new CharacterConfigReader(characterSheetsSettings.CsvFilePath);
    }
}
