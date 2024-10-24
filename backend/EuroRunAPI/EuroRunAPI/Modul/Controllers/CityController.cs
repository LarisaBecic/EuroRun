using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class CityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task <ActionResult> Add([FromBody] CityAddVM CityAdd)
        {
            var NewCity = new City
            {
                Name = CityAdd.Name,
                Country_id = CityAdd.Country_id,

            };
            await _context.Cities.AddAsync(NewCity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _context.Cities.FirstOrDefaultAsync(g=>g.Id==id));
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetAll ()
        {
            var cities=await _context.Cities.Include("Country").ToListAsync();
            
            return Ok(cities);
        }
    }
}
