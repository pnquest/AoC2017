using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank[] banks = File.ReadAllText("./input.txt")
                .Split('\t')
                .Select((b, i) => new Bank(i, int.Parse(b)))
                .ToArray();

            int iterCount = 0;
            List<string> states = new List<string>();
            string curState = string.Join(",", (IEnumerable<Bank>) banks);

            while (!states.Contains(curState))
            {
                curState = RunLoop(states, curState, banks, ref iterCount);
            }

            iterCount = 0;
            states.Clear();

            while (!states.Contains(curState))
            {
                curState = RunLoop(states, curState, banks, ref iterCount);
            }

            Console.WriteLine($"It tooks {iterCount} iterations");
            Console.ReadKey(true);
        }

        private static string RunLoop(List<string> states, string curState, Bank[] banks, ref int iterCount)
        {
            states.Add(curState);

            var curIndx = banks.OrderByDescending(b => b.Blocks).ThenBy(b => b.Index).Select(b => b.Index).First();
            int blocksToAlloc = banks[curIndx].Blocks;
            banks[curIndx].Blocks = 0;

            while (blocksToAlloc > 0)
            {
                banks[++curIndx % banks.Length].Blocks++;
                blocksToAlloc--;
            }

            curState = string.Join(",", (IEnumerable<Bank>) banks);

            iterCount++;
            return curState;
        }
    }
}
