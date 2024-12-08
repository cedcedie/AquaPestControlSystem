using AquaPestControlSystem.DAL;
using AquaPestControlSystem.Models;
using AquaPestControlSystem.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult ProprieterTechnicians()
        {
            return View();
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

        public IActionResult ProprieterAppointments()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProprieterAddCustomer(CustomerViewModel customerData)
        {
            try
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

                    TempData["successMessage"] = " Customer created successfully.";
                    return RedirectToAction("ProprieterCustomer");
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
        public IActionResult ProprieterAddTechnician()
        {
            return View();
        }
        public IActionResult ProprieterEditTechnician()
        {
            return View();
        }

        public IActionResult ProprieterViewReport()
        {
            return View();
        }

        public IActionResult ProprieterViewArchives()
        {
            return View();
        }

        public IActionResult ProprieterAddAppointment()
        {
            return View();
        }

        public IActionResult ProprieterViewAppointments()
        {
            return View();
        }


        public IActionResult ProprieterRequestAppointments()
        {
            return View();
        }
    }
}
