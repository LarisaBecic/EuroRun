using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FindMyRouteAPI.Modul.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LogoController : ControllerBase
    {
       
        public LogoController()
        {
            
        }
        [HttpGet]
        public ActionResult Get()
        {
            byte[]? logo = System.IO.File.ReadAllBytes("Images/logo1.png");
            if (logo == null)
                throw new Exception();
            return File(logo, "image/png");
        }
    }
}
