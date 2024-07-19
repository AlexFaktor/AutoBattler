using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

public class GoogleSheetsDownloader
{
    private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private static readonly string ApplicationName = "YourApplicationName";
    private readonly string _spreadsheetId;
    private readonly string _sheetName;
    private readonly string _csvFilePath;
    private readonly string _credentialsFilePath;
    private readonly string _range;

    public GoogleSheetsDownloader(string spreadsheetId, string sheetName, string range, string csvFilePath, string credentialsFilePath)
    {
        _spreadsheetId = spreadsheetId;
        _sheetName = sheetName;
        _csvFilePath = csvFilePath;
        _credentialsFilePath = credentialsFilePath;
        _range = $"{_sheetName}!{range}";
    }

    public async Task DownloadSheetAsCsv()
    {
        GoogleCredential credential;

        using (var stream = new FileStream(_credentialsFilePath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(Scopes);
        }

        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        if (!File.Exists(_csvFilePath))
        {
            await DownloadSheet(service);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"CSV file {_csvFilePath} already exists.");
            Console.ResetColor();
        }
    }

    private async Task DownloadSheet(SheetsService service)
    {
        var request = service.Spreadsheets.Values.Get(_spreadsheetId, _range);
        var response = await request.ExecuteAsync();

        using (var streamWriter = new StreamWriter(_csvFilePath))
        {
            foreach (var row in response.Values)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    streamWriter.Write($"{row[i]}");
                    if (i != row.Count - 1)
                    {
                        streamWriter.Write(",");
                    }
                }
                streamWriter.WriteLine();
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"CSV file {_csvFilePath} downloaded successfully.");
        Console.ResetColor();
    }
}
