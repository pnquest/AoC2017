using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            const int input = 325489;

            Part2(input);
        }

        private static void Part1(int input)
        {
            int curSquareRoot = 1;
            int iterations = 0;

            while (Math.Pow(curSquareRoot, 2) <= input)
            {
                curSquareRoot += 2;
                iterations++;
            }

            int curSquare = (int)Math.Pow(curSquareRoot, 2);

            int maxDistanceFromCenter = curSquareRoot / 2;

            List<int> corners = new List<int>();

            int curCorner = curSquare;

            for (int i = 0; i < 4; i++)
            {
                corners.Add(curCorner);
                curCorner -= curSquareRoot - 1;
            }

            int stepsFromCorner = corners.Min(c => Math.Abs(input - c));

            Console.WriteLine($"Distance is {iterations + maxDistanceFromCenter - stepsFromCorner}");
            Console.ReadKey(true);
        }

        private static void Part2(int input)
        {
            int sqrt = 1;
            int moveCount = 0;
            int curValue = 1;
            Point cur = new Point(0, 0);
            Dictionary<Point, int> values = new Dictionary<Point, int>{{cur, curValue}}; 
            Direction curDirection = Direction.Right;

            while (curValue <= input)
            {
                cur = CalculateNextPoint(cur, curDirection);
                (curDirection, sqrt, moveCount) = CalculateNextDirection(sqrt, moveCount, curDirection);
                curValue = CalculateNextValue(values, cur);
                values.Add(cur, curValue);
            }

            Console.WriteLine($"The first value is {curValue}");
            Console.ReadKey(true);
        }

        private enum Direction
        {
            Right,
            Up,
            Left,
            Down
        }

        private static int CalculateNextValue(Dictionary<Point, int> values, Point cur)
        {
            return values
                .Where(d => CalculateDistance(d.Key, cur) <= Math.Sqrt(2))
                .Sum(d => d.Value);
        }

        private static double CalculateDistance(Point p1, Point p2)
        {
            double x = Math.Pow(p1.X - p2.X, 2);
            double y = Math.Pow(p1.Y - p2.Y, 2);

            return Math.Sqrt(x + y);
        }

        private static (Direction, int, int) CalculateNextDirection(int sqrt, int moveCount, Direction cur)
        {
            if (cur == Direction.Right && moveCount + 1 < sqrt 
                ||  cur == Direction.Up && moveCount + 1 < sqrt - 2 
                || (cur == Direction.Down || cur == Direction.Left) && moveCount + 1 < sqrt - 1)
            {
                return (cur, sqrt, moveCount + 1);
            }

            switch (cur)
            {
                case Direction.Up:
                    return (Direction.Left, sqrt, 0);

                case Direction.Left:
                    return (Direction.Down, sqrt, 0);

                case Direction.Down:
                    return (Direction.Right, sqrt, 0);

                case Direction.Right:
                    return (Direction.Up, sqrt + 2, 0);

                default:
                    throw new Exception("this cant happen");
            }
        }

        private static Point CalculateNextPoint(Point pt, Direction dir)
        {
            Point ret = new Point(pt.X, pt.Y);
            switch (dir)
            {
                case Direction.Down:
                    ret.Offset(0, -1);
                    break;

                case Direction.Up:
                    ret.Offset(0, 1);
                    break;

                case Direction.Left:
                    ret.Offset(-1, 0);
                    break;

                case Direction.Right:
                    ret.Offset(1, 0);
                    break;
            }

            return ret;
        }
    }
}
