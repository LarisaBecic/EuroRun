using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class RaceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] RaceAddVM RaceAdd)
        {
            var NewRace = new Race
            {
                Name = RaceAdd.Name,
                Event_id = RaceAdd.Event_id,
                Length= RaceAdd.Length,

            };
            await _context.Races.AddAsync(NewRace);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, [FromBody] RaceUpdateVM RaceUpdate)
        {
            var race = await _context.Races.FindAsync(id);

            if (race != null)
            {
                race.Name = RaceUpdate.Name;
                race.Event_id = RaceUpdate.Event_id;
                race.Length = RaceUpdate.Length;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Race not found");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var race = await _context.Races.FindAsync(id);

            if (race != null)
            {
                _context.Races.Remove(race);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Race not found");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _context.Races.FirstOrDefaultAsync(g => g.Id == id));
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetAll()
        {
            var races = await _context.Races.Include("Event").ToListAsync();

            return Ok(races);
        }
    }
}
