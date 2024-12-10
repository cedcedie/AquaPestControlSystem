using Microsoft.AspNetCore.Mvc;

namespace AquaPestControlSystem.Controllers
{
    public class TechnicianController : Controller
    {
        public IActionResult TechnicianDashboard()
        {
            return View();
        }

        public IActionResult TechnicianRequest()
        {
            return View();
        }

        public IActionResult TechnicianSchedule()
        {
            return View();
        }
        public IActionResult TechnicianReports()
        {
            return View();
        }
        public IActionResult TechnicianProfile()
        {
            return View();
        }
    }
}
