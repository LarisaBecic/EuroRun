using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ReviewAddVM ReviewAdd)
        {
            var NewReview = new Review
            {
                Rating = ReviewAdd.Rating,
                Comment = ReviewAdd.Comment,
                User_id = ReviewAdd.User_id,
                Event_id = ReviewAdd.Event_id
            };
            await _context.Reviews.AddAsync(NewReview);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Update(int id, [FromBody] ReviewUpdateVM ReviewUpdate)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                review.Rating = ReviewUpdate.Rating;
                review.Comment = ReviewUpdate.Comment;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Review not found");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest("Review not found");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            var review = await _context.Reviews.Include("User").Include("Event").FirstOrDefaultAsync(r => r.Id == id);

            if (review != null)
            {
                var ReviewGet = new ReviewGetVM
                {
                    Rating = review.Rating,
                    Comment = review.Comment,
                    User = review.User.UserName,
                    Event = review.Event.Name
                };

                return Ok(ReviewGet);
            }
            else
            {
                return BadRequest("Review not found");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetAll()
        {
            var cities = await _context.Reviews.ToListAsync();

            return Ok(cities);
        }
    }
}
