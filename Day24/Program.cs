using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day24
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Link> links = new List<Link>();
            foreach (string line in FileIterator.Create("./input.txt"))
            {
                int[] vals = line.Split('/').Select(int.Parse).ToArray();
                links.Add(new Link(vals[0], vals[1]));
            }

            Bridge b = new Bridge(links);

            while (!b.IsAllComplete())
            {
                b.SolveNext();
            }

            Bridge longest = b.GetLongest();

            Console.WriteLine($"Greatest strength is {longest.MyScore}");
            Console.ReadKey(true);
        }
    }
}
