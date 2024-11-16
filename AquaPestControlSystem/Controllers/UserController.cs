using Microsoft.AspNetCore.Mvc;

namespace AquaPestControlSystem.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserDashboard()
        {
            return View();
        }

        public IActionResult UserMakeAppointment()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }
    }
}
