using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class EventTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EventTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventType>>> GetAll()
        {
            var evetTypes = await _context.EventTypes.ToListAsync();

            return Ok(evetTypes);
        }

    }
}
