using System;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator genA = new Generator(634, 16807, 4);
            Generator genB = new Generator(301, 48271, 8);

            int count = 0;
            Console.CursorVisible = false;
            for(int i = 0; i < 5_000_000; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.CursorLeft = 0;
                    Console.Write($"{i:N0}");
                }

                int valueA = genA.ComputeNextValue();
                string binaryA = Convert.ToString(valueA, 2).PadLeft(32, '0');

                int valB = genB.ComputeNextValue();
                string binaryB = Convert.ToString(valB, 2).PadLeft(32, '0');

                if (binaryA.Substring(16) == binaryB.Substring(16))
                {
                    count++;
                }
            }
            Console.WriteLine();
            Console.CursorVisible = true;
            Console.WriteLine($"The match count is {count}");
            Console.ReadKey(true);
        }
    }
}
