using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;

namespace App.GameCore.Tools.ConfigImporters;

public class GoogleSheetsDownloader
{
    private readonly GoogleSheetsSettings _settings;
    private readonly HttpClient _httpClient;
    private static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
    private UserCredential _credential;

    public GoogleSheetsDownloader(GoogleSheetsSettings settings, HttpClient httpClient)
    {
        _settings = settings;
        _httpClient = httpClient;
    }

    public void Authorize()
    {
        using var stream = new FileStream(_settings.CredentialsFilePath, FileMode.Open, FileAccess.Read);
        string credPath = "token.json";
        _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            GoogleClientSecrets.Load(stream).Secrets,
            Scopes,
            "user",
            CancellationToken.None,
            new FileDataStore(credPath, true)).Result;
        Console.WriteLine("Credential file saved to: " + credPath);
    }

    public async Task DownloadGoogleSheetAsync()
    {
        if (_credential == null)
        {
            throw new InvalidOperationException("Authorization is required before making requests.");
        }

        if (File.Exists(_settings.CsvFilePath))
        {
            Console.WriteLine("File already exists.");
            return;
        }

        string accessToken = await _credential.GetAccessTokenForRequestAsync();
        string downloadUrl = $"https://docs.google.com/spreadsheets/d/{_settings.SpreadsheetId}/gviz/tq?tqx=out:csv&sheet={_settings.SheetName}";

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, downloadUrl);
        requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsByteArrayAsync();
            await File.WriteAllBytesAsync(_settings.CsvFilePath, content);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("File downloaded successfully.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Failed to download file. Status code: " + response.StatusCode);
            Console.ResetColor();
        }
    }
}

public class GoogleSheetsSettings
{
    public string SpreadsheetId { get; set; } = string.Empty;
    public string SheetName { get; set; } = string.Empty;
    public string CsvFilePath { get; set; } = string.Empty;
    public string CredentialsFilePath { get; set; } = string.Empty;
}
