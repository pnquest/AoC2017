using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            int checksum = 0;

            foreach (string line in FileIterator.Create("./input.txt"))
            {
                int[] values = line.Split('\t', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                    .ToArray();

                for (int i = 0; i < values.Length; i++)
                {
                    List<int> temp = new List<int>(values);
                    temp.RemoveAt(i);
                    List<int> mods = temp.Select(v => values[i] % v).ToList();
                    int idx = mods.IndexOf(0);

                    if (idx >= 0)
                    {
                        int otherNumber = temp[idx];
                        checksum += values[i] / otherNumber;
                        break;
                    }
                }
            }


            Console.WriteLine($"The checksum is {checksum}");
            Console.ReadKey(true);
        }
    }
}
