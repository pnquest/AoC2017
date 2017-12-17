using System;
using System.Collections.Generic;
using System.Text;

namespace Day15
{
    public class Generator
    {
        public int Value { get; private set; }
        public int Factor { get; }
        public int Multiple { get; }

        private const int Divisor = 2147483647;

        public Generator(int value, int factor, int multiple)
        {
            Value = value;
            Factor = factor;
            Multiple = multiple;
        }

        public int ComputeNextValue()
        {
            do
            {
                Value = (int) (((long) Value * (long) Factor) % (long) Divisor);
            } while (Value % Multiple != 0);
            
            return Value;
        }
    }
}
