using System;
using System.Collections.Generic;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        private static void Part2()
        {
            const int spins = 337;
            int curValue = 1;
            int valAfter0 = 0;
            int curLength = 1;
            int curPos = 0;

            while (curValue <= 50_000_000)
            {
                curPos = (curPos + spins) % curLength;

                if (curPos == 0)
                {
                    valAfter0 = curValue;
                }

                curPos++;
                curLength++;
                curValue++;
            }

            Console.WriteLine($"The value after 0 is {valAfter0}");
            Console.ReadKey(true);
        }

        private static void Part1()
        {
            int nextNumber = 1;
            LinkedList<int> buffer = new LinkedList<int>();
            buffer.AddFirst(0);
            LinkedListNode<int> cur = buffer.First;
            Console.CursorVisible = false;
            while (nextNumber < 50000000)
            {
                Console.CursorLeft = 0;
                Console.Write(nextNumber);
                for (int i = 0; i < 337; i++)
                {
                    cur = cur.Next ?? cur.List.First;
                }
                buffer.AddAfter(cur, nextNumber++);
                cur = cur.Next;
            }

            int after = buffer.Find(0).Next.Value;

            Console.WriteLine($"The value after 2017 is {after}");
            Console.ReadKey(true);
        }
    }
}
