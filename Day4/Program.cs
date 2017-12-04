using System;
using System.Linq;
using Common;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            int valid = 0;

            foreach (string line in FileIterator.Create("./input.txt"))
            {
                string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => new string(s.ToCharArray().OrderBy(c => c).ToArray()))
                    .ToArray();

                if (words.Distinct().Count() == words.Length)
                {
                    valid++;
                }
            }

            Console.WriteLine($"The valid count is {valid}");
            Console.ReadKey(true);
        }
    }
}
