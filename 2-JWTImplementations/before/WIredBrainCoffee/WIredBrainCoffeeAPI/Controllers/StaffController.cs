using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffeeAPI;

namespace WiredBrainCoffeeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetStaff")]
        public IActionResult Get()
        {
            IEnumerable<Staff> staff = new List<Staff>
            {
                new Staff {
                    StartDate = new DateTime (2024,2,1),
                    FirstName = "Sue",
                    LastName = "Latimer"
                },
                new Staff {
                    StartDate = new DateTime (2025,2,11),
                    FirstName = "David",
                    LastName = "Jenkins"
                }
            };

            return Ok(staff);

        }
    }
}
