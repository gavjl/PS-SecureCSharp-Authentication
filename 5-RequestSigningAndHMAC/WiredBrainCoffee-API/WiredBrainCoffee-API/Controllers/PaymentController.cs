using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffee_API.Data;
using WiredBrainCoffee_API.Utilities;

namespace WiredBrainCoffee_API.Controllers
{

    //We should clearly have some other form of authorization here too!

    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Payment")]
        public ActionResult CreatePayment(PaymentRequest paymentRequest)
        {
            if (!Request.Headers.TryGetValue("X-Signature", out var xSignatureHeaderValue))
                return BadRequest("Missing X-Signature header");

            var requestSignature = xSignatureHeaderValue.FirstOrDefault();
            if (string.IsNullOrEmpty(requestSignature))
                return BadRequest("Empty X-Signature header");

            // The secretKey for the current user - stored encrypted in the database
            string secretKey = "Plaintext copy of this user's secret";

            var data = $"{paymentRequest.PaymentId}|{paymentRequest.AccountNumber}" +
                $"|{paymentRequest.SortCode}|{paymentRequest.Amount}";

            if (!HmacUtils.VerifyHmac(secretKey, data, requestSignature))
                return BadRequest();

            if (!HmacUtils.ValidNonce(paymentRequest.Nonce))
                return BadRequest("Invalid nonce");

            return Ok("Success");

        }

    }
}

