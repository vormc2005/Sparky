using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Calculator
    {
        public List<int> NumberRange = new List<int>();

        public int AddNumbers(int a, int b)
        {
            return a + b;
        }

        public double AddDoubleNumbers(double a, double b)
        {
            return a + b;
        }

        public bool IsOddNumber(int a) => a % 2 != 0;

        public List<int> GetRange(int min, int max)
        {
            NumberRange.Clear();
            for(int i=min; i<=max; i++)
            {
                if(i%2 != 0)
                {
                    NumberRange.Add(i);
                }
            }
            return NumberRange;
        }
    }
}
