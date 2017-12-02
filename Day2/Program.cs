using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader r =
                new StreamReader(new FileStream("./input.txt", FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                int checksum = 0;
                while (!r.EndOfStream)
                {
                    string line = r.ReadLine();
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
}
