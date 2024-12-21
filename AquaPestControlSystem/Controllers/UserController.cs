using AquaPestControlSystem.DAL;
using AquaPestControlSystem.Models;
using AquaPestControlSystem.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AquaPestControlSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ProprieterCustomerDBContext _context;

        public UserController(ProprieterCustomerDBContext context)
        {
            _context = context;
        }

        public IActionResult UserDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserMakeAppointment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserMakeAppointment(AppointmentViewModel appointmentData)
        {
            try
            {
                // Validate the file
                if (appointmentData.FileImage == null || appointmentData.FileImage.Length == 0)
                {
                    ModelState.AddModelError("ImageFile", "Please select an image file.");
                    return View(appointmentData);
                }

                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(appointmentData.FileImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    appointmentData.FileImage.CopyTo(stream);
                }

                var appointment = new Appointment
                {
                    FirstName = appointmentData.FirstName,
                    LastName = appointmentData.LastName,
                    MiddleName = appointmentData.MiddleName,
                    ContactNum = appointmentData.ContactNum,
                    Email = appointmentData.Email,
                    Address = appointmentData.Address,
                    PestProblem = appointmentData.PestProblem,
                    Schedule = appointmentData.Schedule,
                    FileName = "~/images/" + fileName
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Appointment added successfully!";
                return RedirectToAction("UserMakeAppointment");
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

        public IActionResult LandingPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        public IActionResult UserLogin(AccountViewModel accountData)
        {
            var user = _context.UserAccounts.FirstOrDefault(a => a.username == accountData.username && a.password == accountData.password);

            if (user == null)
            {
                ModelState.AddModelError("UsernameOrId", "Invalid username or password.");
                return View();
            }

            if (user.role == "Proprieter")
            {
                return RedirectToAction("ProprieterDashboard", "Proprieter");
            }
            else if (user.role == "Technician")
            {
                return RedirectToAction("TechnicianDashboard", "Technician");
            }
            else if (user.role == "Customer")
            {
                return RedirectToAction("UserMakeAppointment", "User");
            }
            return View();

        }
        [HttpGet]
        public IActionResult UserCreateAccount()
        {

            return View();
        }
        [HttpPost]
        public IActionResult UserCreateAccount(AccountViewModel accountData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var account = new UserAccount
                    {
                         FirstName = accountData.FirstName,
                         LastName = accountData.LastName,
                         MiddleName = accountData.MiddleName,
                         email = accountData.email,
                         password = accountData.password,
                         role = "Customer"
                    };

                    _context.UserAccounts.Add(account);
                    _context.SaveChanges();
                    return RedirectToAction("UserLogin");
                }
                else
                {
                    TempData["errorMessage"] = " Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        public IActionResult UserForgotPassword()
        {
            return View();
        }
        public IActionResult UserServiceAnts()
        {
            return View();
        }
        public IActionResult UserServiceRoach()
        {
            return View();
        }
        public IActionResult UserServiceRodent()
        {
            return View();
        }
        public IActionResult UserServiceMosq()
        {
            return View();
        }
        public IActionResult UserServiceTermites()
        {
            return View();
        }
    }
}
