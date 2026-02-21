using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroRunAPI.Helpers;
using EuroRunAPI.Authentification.Helpers;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class UserAccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher _passwordHasher;
        private readonly VerifyCaptchaHelper _verifyCaptchaHelper;

        public UserAccountController(ApplicationDbContext context, VerifyCaptchaHelper verifyCaptchaHelper)
        {
            _context = context;
            _passwordHasher = new PasswordHasher();
            _verifyCaptchaHelper = verifyCaptchaHelper;
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserAccountAddVM UserAccountAdd)
        {
            var usernameNew = UserAccountAdd.UserName.Trim().ToLower(); 

            bool exists = await _context.UserAccounts
                .AnyAsync(u => u.UserName.Trim().ToLower() == usernameNew);

            if (exists)
                return Conflict("This username is already taken!");

            var emailNew = UserAccountAdd.Email.Trim().ToLower();

            bool existsMail = await _context.UserAccounts
                .AnyAsync(u => u.Email.Trim().ToLower() == emailNew);

            if (existsMail)
                return Conflict("This email is already taken!");

            var isCaptchaValid = await _verifyCaptchaHelper.VerifyCaptcha(UserAccountAdd.CaptchaToken);

            if (!isCaptchaValid)
                return BadRequest("Captcha validation failed!");

            var NewUserAccount = new UserAccount
            {
                FirstName = UserAccountAdd.FirstName,
                LastName = UserAccountAdd.LastName,
                PhoneNumber = UserAccountAdd.PhoneNumber,
                Email = UserAccountAdd.Email,
                UserName = UserAccountAdd.UserName,
                Picture = UserAccountAdd.Picture != null ? Convert.FromBase64String(UserAccountAdd.Picture) : null,
                Active = UserAccountAdd.Active,
                DateOfBirth = UserAccountAdd.DateOfBirth,
                Gender_id = UserAccountAdd.Gender_id,
                Role_id = UserAccountAdd.Role_id,
            };

            string hashedPassword = _passwordHasher.HashPassword(NewUserAccount, UserAccountAdd.Password);
            NewUserAccount.Password = hashedPassword;
    
            await _context.UserAccounts.AddAsync(NewUserAccount);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<UserAccount>>> GetAll()
        {
            var useraccounts = await _context.UserAccounts.Include("Role").ToListAsync();

            return Ok(useraccounts);
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var user = await _context.UserAccounts.Include("Role").Include("Gender").FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                var userGet = new UserAccountGetVM {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    UserName = user.UserName,
                    Picture = user.Picture != null ? Convert.ToBase64String(user.Picture) : null,
                    Active = user.Active,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    Role = user.Role
                };

                return Ok(userGet);
            }
            else
            {
                return BadRequest("User not found");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, [FromBody] UserAccountUpdateVM UserAccountUpdate)
        {
            var user = await _context.UserAccounts.FindAsync(id);

            if (user != null)
            {
                user.FirstName = UserAccountUpdate.FirstName;
                user.LastName = UserAccountUpdate.LastName;
                user.PhoneNumber = UserAccountUpdate.PhoneNumber;
                user.Email = UserAccountUpdate.Email;
                user.UserName = UserAccountUpdate.UserName;
                user.Picture = UserAccountUpdate.Picture != null ? Convert.FromBase64String(UserAccountUpdate.Picture) : null;
                user.Active = UserAccountUpdate.Active;
                user.DateOfBirth = UserAccountUpdate.DateOfBirth;
                user.Gender_id = UserAccountUpdate.Gender_id;
                
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("User not found");
            }  
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.UserAccounts.FindAsync(id);

            if (user != null)
            {
                _context.UserAccounts.Remove(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("User not found");
            }
        }
    }
}
