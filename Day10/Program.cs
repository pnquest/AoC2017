using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            //version with cheap hacker effect for fun
            Console.CursorVisible = false;
            List<int> numbers = new List<int>(Enumerable.Range(0, 256));
            int skipCount = 0;
            int index = 0;
            string txtInp = File.ReadAllText("./input.txt");
            
            Console.WriteLine(txtInp);
            Thread.Sleep(1000);
            Console.Clear();

            byte[] bytes = Encoding.ASCII.GetBytes(txtInp);
            int[] input = bytes.Select(b => (int)b).Concat(new[] {17, 31, 73, 47, 23}).ToArray();
            for (int r = 0; r < 64; r++)
            {
                

                RunRound(input, ref index, numbers, ref skipCount);

                string result = ComputeDenseHash(numbers);
                Console.CursorLeft = 0;
                Console.Write(result);
                Thread.Sleep(100);

            }
            Console.ReadKey(true);
        }

        private static void RunRound(int[] input, ref int index, List<int> numbers, ref int skipCount)
        {
            foreach (int selection in input)
            {
                List<int> subList = new List<int>();
                for (int i = index; i < index + selection; i++)
                {
                    subList.Add(numbers[i % numbers.Count]);
                }

                subList.Reverse();
                int j = 0;
                for (int i = index; i < index + selection; i++)
                {
                    numbers[i % numbers.Count] = subList[j++];
                }

                index += (skipCount++ + selection);
            }
        }

        private static string ComputeDenseHash(List<int> numbers)
        {
            var groups = numbers.Select((n, i) => new {Idx = i / 16, Val = n}).GroupBy(g => g.Idx);
            List<int> denseHash = new List<int>();
            foreach (var grp in groups)
            {
                int val = grp.Select(g => g.Val).Aggregate((v1, v2) => v1 ^ v2);
                denseHash.Add(val);
            }

            string result = denseHash.Select(d => d.ToString("X2")).Aggregate(string.Empty, (v1, v2) => v1 + v2);
            return result;
        }
    }
}
