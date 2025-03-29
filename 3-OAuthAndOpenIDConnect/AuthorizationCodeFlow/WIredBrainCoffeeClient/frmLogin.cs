using Auth0.OidcClient;
using System.Net.Http.Headers;

namespace WiredBrainCoffeeClient
{
    public partial class frmLogin : Form
    {
        private static readonly string _wiredBrainCoffeeApiUrl = "https://localhost:7212/";
        private string _accessToken = "";
        private string _refreshToken = "";
        private string _identityToken = "";

        private Auth0Client _auth0Client;

        public frmLogin()
        {
            InitializeComponent();

            Auth0ClientOptions clientOptions = new Auth0ClientOptions
            {
                Domain = "dev-frc3e1y1xb0btl25.us.auth0.com",
                ClientId = "FwzLPT6UsAXqwFSarDHG1Qs3Vb4dXfZr",
                Scope = "openid offline_access"
            };

            _auth0Client = new Auth0Client(clientOptions);
        }

        private async void frmLogin_Load(object sender, EventArgs e)
        {
            await LoginAsync();

            HttpResponseMessage response = await GetStaffDataAsync();

            string data = "";
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await RefreshAccessTokenAsync();
                response = await GetStaffDataAsync();

                //...if (!response.IsSuccessStatusCode)
                //          throw an error
            }
        }

        private async Task LoginAsync()
        {
            var extraParameters = new Dictionary<string, string>();
            extraParameters.Add("audience", "WiredBrainCoffeeAPI");

            var loginResult = await _auth0Client.LoginAsync(extraParameters: extraParameters);
            _accessToken = loginResult.AccessToken;
            _identityToken = loginResult.IdentityToken;
            _refreshToken = loginResult.RefreshToken;
        }

        private async Task RefreshAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_refreshToken))
            {
                var refreshResult = await _auth0Client.RefreshTokenAsync(_refreshToken);
                _accessToken = refreshResult.AccessToken;
                _refreshToken = refreshResult.RefreshToken;
                _identityToken = refreshResult.IdentityToken;
            }
        }

        private async Task<HttpResponseMessage> GetStaffDataAsync()
        {
            var handler = new HttpClientHandler();

            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri(_wiredBrainCoffeeApiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _accessToken);

            return await client.GetAsync("Data");
        }
    }
}
