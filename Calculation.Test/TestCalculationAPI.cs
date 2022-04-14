using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Calculation.Test
{
    public class TestCalculationAPI
    {
        private readonly API.Controllers.CalculationController _calculationController;

        public TestCalculationAPI()
        {
            _calculationController = new  API.Controllers.CalculationController(new API.Services.Calculation());
        }
        
        [Fact]
        public void Test_Addition_WithTwoNumbers_ShouldAddCorrectly()
        {
            var addModel = new API.Model.Values();
            addModel.Value1 = "123";
            addModel.Value2 = "123";
            
            var result = _calculationController.Add(addModel);
            
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Test_Addition_WithEmptyValues_ShouldReturnBadRequest()
        {
            var addModel = new API.Model.Values();
            addModel.Value1 = "";
            addModel.Value2 = "";

            var result = _calculationController.Add(addModel);
            
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
