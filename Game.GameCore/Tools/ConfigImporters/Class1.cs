using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace Game.GameCore.Tools.ConfigImportersl;

public class GoogleSheetsImporter
{
    private readonly List<string> _headers = [];

    private readonly string _sheetId;
    private readonly SheetsService _service;

    public GoogleSheetsImporter(string credentialPath, string sheetId)
    {
        _sheetId = sheetId;

        GoogleCredential credential;
        using (var stream = new System.IO.FileStream(credentialPath, System.IO.FileMode.Open))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);
        }

        _service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential
        });
    }

    public async Task DownloadAndParseSheet(string sheetName)
    {
        Console.WriteLine($"Starting download sheet (${sheetName})...");

        var range = $"{sheetName}!A1:Z";
        var request = _service.Spreadsheets.Values.Get(_spreadsheetId, range);

        ValueRange response;

        try
        {
            response = await request.ExecuteAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        // Parse the received data
        if (response != null && response.Values != null)
        {
            var tableArray = response.Values;

            var firstRow = tableArray[0];
            foreach (var cell in firstRow)
            {
                _heagers.Add(cell.ToString());
            }

            var rowsCount = tableArray.Count;
        }
    }
}
