using EuroRunAPI.Data;
using EuroRunAPI.Helpers;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Stripe;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class EventRegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventRegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EventRegistrationAddVM eventRegistrationAdd)
        {
            Payment? payment = await _context.Payments.FirstOrDefaultAsync(p => p.StripePaymentIntendId == eventRegistrationAdd.StripePaymentIntentId);

            if (payment == null)
            {
                return BadRequest("Payment doesn't exist.");
            }

            var newPaymentStatus = await PaymentHelper.VerifyPaymentIntentAsync(eventRegistrationAdd.StripePaymentIntentId);

            payment.Status = newPaymentStatus;
            await _context.SaveChangesAsync();

            if (newPaymentStatus != "succeeded")
            {
                if (string.IsNullOrEmpty(eventRegistrationAdd.StripeError))
                {
                    return BadRequest("Payment not successful - unknown error. Please try again.");
                }
                else
                {
                    return BadRequest("Payment not successful - Details: " + eventRegistrationAdd.StripeError + " Please try again.");
                }   
            }

            var newEventRegistration = new EventRegistration
            {
                RegistrationDate = DateTime.Now,
                UserAccount_id = eventRegistrationAdd.UserAccount_id,
                Event_id = eventRegistrationAdd.Event_id,
                Club = eventRegistrationAdd.Club,
                ShirtSize = eventRegistrationAdd.ShirtSize,
                NumberOfFinishedRaces = eventRegistrationAdd.NumberOfFinishedRaces,
                EventDiscoverySource = eventRegistrationAdd.EventDiscoverySource,
                Note = eventRegistrationAdd.Note,
                Payment_id = payment.Id
            };

            await _context.EventRegistrations.AddAsync(newEventRegistration);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<EventRegistration>>> GetAll()
        {
            List <EventRegistration> eventRegistrations = await _context.EventRegistrations.Include(er=> er.UserAccount).Include(er=> er.Event).ToListAsync();

            return Ok(eventRegistrations);
        }

        [HttpGet("userAccountId")]
        public async Task<ActionResult<List<EventRegistrationGetVM>>> GetByUserId(int userAccountId)
        {
            List<EventRegistration> eventRegistrations = await _context.EventRegistrations
                .Include(er => er.UserAccount)
                .Include(er => er.Event).ThenInclude(e => e.Location).ThenInclude(l => l.City).ThenInclude(c => c.Country)
                .Include(er => er.Event).ThenInclude(e => e.EventType)
                .Where(er => er.UserAccount_id == userAccountId)
                .ToListAsync();

            if (eventRegistrations != null)
            {
                List<EventRegistrationGetVM> eventRegistrationsGet = new List<EventRegistrationGetVM>();

                foreach (var eventRegistration in eventRegistrations)
                {
                    EventRegistrationGetVM eventRegistrationGet = new EventRegistrationGetVM
                    {         
                        Id = eventRegistration.Id,
                        RegistrationDate = eventRegistration.RegistrationDate,
                        UserAccount = eventRegistration.UserAccount,
                        Event = eventRegistration.Event,
                        Club = eventRegistration.Club,
                        ShirtSize = eventRegistration.ShirtSize,
                        NumberOfFinishedRaces = eventRegistration.NumberOfFinishedRaces,
                        EventDiscoverySource = eventRegistration.EventDiscoverySource,
                        Note = eventRegistration.Note
                    };

                    eventRegistrationsGet.Add(eventRegistrationGet);
                }
                
                return Ok(eventRegistrationsGet);
            }
            else
            {
                return BadRequest("Event registrations for this user not found!");
            }
        }
    }
}
