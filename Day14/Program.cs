using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day14
{
    class Program
    {
        private const string input = "uugsqrei";
        static void Main(string[] args)
        {
            List<int> bits = new List<int>();
            for (int i = 0; i < 128; i++)
            {
                string hash = KnotHasher.ComputeKnotHash($"{input}-{i}");
                int[] toBits = ConvertToBits(hash);
                bits.AddRange(toBits);
                Console.WriteLine(string.Join(" ", toBits));
            }
            Console.WriteLine($"There are {bits.Count(b => b == 1)} used squares");
            Console.ReadKey(true);
        }

        private static int[] ConvertToBits(string hash)
        {
            List<int> ret = new List<int>();
            foreach (char ch in hash)
            {
                switch (ch)
                {
                    case '0':
                        ret.AddRange(new[]{0,0,0,0});
                        break;

                    case '1':
                        ret.AddRange(new[] { 0, 0, 0, 1 });
                        break;

                    case '2':
                        ret.AddRange(new[] { 0, 0, 1, 0 });
                        break;

                    case '3':
                        ret.AddRange(new[] { 0, 0, 1, 1 });
                        break;

                    case '4':
                        ret.AddRange(new[] { 0, 1, 0, 0 });
                        break;

                    case '5':
                        ret.AddRange(new[] { 0, 1, 0, 1 });
                        break;

                    case '6':
                        ret.AddRange(new[] { 0, 1, 1, 0 });
                        break;

                    case '7':
                        ret.AddRange(new[] { 0, 1, 1, 1 });
                        break;

                    case '8':
                        ret.AddRange(new[] { 1, 0, 0, 0 });
                        break;

                    case '9':
                        ret.AddRange(new[] { 1, 0, 0, 1 });
                        break;

                    case 'A':
                        ret.AddRange(new[] { 1, 0, 1, 0 });
                        break;

                    case 'B':
                        ret.AddRange(new[] { 1, 0, 1, 1 });
                        break;

                    case 'C':
                        ret.AddRange(new[] { 1, 1, 0, 0 });
                        break;

                    case 'D':
                        ret.AddRange(new[] { 1, 1, 0, 1 });
                        break;

                    case 'E':
                        ret.AddRange(new[] { 1, 1, 1, 0 });
                        break;

                    case 'F':
                        ret.AddRange(new[] { 1, 1, 1, 1 });
                        break;
                }
            }

            return ret.ToArray();
        }
    }
}
