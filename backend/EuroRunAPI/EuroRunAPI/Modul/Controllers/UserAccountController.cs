using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EuroRunAPI.Helpers;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class UserAccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher _passwordHasher;

        public UserAccountController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher();
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserAccountAddVM UserAccountAdd)
        {
            var NewUserAccount = new UserAccount
            {
                FirstName = UserAccountAdd.FirstName,
                LastName = UserAccountAdd.LastName,
                PhoneNumber = UserAccountAdd.PhoneNumber,
                Email = UserAccountAdd.Email,
                UserName = UserAccountAdd.UserName,
                Picture = UserAccountAdd.Picture != null ? Convert.FromBase64String(UserAccountAdd.Picture) : null,
                Active = UserAccountAdd.Active,
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
            var user = await _context.UserAccounts.Include("Role").FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                var userGet = new UserAccountGetVM {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    UserName = user.UserName,
                    Picture = user.Picture != null ? Convert.ToBase64String(user.Picture) : null,
                    Active = user.Active,
                    Role = user.Role.Name
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
                user.Role_id = UserAccountUpdate.Role_id;
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
