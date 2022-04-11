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
                if (BigInteger.TryParse(value.Value1, out BigInteger convertedInt1) && BigInteger.TryParse(value.Value2, out BigInteger convertedInt2))
                {
                    var result = _calculation.Add(convertedInt1, convertedInt2);
                    return Ok(result.ToString());
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
