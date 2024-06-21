using Microsoft.AspNetCore.Mvc;
using MinProject.Data;
using MinProject.Functions.AccountFunctions;
using MinProject.Models;
using NuGet.Common;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace MinProject.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IFunctions _functions;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public AccountController(IFunctions functions, ApplicationDbContext context, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _functions = functions;
            _context = context;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var userId = _contextAccessor.HttpContext.Session.GetString("UserId");

            var user = _context.Users.SingleOrDefault(u => u.Email == login.Email);
            if (user != null)
            {
                var token = JwtTokenGen.JwtHelper.GenerateJwtToken(login, _configuration);

                _contextAccessor.HttpContext.Session.SetString("JwtToken", token);
                _contextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
                _contextAccessor.HttpContext.Session.SetString("UserName", user.Name);
                Console.WriteLine($"Generated JWT Token: {token}");
            }

            var isValid = _functions.IsValidUser(login.Email, login.Password);
            if (isValid)
            {
                return RedirectToAction("Index", "Products");
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


        [HttpGet]
        public IActionResult LogOut()
        {
            _contextAccessor.HttpContext.Session.Clear();
            Response.Cookies.Delete("Logger");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult localPort()
        {
            ModelState.AddModelError("", "unable to read");
            return Ok("unable to read");
        }

    }
}
