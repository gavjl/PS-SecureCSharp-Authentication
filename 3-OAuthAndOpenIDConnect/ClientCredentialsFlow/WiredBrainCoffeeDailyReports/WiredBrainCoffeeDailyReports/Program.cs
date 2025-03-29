using Newtonsoft.Json;
using RestSharp;

class Program
{
    private static readonly string _clientId = "KkDREHE2BIY4hhBuOq86MELHh4Vqzeru";
    private static readonly string _clientSecret = "dUbFIz0gVvkqaMu6NEZ0x04-CJixy1XreVl5Ya4XpQFEn0kFTNq4VGrxm5j24-68";  //Don't hardcode this, store it securely!
    private static readonly string _authorizationServerUrl = 
            "https://dev-frc3e1y1xb0btl25.us.auth0.com/oauth/token";
    private static readonly string _reportDataApiUrl = "https://localhost:7212/";

    static async Task Main()
    {
        Console.WriteLine("Starting report generation process...");

        string token = await GetAccessTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine("Failed to get access token. Exiting.");
            return;
        }

        string reportData = await GetReportDataAsync(token);
        if (string.IsNullOrEmpty(reportData))
        {
            Console.WriteLine($"Failed to get report data");
            return;
        }

        Console.WriteLine("Generating reports based on data...");

        Console.WriteLine("Report generation complete!");

    }

    private static async Task<string> GetAccessTokenAsync()
    {
        var client = new RestClient(_authorizationServerUrl);
        var request = new RestRequest("", Method.Post);

        var payload = new
        {
            client_id = _clientId,
            client_secret = _clientSecret,
            audience = "ReportingDataAPI",
            grant_type = "client_credentials"
        };

        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(JsonConvert.SerializeObject(payload));

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return jsonResponse?.access_token;
        }

        return null;
    }

    private static async Task<string> GetReportDataAsync(string token)
    {
        var client = new RestClient(_reportDataApiUrl);
        var request = new RestRequest("/Data", Method.Get);

        request.AddHeader("Authorization", $"Bearer {token}");
        request.AddHeader("Content-Type", "application/json");

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
            return response.Content;
        else
            return "";
    }
}
