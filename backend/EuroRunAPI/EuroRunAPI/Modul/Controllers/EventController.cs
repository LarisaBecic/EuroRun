﻿using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
