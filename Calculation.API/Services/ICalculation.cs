using System.Numerics;

namespace Calculation.API.Services
{
    public interface ICalculation
    {
        BigInteger Add(BigInteger value1, BigInteger value2);
    }
}