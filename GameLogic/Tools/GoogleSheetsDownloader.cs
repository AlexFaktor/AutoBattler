using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Serilog;

namespace GameLogic.Tools;

public class GoogleSheetsDownloader
{
    private readonly GoogleSheetsSettings _settings;
    private readonly HttpClient _httpClient;
    private static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
    private GoogleCredential _credential;

    public GoogleSheetsDownloader(GoogleSheetsSettings settings, HttpClient httpClient)
    {
        _settings = settings;
        _httpClient = httpClient;
        Authorize();
    }

    private void Authorize()
    {
        try
        {
            Log.Information($"Loading credentials.");

            using (var stream = new FileStream(_settings.CredentialsFilePath, FileMode.Open, FileAccess.Read))
            {
                _credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            if (_credential == null)
            {
                throw new InvalidOperationException("Failed to create credentials from the service account file.");
            }

            Log.Information($"Authorization successful.");
        }
        catch (Exception ex)
        {
            Log.Error("Error during authorization: " + ex.Message);
            throw;
        }
    }

    public async Task DownloadGoogleSheetAsync()
    {
        if (_credential == null)
        {
            throw new InvalidOperationException("Authorization is required before making requests.");
        }

        if (File.Exists(_settings.CsvFilePath))
        {
            Log.Warning($"File {_settings.CsvFilePath} already exists.");
            return;
        }

        string downloadUrl = $"https://docs.google.com/spreadsheets/d/{_settings.SpreadsheetId}/gviz/tq?tqx=out:csv&sheet={_settings.SheetName}";

        try
        {
            var token = await _credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, downloadUrl);
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(_settings.CsvFilePath, content);
                Log.Information("File downloaded successfully");
            }
            else
            {
                Log.Warning($"Failed to download file. Status code:  {response.StatusCode} downloaded successfully");
            }
        }
        catch (Exception ex)
        {
            Log.Error("Error during download: " + ex.Message);
            throw;
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
