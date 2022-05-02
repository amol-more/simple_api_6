using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Moq;


namespace Calculation.Test
{
    public class TestCalculationAPI
    {
        private  API.Controllers.CalculationController _calculationController;

        public TestCalculationAPI()
        {
            

           
            
            //Pobably use MOQ here Like logger = Mock.Of<ILogger<API.Controllers.CalculationController>>()
            //var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            //var factory = serviceProvider.GetService<ILoggerFactory>();

            //var logger = factory.CreateLogger<API.Controllers.CalculationController>();

            //_calculationController = new API.Controllers.CalculationController(new API.Services.Calculation(), mockLogger.Object);
        }

        [Fact]
        public void Test_Addition_WithTwoNumbers_ShouldAddCorrectly()
        {
            var mockLogger = new Mock<ILogger<API.Controllers.CalculationController>>();
            var mockCalculationService = new Mock<API.Services.Calculation>();
            mockCalculationService.Setup(x => x.Addition(It.IsAny<string>(), It.IsAny<string>())).Returns("246");
            _calculationController = new API.Controllers.CalculationController(mockCalculationService.Object,mockLogger.Object);
            var addModel = new API.Model.Values();
            addModel.Value1 = "123";
            addModel.Value2 = "123";
            var expected = "246";

            var result =  (_calculationController.Add(addModel)) as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result?.Value?.ToString());
        }

        

        [Fact]
        public void Test_Addition_WithString_ShouldBeEmptyValue()
        {
            var mockLogger = new Mock<ILogger<API.Controllers.CalculationController>>();
            var mockCalculationService = new Mock<API.Services.ICalculation>();
            //mockCalculationService.Setup(x => x.Addition("a", "b")).Returns(string.Empty);
            mockCalculationService.Setup(x => x.Addition(It.IsAny<string>(), It.IsAny<string>())).Returns(string.Empty);
            _calculationController = new API.Controllers.CalculationController(mockCalculationService.Object, mockLogger.Object);
            var addModel = new API.Model.Values();
            addModel.Value1 = "a";
            addModel.Value2 = "b";
            var expected = string.Empty;

            var result = _calculationController.Add(addModel) as ObjectResult;

            //Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, result?.Value?.ToString());
        }

        [Fact]
        public void Test_Addition_WithEmptyValues_ShouldReturnBadRequest()
        {
            var mockLogger = new Mock<ILogger<API.Controllers.CalculationController>>();
            var mockCalculationService = new Mock<API.Services.Calculation>();
            mockCalculationService.Setup(x => x.Addition(It.IsAny<string>(), It.IsAny<string>())).Returns("");
            _calculationController = new API.Controllers.CalculationController(mockCalculationService.Object, mockLogger.Object);
            var addModel = new API.Model.Values();
            addModel.Value1 = "";
            addModel.Value2 = "";

            var result = _calculationController.Add(addModel);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Test_Addition_WithGarbageValues_ShouldReturnInternalError()
        {
            var expected = "Testing Exception Handling";
            var mockLogger = new Mock<ILogger<API.Controllers.CalculationController>>();
            var mockCalculationService = new Mock<API.Services.Calculation>();
            mockCalculationService.Setup(x => x.Addition(It.IsAny<string>(), It.IsAny<string>())).Throws(new System.Exception(expected));
            _calculationController = new API.Controllers.CalculationController(mockCalculationService.Object, mockLogger.Object);
            var addModel = new API.Model.Values();
            addModel.Value1 = "%%%";
            addModel.Value2 = "%%%%";

            var result = _calculationController.Add(addModel) as ObjectResult;

            Assert.Equal(expected, result?.Value);
        }
    }
}
