using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> recipies = new Dictionary<string, string>();

            foreach (string line in FileIterator.Create("./input.txt"))
            {
                string[] split = line.Split("=>").Select(s => s.Trim()).ToArray();
                recipies.Add(split[0], split[1]);
            }

            Img full = new Img(".#./..#/###");

            for (int i = 0; i < 18; i++)
            {
                Console.WriteLine($"Iteration {i}");
                Img[] split;
                List<Img> expanded = new List<Img>();

                if (full.Rows.Count % 2 == 0)
                {
                    split = full.Split(2).ToArray();
                }
                else
                {
                    split = full.Split(3).ToArray();
                }

                foreach (Img img in split)
                {
                    string result = null;
                    Img working = img;
                    while (result == null)
                    {
                        result = CheckMatch(working, recipies);
                        if (result != null)
                        {
                            break;
                        }
                        working = working.Flip();
                        result = CheckMatch(working, recipies);
                        if (result != null)
                        {
                            break;
                        }
                        working = working.Flip().Rotate();
                    }

                    expanded.Add(new Img(result) {Row = img.Row, Col = img.Col});
                }

                full = expanded[0].Assemble(expanded.Skip(1));

            }

            int cnt = full.Rows.SelectMany(r => r).Count(r => r.Equals("#"));

            Console.WriteLine($"the answer is {cnt}");
            Console.ReadKey(true);
        }

        private static string CheckMatch(Img working, Dictionary<string, string> recipies)
        {
            string key = working.ToString();
            string result = null;
            if (recipies.ContainsKey(key))
            {
                result = recipies[key];
            }
            return result;
        }
    }
}
