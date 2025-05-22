using Microsoft.AspNetCore.Mvc;

namespace LocationTrackerCLIENT.Controllers
{
    public class UserController : Controller
    {
        public IActionResult CreateClientUser()
        {
            return View();
        }

        public IActionResult CreateSystemUser()
        {
            return View();
        }
    }
}
