using Calculation.API.Model;
using Calculation.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Calculation.API.Controllers
{
    /// <summary>
    /// Doese simple Calculations
    /// </summary>
    
    //[ApiController]
    [Route("/api/[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculation _calculation;
        private readonly ILogger<CalculationController> _logger;

        public CalculationController(ICalculation calculation,ILogger<CalculationController> logger)
        {
            _calculation = calculation;
            _logger = logger;
        }

        /// <summary>
        /// Add 2 interger numbers.
        /// </summary>
        /// <param name="value">Takes 2 interger numbers.</param>
        /// <returns>Sum of input number</returns>
        [HttpPost]
        [Route("/api/[controller]/add")]
        public IActionResult Add([FromBody] Values value)
        {
            try
            {
                if (String.IsNullOrEmpty(value.Value1) || String.IsNullOrEmpty(value.Value2))
                {
                    _logger.LogError("Value missing.");
                    return BadRequest();
                }
                var result = _calculation.Addition(value.Value1.Trim(), value.Value2.Trim());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

    }
}
