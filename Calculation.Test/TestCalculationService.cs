using Xunit;

namespace Calculation.Test
{
    public class TestCalculationService
    {
        private readonly API.Services.Calculation _calculator;
        public TestCalculationService()
        {
            _calculator = new API.Services.Calculation();
        }

        [Theory]
        [InlineData("999999999999", "999999999999", "1999999999998")]
        [InlineData("1000", "99","1099")]
        [InlineData("28", "100072", "100100")]
        public void Test_Calcultaion_WithTwoNumbers_ShouldAddCorrectly(string value1, string value2,string expectedValue)
        {
            
            var result = _calculator.Addition(value1, value2);

            Assert.True(result.Equals(expectedValue));
        }
        [Theory]
        [InlineData("","")]
        [InlineData("ABC","ABC")]
        public void Test_Calculation_WithNoInput_ShouldReturnNullOrEmpty(string value1,string value2 )
        {
            
            var result = _calculator.Addition(value1, value2);

            Assert.True(string.IsNullOrEmpty(result));
        }
    }
}