using Newtonsoft.Json;
using System.Net.Http.Headers;
using WiredBrainCoffeeClient.Data;

namespace WiredBrainCoffeeClient
{
    public partial class frmStaffList : Form
    {
        private string jwt = "";
        public frmStaffList()
        {
            InitializeComponent();
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtStaffList.Text = "";
            List<Staff>? staffData = await GetStaffData();
            foreach (Staff staff in staffData)
            {
                txtStaffList.Text += staff.FirstName + " " + staff.LastName + "\r\n";
            }
        }

        private async Task<List<Staff>?> GetStaffData()
        {
            var handler = new HttpClientHandler();
            // Disable SSL certificate validation only in Development
#if DEBUG
            handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true;
#endif

            HttpClient client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:7182/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Staff");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                List<Staff>? resultContent = JsonConvert.DeserializeObject<List<Staff>>(data);
                return resultContent;
            }
            else
            {
                return null;
            }
        }
    }
}
