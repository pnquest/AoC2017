using System;
using System.Collections.Generic;
using Common;
using Common.Emulator;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
           Emulator em = new Emulator(new Dictionary<string, long>{{"a", 1}, { "b", 0 } , { "c", 0 } , { "d", 0 } , { "e", 0 } , { "f", 0 } , { "g", 0 } , { "h", 0 } });

            foreach (string line in FileIterator.Create("./input.txt"))
            {
                em.LoadInstruction(line);
            }

            while (!em.Terminated)
            {
                em.NextInst().GetAwaiter().GetResult();
            }

            Console.WriteLine($"The count is {em.DebugCounts[typeof(MulInstruction).Name]}");
            Console.WriteLine($"Register h is {em.GetRegisterValue("h")}");
            Console.ReadKey(true);
        }
    }
}
