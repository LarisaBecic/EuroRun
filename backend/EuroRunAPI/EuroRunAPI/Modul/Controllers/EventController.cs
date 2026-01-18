using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using FindMyRouteAPI.Helper;
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
            byte[]? PictureBytes = EventAdd.Picture != null ? Convert.FromBase64String(EventAdd.Picture) : null;

            if (PictureBytes != null)
            {
                PictureBytes = Images.Resize(PictureBytes, 550);
            }

            var NewEvent = new Event
            {
                Name = EventAdd.Name,
                Location_id = EventAdd.Location_id,
                EventType_id = EventAdd.EventType_id,
                DateTime=EventAdd.DateTime,   
                Description = EventAdd.Description,
                RegistrationDeadline = EventAdd.RegistrationDeadline,
                Picture = PictureBytes
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
                byte[]? PictureBytes = EventUpdate.Picture != null ? Convert.FromBase64String(EventUpdate.Picture) : null;

                if (PictureBytes != null)
                {
                    PictureBytes = Images.Resize(PictureBytes, 450);
                }

                Event.Name = EventUpdate.Name;
                Event.Location_id = EventUpdate.Location_id;
                Event.EventType_id = EventUpdate.EventType_id;
                Event.DateTime = EventUpdate.DateTime;
                Event.Description = EventUpdate.Description;
                Event.RegistrationDeadline = EventUpdate.RegistrationDeadline;
                Event.Picture = PictureBytes;
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
        public async Task<ActionResult<EventGetVM>> GetById(int id)
        {
            Event? eventDb = await _context.Events.Include(e => e.EventType)
                .Include(e => e.Location).ThenInclude(l => l.City).ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (eventDb != null)
            {
                EventGetVM eventGet = new EventGetVM
                {
                    Id = eventDb.Id,
                    Name = eventDb.Name,
                    DateTime = eventDb.DateTime,
                    Description = eventDb.Description,
                    RegistrationDeadline = eventDb.RegistrationDeadline,
                    Location_id = eventDb.Location_id,
                    Location = eventDb.Location,
                    EventType = eventDb.EventType,
                    EventType_id = eventDb.EventType_id,
                    Picture = eventDb.Picture != null ? Convert.ToBase64String(eventDb.Picture) : null
                };
                return Ok(eventGet);
            }
            return BadRequest("Event doesn't exist!");
        }

        [HttpGet]
        public async Task<ActionResult<List<EventGetVM>>> GetAll()
        {
            var events = await _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Location).ThenInclude(l => l.City).ThenInclude(c => c.Country)
                .ToListAsync();

            var eventsGet = new List<EventGetVM>();

            foreach (var e in events) {
                var eventGet = new EventGetVM {
                    Id = e.Id,
                    Name = e.Name,
                    DateTime = e.DateTime,
                    Description = e.Description,
                    RegistrationDeadline = e.RegistrationDeadline,
                    Location_id = e.Location_id,
                    Location = e.Location,
                    EventType = e.EventType,
                    EventType_id = e.EventType_id,
                    Picture = e.Picture != null ? Convert.ToBase64String(e.Picture) : null
                };
                eventsGet.Add(eventGet);
            }

            return Ok(eventsGet);
        }

        [HttpGet]
        public async Task<ActionResult<List<EventGetVM>>> GetFiveEvents()
        {
            var events = await _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Location).ThenInclude(l => l.City).ThenInclude(c => c.Country)
                .Take(5)
                .ToListAsync();

            var eventsGet = new List<EventGetVM>();

            foreach (var e in events)
            {
                var eventGet = new EventGetVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    DateTime = e.DateTime,
                    Description = e.Description,
                    RegistrationDeadline = e.RegistrationDeadline,
                    Location_id = e.Location_id,
                    Location = e.Location,
                    EventType = e.EventType,
                    EventType_id = e.EventType_id,
                    Picture = e.Picture != null ? Convert.ToBase64String(e.Picture) : null
                };
                eventsGet.Add(eventGet);
            }

            return Ok(eventsGet);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<EventGetVM>>> SearchEvents(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return BadRequest("Please provide a city name!");
            }

            var events = await _context.Events
                .Where(c => c.Location.City.Name.Contains(city))               
                .Include(c=>c.EventType)
                .Include(c => c.Location).ThenInclude(l => l.City).ThenInclude(l => l.Country)
                .ToListAsync();

            var eventsGet = new List<EventGetVM>();

            foreach (var e in events)
            {
                var eventGet = new EventGetVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    DateTime = e.DateTime,
                    Description = e.Description,
                    RegistrationDeadline = e.RegistrationDeadline,
                    Location_id = e.Location_id,
                    Location = e.Location,
                    EventType = e.EventType,
                    EventType_id = e.EventType_id,
                    Picture = e.Picture != null ? Convert.ToBase64String(e.Picture) : null
                };
                eventsGet.Add(eventGet);
            }

            return Ok(eventsGet);
        }
    }
}
