using EuroRunAPI.Data;
using EuroRunAPI.Modul.Models;
using EuroRunAPI.Modul.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EuroRunAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class GradController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GradController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task <ActionResult> Add([FromBody] GradAddVM GradAdd)
        {
            var NoviGrad = new Grad
            {
                Naziv = GradAdd.Naziv,

            };
            await _context.Gradovi.AddAsync(NoviGrad);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _context.Gradovi.FirstOrDefaultAsync(g=>g.Id==id));
        }

        [HttpGet]
        public async Task<ActionResult<List<Grad>>> GetAll ()
        {
            var gradovi=await _context.Gradovi.ToListAsync();
            return Ok(gradovi);
        }
    }
}
