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

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, [FromBody] CityUpdateVM CityUpdate)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city != null)
            {
                city.Name = CityUpdate.Name;
                city.Country_id = CityUpdate.Country_id;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("City not found");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("City not found");
            }
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
