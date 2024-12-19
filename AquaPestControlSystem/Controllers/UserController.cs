﻿using AquaPestControlSystem.DAL;
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

        public IActionResult UserMakeAppointment()
        {
            return View();
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
                return RedirectToAction("UserDashboard");
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
