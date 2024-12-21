using AquaPestControlSystem.DAL;
using AquaPestControlSystem.Models;
using AquaPestControlSystem.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AquaPestControlSystem.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly ProprieterCustomerDBContext _context;

        public TechnicianController(ProprieterCustomerDBContext context)
        {
            _context = context;
        }


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

        [HttpGet]
        public IActionResult TechnicianReports()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TechnicianReports(ReportViewModel reportData)
        {
            try
            {
                // Validate the file
                if (reportData.ImageFile == null || reportData.ImageFile.Length == 0)
                {
                    ModelState.AddModelError("ImageFile", "Please select an image file.");
                    return View(reportData);
                }

                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(reportData.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    reportData.ImageFile.CopyTo(stream);
                }

                var report = new Report
                {
                    DateInspection = reportData.DateInspection,
                    InspectorName = reportData.InspectorName,
                    ClientName = reportData.ClientName,
                    Address = reportData.Address,
                    ContactNum = reportData.ContactNum,
                    InspectionType = reportData.InspectionType,
                    Areas = reportData.Areas,
                    TreatmentType = reportData.TreatmentType,
                    PestType = reportData.PestType,
                    SalesInput = reportData.SalesInput,
                    FileName = "~/images/" + fileName
                };

                _context.Reports.Add(report);
                _context.SaveChanges();

                return RedirectToAction("TechnicianViewReports");
            }
            catch (DbUpdateException ex)
            {
                // Log the error for debugging
                Console.WriteLine(ex.Message);

                // Add a user-friendly error message
                ModelState.AddModelError("", "An error occurred while saving your data. Please try again.");

                // Return the view with the same model to show validation errors
                return View();
            }
        }
        public IActionResult TechnicianProfile()
        {
            return View();
        }
        public IActionResult TechnicianViewReports()
        {
            return View();
        }
    }
}
