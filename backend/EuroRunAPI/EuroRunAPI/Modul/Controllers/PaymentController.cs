using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public PaymentsController(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequest request)
        {
            Models.Event? userEvent = await _context.Events.FindAsync(request.Event_id);

            if (userEvent == null)
            {
                return BadRequest("This event doesn't exist!");
            }

            if (userEvent.RegistrationDeadline.Date <= DateTime.Now)
            {
                return BadRequest("You can only register for events that are at least 3 days away.");
            }

            var amountInCents = (long)(request.Amount * 100);

            var options = new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = "eur",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                }
            };

            var service = new PaymentIntentService();
            var intent = await service.CreateAsync(options);

            var newPayment = new Payment
            {
                Amount = request.Amount,
                StripePaymentIntendId = intent.Id,
                Status = "Initiated",
                CreatedAt = DateTime.Now
            };

            await _context.Payments.AddAsync(newPayment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                clientSecret = intent.ClientSecret
            });
        }
    }

    public class CreatePaymentIntentRequest
    {
        public int Event_id { get; set; }
        public long Amount { get; set; }
    }
}
