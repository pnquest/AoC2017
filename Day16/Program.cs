using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<char> dancers = new LinkedList<char>(new []{'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'});
            string[] inputs = File.ReadAllText("./input.txt").Split(',', StringSplitOptions.RemoveEmptyEntries);

            using (StreamWriter sw =
                new StreamWriter(new FileStream("./output.txt", FileMode.Create, FileAccess.Write)))
            {
                for (long i = 0; i < 1000; i++)
                {
                    sw.WriteLine(string.Join(string.Empty, dancers));
                    if (i % 1000 == 0)
                    {
                        Console.WriteLine($"round {i}");
                    }

                    foreach (string input in inputs)
                    {
                        if (input.StartsWith('s'))
                        {
                            int spinNum = int.Parse(input.Substring(1));
                            SpinChars(spinNum, dancers);
                        }
                        else if (input.StartsWith('x'))
                        {
                            SwapIndexes(input, dancers);
                        }
                        else if (input.StartsWith('p'))
                        {
                            SwapDancers(input, dancers);
                        }
                        else
                        {
                            throw new Exception("bad input");
                        }
                    }
                }
            }
                
            
            StringBuilder sb = new StringBuilder(16);

            foreach (char dancer in dancers)
            {
                sb.Append(dancer);
            }

            Console.WriteLine(sb.ToString());
            Console.ReadKey(true);
        }

        private static void SwapDancers(string input, LinkedList<char> dancers)
        {
            Dictionary<char, int> ordered = dancers.Select((d, i) => new { idx = i, val = d }).ToDictionary(v => v.val, v => v.idx);
            char[] swaps = input.Substring(1).Split('/').Select(s => s.ToArray().First()).OrderBy(s => ordered[s]).ToArray();

            char first = swaps[0];
            char second = swaps[1];

            SwapChars(dancers, first, second);
        }

        private static void SwapIndexes(string input, LinkedList<char> dancers)
        {
            int[] swaps = input.Substring(1).Split('/').Select(int.Parse).OrderBy(i => i).ToArray();
            char first = dancers.Select((d, i) => new {val = d, idx = i}).First(v => v.idx == swaps[0]).val;
            char second = dancers.Select((d, i) => new {val = d, idx = i}).First(v => v.idx == swaps[1]).val;

            SwapChars(dancers, first, second);
        }

        private static void SpinChars(int spinNum, LinkedList<char> dancers)
        {
            for (int i = 0; i < spinNum; i++)
            {
                char last = dancers.Last.Value;
                dancers.RemoveLast();
                dancers.AddFirst(last);
            }
        }

        private static void SwapChars(LinkedList<char> dancers, char first, char second)
        {
            LinkedListNode<char> firstNode = dancers.Find(first);
            LinkedListNode<char> secondNode = dancers.Find(second);

            LinkedListNode<char> before = secondNode.Previous;
            LinkedListNode<char> after = secondNode.Next;

            dancers.Remove(secondNode);
            dancers.AddBefore(firstNode, second);

            if (before?.List == null && after?.List == null || after == firstNode || before == firstNode) //this means we just swaped neighbors
            {
                return;
            }
            dancers.Remove(firstNode);
            if (before != null)
            {
                dancers.AddAfter(before, first);
            }
            else
            {
                dancers.AddBefore(after, first);
            }
        }
    }
}
