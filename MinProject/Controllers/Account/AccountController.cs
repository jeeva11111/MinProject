using Microsoft.AspNetCore.Mvc;
using MinProject.Data;
using MinProject.Functions.AccountFunctions;
using MinProject.Models;
using System;
using System.Linq;

namespace MinProject.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IFunctions _functions;
        private readonly ApplicationDbContext _context;

        public AccountController(IFunctions functions, ApplicationDbContext context)
        {
            _functions = functions;
            _context = context;
        }

 

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var isValid = _functions.IsValidUser(login.Email, login.Password);
            if (isValid)
            {
                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError("", "Invalid user inputs");
            return View(login);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var isExisting = (from x in _context.Users
                                  where x.Email == register.Email
                                  select new { email = x }).SingleOrDefault();

                if (isExisting == null)
                {
                    string passwordSalt = _functions.GenerateSalt();
                    string passwordHash = _functions.HashPassword(register.Password, passwordSalt);

                    _context.Users.Add(new User()
                    {
                        Email = register.Email,
                        Name = register.UserName,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                     
                    });

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Registration successful. Please login.";

                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError("", "User already exists.");
            }

            return View(register);
        }
    }
}
