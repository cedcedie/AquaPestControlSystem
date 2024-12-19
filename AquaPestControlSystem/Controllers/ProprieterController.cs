using AquaPestControlSystem.DAL;
using AquaPestControlSystem.Models;
using AquaPestControlSystem.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace AquaPestControlSystem.Controllers
{
    public class ProprieterController : Controller
    {
        private readonly ProprieterCustomerDBContext _context;

        public ProprieterController(ProprieterCustomerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ProprieterCustomer()
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            var customer = _context.Customers.ToList();

            if (customer != null)
            {
                foreach (var customers in customer)
                {
                    var CustomerViewModel = new CustomerViewModel
                    {
                        CustomerId = customers.CustomerId,
                        FirstName = customers.FirstName,
                        LastName = customers.LastName,
                        MiddleName = customers.MiddleName,
                        ContactNum = customers.ContactNum,
                        Email = customers.Email,
                        Address = customers.Address
                    };
                    customerList.Add(CustomerViewModel);
                }
                return View(customerList);
            }
            return View(customerList);
        }
        [HttpGet]
        public IActionResult ProprieterTechnicians()
        {
            List<TechnicianViewModel> technicianList = new List<TechnicianViewModel>();
            var activeTechnicians = _context.Technicians
            .Where(t => t.Status == "Active") // Filter for active technicians
            .ToList();

            if (activeTechnicians != null)
            {
                foreach (var technicians in activeTechnicians)
                {
                    var TechnicianViewModel = new TechnicianViewModel
                    {
                        TechnicianId = technicians.TechnicianId,
                        FirstName = technicians.FirstName,
                        LastName = technicians.LastName,
                        MiddleName = technicians.MiddleName,
                        ContactNum = technicians.ContactNum,
                        Address = technicians.Address,
                        Status = technicians.Status
                    };
                    technicianList.Add(TechnicianViewModel);
                }
                return View(technicianList);
            }
            return View(technicianList);
        }

        public IActionResult ProprieterReports()
        {
            return View();
        }

        public IActionResult ProprieterDashboard()
        {
            return View();
        }
        public IActionResult ProprieterActivityLog()
        {
            return View();
        }

        public IActionResult ProprieterArchives()
        {
            return View();
        }


        public IActionResult ProprieterViewCustomer()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProprieterAppointments()
        {
            var appointments = _context.Appointments.ToList();
            var appointmentViewModel = appointments.Select(a => new AppointmentViewModel
            {
                AppointmentId = a.AppointmentId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                ContactNum = a.ContactNum,
                Address = a.Address,
                Email = a.Email,
                PestProblem = a.PestProblem,
                Schedule = a.Schedule,
                ImageUrl = Url.Content(a.FileName)
            }).ToList();

            return View(appointmentViewModel);
        }

        [HttpGet]
        public IActionResult ProprieterAddCustomer()
        {
            return View();
        }

        [HttpPost]
        [Route("Proprieter/ProprieterAddCustomer")]
        public IActionResult ProprieterAddCustomer(CustomerViewModel customerData)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    FirstName = customerData.FirstName,
                    LastName = customerData.LastName,
                    MiddleName = customerData.MiddleName,
                    ContactNum = customerData.ContactNum,
                    Email = customerData.Email,
                    Address = customerData.Address
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("ProprieterCustomer");
            }
            else
            {
                TempData["errorMessage"] = " Model data is not valid";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ProprieterAddTechnician()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProprieterAddTechnician(TechnicianViewModel technicianData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var technician = new Technician
                    {
                        FirstName = technicianData.FirstName,
                        LastName = technicianData.LastName,
                        MiddleName = technicianData.MiddleName,
                        ContactNum = technicianData.ContactNum,
                        Address = technicianData.Address,
                        Status = "Active"
                    };

                    _context.Technicians.Add(technician);
                    _context.SaveChanges();
                    return RedirectToAction("ProprieterTechnicians");
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

        [HttpGet("EditTechnician/{id:int}")]
        public IActionResult ProprieterEditTechnician(int id)
        {
            var technician = _context.Technicians.FirstOrDefault(a => a.TechnicianId == id);

            if (technician == null)
            {
                return NotFound();
            }

            // Create an instance of the ViewModel and map the data
            var viewModel = new TechnicianViewModel
            {
                TechnicianId = technician.TechnicianId,
                FirstName = technician.FirstName,
                ContactNum = technician.ContactNum,
                Address = technician.Address,
                MiddleName = technician.MiddleName,
                LastName = technician.LastName
            };

            return View(viewModel); // Pass the ViewModel to the view
        }

        [HttpPost("EditTechnician/{id:int}")] // Keep the route attribute
        public async Task<IActionResult> ProprieterEditTechnician(int id, TechnicianViewModel technicianData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // 1. Retrieve the existing technician from the database using the ID
                    var technician = await _context.Technicians.FindAsync(id); // Use FindAsync for async operations

                    if (technician == null)
                    {
                        return NotFound(); // Handle the case where the technician doesn't exist
                    }

                    // 2. Update the properties of the retrieved technician
                    technician.FirstName = technicianData.FirstName;
                    technician.LastName = technicianData.LastName;
                    technician.MiddleName = technicianData.MiddleName;
                    technician.ContactNum = technicianData.ContactNum;
                    technician.Address = technicianData.Address;
                    technician.Status = technicianData.Status;
                    // Do not update the TechnicianId as it is the primary key

                    // 3. EF Core tracks the changes automatically, so you don't need .Update()
                    await _context.SaveChangesAsync(); // Save the changes asynchronously

                    return RedirectToAction("ProprieterTechnicians");
                }
                else
                {
                    //If the model is invalid, return the view with the model so the user can correct the errors.
                    return View(technicianData);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View(technicianData);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(technicianData);
            }
        }

        public IActionResult ProprieterViewReport()
        {
            return View();
        }

        public IActionResult ProprieterViewArchives()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProprieterAddAppointment()
        {
            return View();
        }

        

        [HttpPost]
        public IActionResult ProprieterAddAppointment(AppointmentViewModel appointmentData)
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

                return RedirectToAction("ProprieterAppointments");
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

        [HttpGet]
        public IActionResult ProprieterViewAppointments()
        {
            var appointments = _context.Appointments.ToList();
            var appointmentViewModel = appointments.Select(a => new AppointmentViewModel
            {
                AppointmentId = a.AppointmentId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                ContactNum = a.ContactNum,
                Address = a.Address,
                Email = a.Email,
                PestProblem = a.PestProblem,
                Schedule = a.Schedule,
                ImageUrl = Url.Content(a.FileName)
            }).ToList();

            return View(appointmentViewModel);
        }


        public IActionResult ProprieterRequestAppointments()
        {
            return View();
        }

        public IActionResult ProprieterAssignAppointment()
        {
            return View();
        }

        public IActionResult ProprieterArchiveActivityLog()
        {
            return View();
        }

        public IActionResult ProprieterArchiveAppointment()
        {
            return View();
        }

        public IActionResult ProprieterArchiveCustomers()
        {
            return View();
        }

        public IActionResult ProprieterArchiveReports()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProprieterArchiveTechnicians()
        {
            List<TechnicianViewModel> technicianList = new List<TechnicianViewModel>();
            var inactiveTechnicians = _context.Technicians
            .Where(t => t.Status == "Inactive") // Filter for active technicians
            .ToList();

            if (inactiveTechnicians != null)
            {
                foreach (var technicians in inactiveTechnicians)
                {
                    var TechnicianViewModel = new TechnicianViewModel
                    {
                        TechnicianId = technicians.TechnicianId,
                        FirstName = technicians.FirstName,
                        LastName = technicians.LastName,
                        MiddleName = technicians.MiddleName,
                        ContactNum = technicians.ContactNum,
                        Address = technicians.Address,
                        Status = technicians.Status
                    };
                    technicianList.Add(TechnicianViewModel);
                }
                return View(technicianList);
            }
            return View(technicianList);
        }

        [HttpPost("RestoreTechnician/{id:int}")] // Use POST for actions that modify data
        public async Task<IActionResult> RestoreTechnician(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var technician = await _context.Technicians.FindAsync(id);

            if (technician == null)
            {
                return NotFound();
            }

            technician.Status = "Active"; // Change the status

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("ProprieterArchiveTechnicians"); // Redirect back to the archive view
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return RedirectToAction("ProprieterArchiveTechnicians");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("ProprieterArchiveTechnicians");
            }
        }

        public IActionResult ProprieterProfile()
        {
            return View();
        }

        [HttpGet("EditCustomer/{id:int}")]
        public IActionResult ProprieterEditCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(a => a.CustomerId == id);

            if (customer == null)
            {
                return NotFound();
            }

            // Create an instance of the ViewModel and map the data
            var viewModel = new CustomerViewModel
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                ContactNum = customer.ContactNum,
                Address = customer.Address,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            return View(viewModel); // Pass the ViewModel to the view
        }

        [HttpPost("EditCustomer/{id:int}")] // Keep the route attribute
        public async Task<IActionResult> ProprieterEditCustomer(int id, CustomerViewModel customerData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // 1. Retrieve the existing technician from the database using the ID
                    var customer = await _context.Customers.FindAsync(id); // Use FindAsync for async operations

                    if (customer == null)
                    {
                        return NotFound(); // Handle the case where the technician doesn't exist
                    }

                    // 2. Update the properties of the retrieved technician
                    customer.FirstName = customerData.FirstName;
                    customer.LastName = customerData.LastName;
                    customer.MiddleName = customerData.MiddleName;
                    customer.ContactNum = customerData.ContactNum;
                    customer.Address = customerData.Address;
                    customer.Email = customerData.Email;
                    // Do not update the TechnicianId as it is the primary key

                    // 3. EF Core tracks the changes automatically, so you don't need .Update()
                    await _context.SaveChangesAsync(); // Save the changes asynchronously

                    return RedirectToAction("ProprieterCustomer");
                }
                else
                {
                    //If the model is invalid, return the view with the model so the user can correct the errors.
                    return View(customerData);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View(customerData);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(customerData);
            }
        }
    }
}
