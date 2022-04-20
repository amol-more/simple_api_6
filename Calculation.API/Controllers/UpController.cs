using Microsoft.AspNetCore.Mvc;

namespace Calculation.API.Controllers
{
    [ApiController]
    public class UpController : Controller
    {
        [HttpGet]
        [Route("[controller]")]
        public IActionResult Get()
        {
            return Ok("Service Up");
        }
    }
}
