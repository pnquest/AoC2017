using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Common;

namespace Day14
{
    class Program
    {
        private const string input = "uugsqrei";
        static void Main(string[] args)
        {
            int[,] value = new int[128,128];
            for (int i = 0; i < 128; i++)
            {
                string hash = KnotHasher.ComputeKnotHash($"{input}-{i}");
                int[] toBits = ConvertToBits(hash);

                for (int j = 0; j < toBits.Length; j++)
                {
                    value[j, i] = toBits[j];
                }
            }

            int groupCounter = 0;
            HashSet<Point> usedPoints = new HashSet<Point>();

            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    Point item = new Point(x, y);
                    if (value[x, y] == 1 && !usedPoints.Contains(item))
                    {
                        groupCounter++;
                        usedPoints.Add(item);

                        Stack<Point> pointsToCheck = new Stack<Point>();
                        pointsToCheck.Push(item);

                        while (pointsToCheck.Any())
                        {
                            Point pt = pointsToCheck.Pop();

                            if (pt.X < 127)
                            {
                                Point ptn = new Point(pt.X + 1, pt.Y);
                                if (value[pt.X + 1, pt.Y] == 1 && !usedPoints.Contains(ptn))
                                {
                                    
                                    usedPoints.Add(ptn);
                                    pointsToCheck.Push(ptn);
                                }
                            }

                            if (pt.Y < 127)
                            {
                                Point ptn = new Point(pt.X, pt.Y + 1);

                                if (value[pt.X, pt.Y + 1] == 1 && !usedPoints.Contains(ptn))
                                {
                                    usedPoints.Add(ptn);
                                    pointsToCheck.Push(ptn);
                                }
                            }

                            if (pt.X > 0)
                            {
                                Point ptn = new Point(pt.X - 1, pt.Y);

                                if (value[pt.X - 1, pt.Y] == 1 && !usedPoints.Contains(ptn))
                                {

                                    usedPoints.Add(ptn);
                                    pointsToCheck.Push(ptn);
                                }

                            }

                            if (pt.Y > 0)
                            {
                                Point ptn = new Point(pt.X, pt.Y - 1);

                                if (value[pt.X, pt.Y - 1] == 1 && !usedPoints.Contains(ptn))
                                {
                                    usedPoints.Add(ptn);
                                    pointsToCheck.Push(ptn);
                                }
                            }
                        }
                    }
                }
            }


            Console.WriteLine($"There are {groupCounter} groups");
            Console.ReadKey(true);
        }

        private static int[] ConvertToBits(string hash)
        {
            List<int> ret = new List<int>();
            foreach (char ch in hash)
            {
                switch (ch)
                {
                    case '0':
                        ret.AddRange(new[]{0,0,0,0});
                        break;

                    case '1':
                        ret.AddRange(new[] { 0, 0, 0, 1 });
                        break;

                    case '2':
                        ret.AddRange(new[] { 0, 0, 1, 0 });
                        break;

                    case '3':
                        ret.AddRange(new[] { 0, 0, 1, 1 });
                        break;

                    case '4':
                        ret.AddRange(new[] { 0, 1, 0, 0 });
                        break;

                    case '5':
                        ret.AddRange(new[] { 0, 1, 0, 1 });
                        break;

                    case '6':
                        ret.AddRange(new[] { 0, 1, 1, 0 });
                        break;

                    case '7':
                        ret.AddRange(new[] { 0, 1, 1, 1 });
                        break;

                    case '8':
                        ret.AddRange(new[] { 1, 0, 0, 0 });
                        break;

                    case '9':
                        ret.AddRange(new[] { 1, 0, 0, 1 });
                        break;

                    case 'A':
                        ret.AddRange(new[] { 1, 0, 1, 0 });
                        break;

                    case 'B':
                        ret.AddRange(new[] { 1, 0, 1, 1 });
                        break;

                    case 'C':
                        ret.AddRange(new[] { 1, 1, 0, 0 });
                        break;

                    case 'D':
                        ret.AddRange(new[] { 1, 1, 0, 1 });
                        break;

                    case 'E':
                        ret.AddRange(new[] { 1, 1, 1, 0 });
                        break;

                    case 'F':
                        ret.AddRange(new[] { 1, 1, 1, 1 });
                        break;
                }
            }

            return ret.ToArray();
        }
    }
}
