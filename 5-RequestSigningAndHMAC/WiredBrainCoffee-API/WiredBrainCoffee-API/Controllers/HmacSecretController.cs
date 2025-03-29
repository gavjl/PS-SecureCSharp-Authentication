using Microsoft.AspNetCore.Mvc;

namespace WiredBrainCoffee_API.Controllers
{
    public class HmacSecretController : Controller
    {
        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateHmacSecret()
        {
            //generate a secret for the current user
            // ** Share the secret only once**
            //Store it encrypted in the database
            return Json(new { HmacSecret = "Plaintext copy of this user's secret" });
        }

    }
}
