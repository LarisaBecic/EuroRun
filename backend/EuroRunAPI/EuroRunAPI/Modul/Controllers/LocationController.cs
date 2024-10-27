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

        [HttpGet]
        public async Task<ActionResult<List<Location>>> GetAll()
        {
            var locations = await _context.Locations.Include("City").ToListAsync();

            return Ok(locations);

        }

    }
}
