namespace WiredBrainCoffee_API.Data
{
    public class PaymentRequest
    {
        public PaymentRequest() { }

        public PaymentRequest(string paymentId) { }

        public Guid PaymentId { get; set; }

        public string AccountNumber { get; set; }

        public string SortCode { get; set; }

        public decimal Amount { get; set; }

        public Guid Nonce { get; set; }

    }
}
