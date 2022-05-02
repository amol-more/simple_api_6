using System.Text;

namespace Calculation.API.Services
{
    public class Calculation : ICalculation
    {
        public virtual string Addition(string value1, string value2)
        {
            var sum = new StringBuilder();
            int carry = 0;
            if (value1.Length != value2.Length)
            {
                var maxLength = Math.Max(value1.Length, value2.Length);
                value1 = value1.PadLeft(maxLength, '0');
                value2 = value2.PadLeft(maxLength, '0');
            }
            for (int i = value1.Length - 1; i >= 0; i--)
            {
                if(!char.IsNumber(value1[i]) || !char.IsNumber(value2[i]))
                {
                    return string.Empty;
                }
                var singleDigitSum = (value1[i]-'0') + (value2[i] - '0') + carry;
                if (singleDigitSum > 9)
                {
                    carry = 1;
                    singleDigitSum = singleDigitSum - 10;
                }
                else
                {
                    carry = 0;
                }
                sum.Insert(0, singleDigitSum.ToString());
            }
            if (carry == 1)
            {
                sum.Insert(0, carry.ToString());
            }
            return sum.ToString();
        }
    }
}
