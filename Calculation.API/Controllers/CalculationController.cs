using Calculation.API.Model;
using Calculation.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Calculation.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculation _calculation;
        public CalculationController(ICalculation calculation)
        {
            _calculation = calculation;
        }


        [HttpPost]
        [Route("/api/[controller]/add")]
        public IActionResult Add([FromBody] Values value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Value1) && String.IsNullOrEmpty(value.Value2))
                {
                    return BadRequest();
                }
                var result = _calculation.Addition(value.Value1.Trim(), value.Value2.Trim());
                return Ok(result.ToString());

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
