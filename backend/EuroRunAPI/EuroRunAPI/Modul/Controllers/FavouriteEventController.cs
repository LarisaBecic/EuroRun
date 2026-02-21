using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class FavouriteEventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FavouriteEventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FavouriteEvent>>> GetAll()
        {
            var favouriteEvents = await _context.FavouriteEvents.ToListAsync();

            return Ok(favouriteEvents);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] FavouriteEventGetAddVM favouriteEventNew)
        {
            var favouriteEvents = await _context.FavouriteEvents.Where(fe => fe.User_Id == favouriteEventNew.User_Id && 
            fe.Event_Id == favouriteEventNew.Event_Id).ToListAsync();

            if (favouriteEvents.Count != 0)
            {
                return BadRequest("This user has already favourited this event!");
            }

            FavouriteEvent favouirteEvent = new FavouriteEvent 
             { 
                User_Id = favouriteEventNew.User_Id,
                Event_Id = favouriteEventNew.Event_Id
            };

            await _context.FavouriteEvents.AddAsync(favouirteEvent);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] FavouriteEventGetAddVM favouriteEventNew)
        {
            var favouriteEvent = await _context.FavouriteEvents.FirstOrDefaultAsync(fe => fe.User_Id == favouriteEventNew.User_Id &&
            fe.Event_Id == favouriteEventNew.Event_Id);

            if (favouriteEvent == null)
            {
                return BadRequest("This user doesn't have this event as a favourite!");
            }

            _context.FavouriteEvents.Remove(favouriteEvent);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("userId")]
        public async Task<ActionResult<List<FavouriteEventGetAddVM>>> GetByUserId(int userId)
        {
            List<FavouriteEvent> favouriteEvents = await _context.FavouriteEvents.Where(fe => fe.User_Id == userId).ToListAsync();

            if (favouriteEvents.Count() != 0)
            {
                List<FavouriteEventGetAddVM> favouriteEventsGet = new List<FavouriteEventGetAddVM>();

                foreach (var favEvent in favouriteEvents)
                {
                    FavouriteEventGetAddVM favEventGet = new FavouriteEventGetAddVM 
                    {
                        User_Id = favEvent.User_Id,
                        Event_Id = favEvent.Event_Id
                    };

                    favouriteEventsGet.Add(favEventGet);
                }

                return Ok(favouriteEventsGet);
            }
            else
            {
                return BadRequest($"This user has no favouirte events!");
            }
        }
    }
}
