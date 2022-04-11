using System.Numerics;

namespace Calculation.API.Services
{
    public class Calculation : ICalculation
    {
        public BigInteger Add(BigInteger value1, BigInteger value2)
        {
            return BigInteger.Add(value1, value2);
        }
    }
}
