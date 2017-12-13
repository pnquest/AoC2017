using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int curDepth = 0;
            List<Layer> layers = new List<Layer>();
            int totalSeverity = 0;

            foreach (string line in FileIterator.Create("./input.txt"))
            {
                string[] vals = line.Split(':').Select(s => s.Trim()).ToArray();
                int depth = int.Parse(vals[0]);
                int range = int.Parse(vals[1]);

                while (curDepth < depth)
                {
                    layers.Add(new Layer(curDepth++, null));
                }

                layers.Add(new Layer(curDepth++, range));
            }

            int delay = 1;

            Layer[] cloned = layers.Select(l => l.Clone()).ToArray();

            while (true)
            {
                if (delay % 100 == 0)
                {
                    Console.WriteLine($"Trying delay of {delay}");
                }

                layers = new List<Layer>(cloned);
                
                bool caught = false;

                layers.ForEach(l => l.MoveScanner());
                cloned = layers.Select(l => l.Clone()).ToArray();

                delay++;

                for (int i = 0; i < layers.Count; i++)
                {
                    if (layers[i].MoveIntoLayer())
                    {
                        caught = true;
                        break;
                    }

                    layers.ForEach(l => l.MoveScanner());
                }

                if (!caught)
                {
                    break;
                }
            }

            Console.WriteLine($"The total delay is {delay - 1}");
            Console.ReadKey(true);
        }
    }
}
