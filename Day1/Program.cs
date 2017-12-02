using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();
            string input = File.ReadAllText("./input.txt");
            LinkedList<int> fields = new LinkedList<int>(input.Select(c => int.Parse(c.ToString())));
            int steps = fields.Count / 2;

            int sum = 0;
            LinkedListNode<int> curNode = fields.First;
            while(curNode != null)
            {
                int first = curNode.Value;
                int second = GetComparisonValue(curNode, steps);

                if (first == second)
                {
                    sum += first;
                }

                curNode = curNode.Next;
            }
            stp.Stop();
            Console.WriteLine($"The result is {sum} ({stp.Elapsed})");
            Console.ReadKey(true);
        }

        private static int GetComparisonValue(LinkedListNode<int> cur, int steps)
        {
            LinkedListNode<int> node = cur;

            for (int i = 0; i < steps; i++)
            {
                if (node.Next != null)
                {
                    node = node.Next;
                }
                else
                {
                    node = node.List.First;
                }
            }

            return node.Value;
        }
    }
}
