using System.Security.Cryptography.X509Certificates;

class Program
{
    private static readonly string ApiUrl = "https://localhost:7165/Payment/";

    static async Task Main(string[] args)
    {
        var clientCertificate =
            new X509Certificate2(@"C:\client1.WiredBrainCoffee.pfx", "ADifferentStrongPassword!");

        var handler = new HttpClientHandler();
        handler.ClientCertificates.Add(clientCertificate);

        using var client = new HttpClient(handler);

        Guid paymentID = Guid.NewGuid();

        HttpResponseMessage response = await client.GetAsync(ApiUrl + paymentID.ToString());
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response Status: {response.StatusCode}");
        Console.WriteLine($"Response Body: {responseBody}");
        Console.Read();
    }
}
