using EuroRunAPI.Authentification.Helpers;
using EuroRunAPI.Authentification.Models;
using EuroRunAPI.Authentification.ViewModels;
using EuroRunAPI.Data;
using EuroRunAPI.Helpers;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static EuroRunAPI.Authentification.Helpers.MyAuthTokenExtension;

namespace EuroRunAPI.Authentification.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthentificationController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher _passwordHasher;
        //private readonly EmailService _emailService;
        public AuthentificationController(ApplicationDbContext dbContext /*, EmailService emailService*/)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher();
            //_emailService = emailService;
        }

        [HttpPost]
        public ActionResult<LoginInfo> Login([FromBody] LoginVM login)
        {
            UserAccount loggedInAccount = _dbContext.UserAccounts.Include("Role").Include(ua=>ua.Gender)
            .FirstOrDefault(u => u.UserName != null && u.UserName == login.UserName);

            if (loggedInAccount == null ||
                _passwordHasher.VerifyHashedPassword(loggedInAccount, loggedInAccount.Password, login.Password) != PasswordVerificationResult.Success)
            {
                return new LoginInfo(null);
            }

            if(!loggedInAccount.Active)
            {
                return StatusCode(410, "User account is inactive.");
            }

            string randomString = TokenGenerator.Generate(10);

            var newToken = new AuthentificationToken()
            {
                IpAddress = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Value = randomString,
                UserAccount = loggedInAccount,
                TimeOfLogin = DateTime.Now
            };

            _dbContext.Add(newToken);
            _dbContext.SaveChanges();

            var userGet = new UserAccountGetVM
            {
                Id = loggedInAccount.Id,
                FirstName = loggedInAccount.FirstName,
                LastName = loggedInAccount.LastName,
                PhoneNumber = loggedInAccount.PhoneNumber,
                Email = loggedInAccount.Email,
                UserName = loggedInAccount.UserName,
                Picture = loggedInAccount.Picture != null ? Convert.ToBase64String(loggedInAccount.Picture) : null,
                Active = loggedInAccount.Active,
                DateOfBirth = loggedInAccount.DateOfBirth,
                Gender = loggedInAccount.Gender,
                Role = loggedInAccount.Role
            };

            var tokenGet = new AuthentificationTokenGetVM()
            {
                IpAddress = newToken.IpAddress,
                Value = newToken.Value,
                UserAccount = userGet,
                TimeOfLogin = newToken.TimeOfLogin
            };

            return new LoginInfo(tokenGet);

        }

        [HttpPost]
        public ActionResult LogOut()
        {
            AuthentificationToken AuthentificationToken = HttpContext.GetAuthToken();

            if(AuthentificationToken == null)
            {
                return Ok();
            }

            _dbContext.Remove(AuthentificationToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AuthentificationTokenGetVM> Get()
        {
            AuthentificationTokenGetVM AuthentificationToken = HttpContext.GetAuthTokenVM();

            return AuthentificationToken;
        }
    }
}
