using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CountryAddVM CountryAdd)
        {
            var NewCountry = new Country
            {
                Name = CountryAdd.Name
            };
            await _context.Countries.AddAsync(NewCountry);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, [FromBody] CountryUpdateVM CountryUpdate)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country != null) {
                country.Name = CountryUpdate.Name;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Country not found");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Country not found");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAll()
        {
            var countries = await _context.Countries.ToListAsync();

            return Ok(countries);
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _context.Countries.FirstOrDefaultAsync(g => g.Id == id));
        }
    }
}
