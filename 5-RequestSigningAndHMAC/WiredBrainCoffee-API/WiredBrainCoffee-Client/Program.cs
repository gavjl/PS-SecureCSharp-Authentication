using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using WiredBrainCoffee_API.Data;

class Program
{
    private static readonly string ApiUrl = "https://localhost:7165/Payment";
    private static readonly string SecretKey = "Plaintext copy of this user's secret";

    static async Task Main(string[] args)
    {
        // Create the request payload
        PaymentRequest paymentRequest = new PaymentRequest
        {
            PaymentId = Guid.NewGuid(),
            AccountNumber = "12345678",
            SortCode = "123456",
            Amount = 999.99m,
            Nonce = Guid.NewGuid(),
        };
        
        var hmacData = $"{paymentRequest.PaymentId}|{paymentRequest.AccountNumber}" +
            $"|{paymentRequest.SortCode}|{paymentRequest.Amount}";

        // Generate HMAC signature
        string signature = GenerateHmac(hmacData, SecretKey);

        string jsonData = JsonSerializer.Serialize(paymentRequest);
        await SendSignedRequest(jsonData, signature);
    }

    static string GenerateHmac(string data, string secretKey)
    {
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    static async Task SendSignedRequest(string jsonData, string signature)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Signature", signature);

        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response Status: {response.StatusCode}");
        Console.WriteLine($"Response Body: {responseBody}");
        Console.Read();
    }
}
