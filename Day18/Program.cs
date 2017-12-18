using System;
using System.Threading.Tasks;
using Common;

namespace Day18
{
    class Program
    { 
        static void Main(string[] args)
        {
           Emulator em0 = new Emulator(0);
           Emulator em1 = new Emulator(1);
            em1.ReceiveQueue = em0.SendQueue;
            em0.ReceiveQueue = em1.SendQueue;

            foreach (string line in FileIterator.Create("./input.txt"))
            {
                em0.LoadInstruction(line);
                em1.LoadInstruction(line);
            }

            Task tsk0 = Task.Run(async () =>
            {
                while (!em0.Terminated)
                {
                    await em0.NextInst().ConfigureAwait(false);
                }
            });

            Task tsk1 = Task.Run(async () =>
            {
                while (!em1.Terminated)
                {
                    await em1.NextInst().ConfigureAwait(false);
                }
            });

            Task.WhenAll(tsk0, tsk1).GetAwaiter().GetResult();

            Console.WriteLine($"Recovered value is {em1.SendCount}");
            Console.ReadKey(true);
        }
    }
}
