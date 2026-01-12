using EuroRunAPI.Modul.Models;
using Microsoft.AspNetCore.Identity;

namespace EuroRunAPI.Helpers
{
    public class PasswordHasher
    {
        private readonly IPasswordHasher<UserAccount> _passwordHasher;

        public PasswordHasher()
        {
            _passwordHasher = new PasswordHasher<UserAccount>();
        }

        public string HashPassword(UserAccount user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(UserAccount user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }
}