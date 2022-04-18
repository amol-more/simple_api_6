using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;

namespace Calculation.Test
{
    public class TestCalculationAPI
    {
        private readonly API.Controllers.CalculationController _calculationController;

        public TestCalculationAPI()
        {
            //Pobably use MOQ here Like logger = Mock.Of<ILogger<API.Controllers.CalculationController>>()
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<API.Controllers.CalculationController>();

            _calculationController = new API.Controllers.CalculationController(new API.Services.Calculation(),logger);
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
