using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class GenderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Gender>>> GetAll()
        {
            var genders = await _context.Genders.ToListAsync();

            return Ok(genders);
        }

    }
}
