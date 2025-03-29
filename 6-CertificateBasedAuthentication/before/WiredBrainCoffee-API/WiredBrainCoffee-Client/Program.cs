using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WiredBrainCoffee_API.Data;

class Program
{
    private static readonly string ApiUrl = "https://localhost:7165/Payment/";

    static async Task Main(string[] args)
    {
        using var client = new HttpClient();

        Guid paymentID = Guid.NewGuid();

        HttpResponseMessage response = await client.GetAsync(ApiUrl + paymentID.ToString());
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response Status: {response.StatusCode}");
        Console.WriteLine($"Response Body: {responseBody}");
        Console.Read();
    }
}
