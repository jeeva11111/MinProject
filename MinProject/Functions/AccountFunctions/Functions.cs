using Microsoft.AspNetCore.Http;
using MinProject.Data;
using MinProject.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MinProject.Functions.AccountFunctions
{
    public class Functions : IFunctions
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public Functions(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public bool IsValidUser(string Email, string password)
        {
            bool isValid = false;
            var user = _context.Users.SingleOrDefault(u => u.Email == Email.ToLower());

            if (user != null)
            {
                var hash = HashPassword(password, user.PasswordSalt);
                if (user.PasswordHash == hash)
                {
                    _contextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());
                    _contextAccessor.HttpContext.Session.SetString("UserName", user.Name);
                    isValid = true;
                }
            }

            return isValid;
        }

        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        public string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hashBytes = sha256.ComputeHash(saltedPasswordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }

    public interface IFunctions
    {
        bool IsValidUser(string Email, string password);
        string GenerateSalt();
        string HashPassword(string password, string salt);
    }
}
