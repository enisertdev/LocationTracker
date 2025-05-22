using Microsoft.AspNetCore.Mvc;

namespace LocationTrackerCLIENT.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
