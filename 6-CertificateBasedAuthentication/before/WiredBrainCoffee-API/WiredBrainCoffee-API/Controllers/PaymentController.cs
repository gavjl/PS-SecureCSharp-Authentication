using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffee_API.Data;

namespace WiredBrainCoffee_API.Controllers
{

    //We should clearly have some other form of authorization here too!

    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{paymentId}")]
        public JsonResult GetPayment(Guid paymentId)
        {
            PaymentRequest paymentRequest = new PaymentRequest
            {
                PaymentId = paymentId,
                AccountNumber = "12345678",
                SortCode = "123456",
                Amount = 999.99m
            };
            return Json(paymentRequest);
        }

    }
}

