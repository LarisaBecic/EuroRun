using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EventAddVM EventAdd)
        {
            var NewEvent = new Event
            {
                Name = EventAdd.Name,
                Location_id = EventAdd.Location_id,
                EventType_id = EventAdd.EventType_id,
                DateTime=EventAdd.DateTime,   
                Description = EventAdd.Description,
                RegistrationDeadline = EventAdd.RegistrationDeadline,

            };
            await _context.Events.AddAsync(NewEvent);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, [FromBody] EventUpdateVM EventUpdate)
        {
            var Event = await _context.Events.FindAsync(id);

            if (Event != null)
            {
                Event.Name = EventUpdate.Name;
                Event.Location_id = EventUpdate.Location_id;
                Event.EventType_id = EventUpdate.EventType_id;
                Event.DateTime = EventUpdate.DateTime;
                Event.Description = EventUpdate.Description;
                Event.RegistrationDeadline = EventUpdate.RegistrationDeadline;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Event not found");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var Event = await _context.Events.FindAsync(id);

            if (Event != null)
            {
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Event not found");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _context.Events.FirstOrDefaultAsync(g => g.Id == id));
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAll()
        {
            var events = await _context.Events.Include("Location").Include("EventType").ToListAsync();

            return Ok(events);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Event>>> SearchEvents(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("Query cannot be empty");
            }

            var events = await _context.Events
                .Where(c => c.Location.City.Name.Contains(city))
                .Include(c => c.Location).ThenInclude(l=>l.City)
                .Include(c=>c.EventType)
                .ToListAsync();

            return Ok(events);
        }
    }
}
