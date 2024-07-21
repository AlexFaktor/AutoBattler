using App.GameCore.Tools.ConfigImporters.ConfigReaders;

namespace App.GameCore.Tools.ConfigImporters;

public class DownloadAllGameConfigService
{
    public readonly CharacterConfigReader characterConfigs;
    public DownloadAllGameConfigService(List<GoogleSheetsSettings> sheetsSettings, HttpClient httpClient)
    {
        foreach (GoogleSheetsSettings settings in sheetsSettings)
        {
            var downloader = new GoogleSheetsDownloader(settings, httpClient);
            downloader.Authorize();
            downloader.DownloadGoogleSheetAsync();
        }

        var characterSheetsSettings = sheetsSettings.First(s => s.SheetName == "character");
        characterConfigs = new CharacterConfigReader(characterSheetsSettings.CsvFilePath);
    }
}
