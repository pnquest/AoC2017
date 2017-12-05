using System;
using System.Collections.Generic;
using Common;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> jumps = new List<int>();
            foreach (string line in FileIterator.Create("./input.txt"))
            {
                jumps.Add(int.Parse(line));
            }

            int index = 0;
            int count = 0;

            while (index >= 0 && index < jumps.Count)
            {
                int inst = jumps[index];
                if (inst >= 3)
                {
                    jumps[index] = jumps[index] - 1;
                }
                else
                {
                    jumps[index] = jumps[index] + 1;
                }
                

                index += inst;
                count++;
            }

            Console.WriteLine($"Escaping took {count} jumps");
            Console.ReadKey(true);
        }
    }
}
