using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Common;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> rows = FileIterator.Create("./input.txt").ToList();
            int x = rows.First().Length;
            int y = rows.Count;

            char[,] map = new char[x,y];

            for (int j = 0; j < y; j++)
            {
                char[] row = rows[j].ToCharArray();
                for (int i = 0; i < x; i++)
                {
                    map[i, j] = row[i];
                }
            }

            Point pos = new Point(rows[0].IndexOf('|'), 0);
            Point dir = new Point(0, 1);
            StringBuilder sb = new StringBuilder();
            bool cont = true;
            int steps = 0; //we will end up with an extra step at the end for stepping off the puzzle, so we start at 0 instead of 1 so as to not count stepping onto the puzzle
            while (cont)
            {
                while (map[pos.X, pos.Y] != '+' && map[pos.X, pos.Y] != ' ')
                {
                    if (map[pos.X, pos.Y] != '|' && map[pos.X, pos.Y] != '-')
                    {
                        sb.Append(map[pos.X, pos.Y]);
                    }

                    pos.Offset(dir);
                    steps++;
                }

                var res = CalculateDirection(dir, pos, map);
                if (res.Item1)
                {
                    dir = res.Item2;
                    pos.Offset(dir);
                    steps++;
                }
                else
                {
                    cont = false;
                }
            }
            
            Console.WriteLine(sb.ToString());
            Console.WriteLine($"{steps} steps");
            Console.ReadKey(true);
        }

        private static (bool, Point) CalculateDirection(Point dir, Point pos, char[,] map)
        {
            if (dir.X == 0)
            {
                if (pos.X > 0)
                {
                    if (map[pos.X - 1, pos.Y] == '-' || char.IsLetter(map[pos.X - 1, pos.Y]))
                    {
                        return (true,new Point(-1, 0));
                    }
                }
                if (pos.X < map.GetLength(0))
                {
                    if (map[pos.X + 1, pos.Y] == '-' || char.IsLetter(map[pos.X + 1, pos.Y]))
                    {
                        return (true, new Point(1, 0));
                    }
                }
            }
            if (dir.Y == 0)
            {
                if (pos.Y > 0)
                {
                    if (map[pos.X, pos.Y - 1] == '|' || char.IsLetter(map[pos.X, pos.Y - 1]))
                    {
                        return(true,new Point(0, -1));
                    }
                }
                if (pos.Y < map.GetLength(1))
                {
                    if (map[pos.X, pos.Y + 1] == '|' || char.IsLetter(map[pos.X, pos.Y + 1]))
                    {
                        return(true, new Point(0, 1));
                    }
                }
            }

            return (false, Point.Empty);
        }
    }
}
