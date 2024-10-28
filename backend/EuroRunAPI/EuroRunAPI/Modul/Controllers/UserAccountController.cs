using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class UserAccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserAccountController(ApplicationDbContext context)
        {
            _context = context;
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
                Picture = UserAccountAdd.Picture,
                Active = UserAccountAdd.Active,
                Role_id = UserAccountAdd.Role_id,
            };

    
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
    }
}
