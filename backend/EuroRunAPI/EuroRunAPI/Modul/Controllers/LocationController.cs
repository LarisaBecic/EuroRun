using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] LocationAddVM LocationAdd)
        {
            var NewLocation = new Location
            {
                Name = LocationAdd.Name,
                Longitude= LocationAdd.Longitude,
                Latitude = LocationAdd.Latitude,
                City_id = LocationAdd.City_id,

            };
            await _context.Locations.AddAsync(NewLocation);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, [FromBody] LocationUpdateVM LocationUpdate)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location != null) {
                location.Name = LocationUpdate.Name;
                location.Longitude = LocationUpdate.Longitude;
                location.Latitude = LocationUpdate.Latitude;
                location.City_id = LocationUpdate.City_id;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Location not found");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var location = await _context.Locations.FindAsync(id);

            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Location not found");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Location>>> GetAll()
        {
            var locations = await _context.Locations.Include(l => l.City).ThenInclude(c => c.Country).ToListAsync();

            return Ok(locations);
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _context.Locations.FirstOrDefaultAsync(g => g.Id == id));
        }
    }
}
