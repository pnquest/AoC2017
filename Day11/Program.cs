using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            Direction[] input = File.ReadAllText("./input.txt")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(Enum.Parse<Direction>)
                .ToArray();

            Point childLocation = new Point(0, 0);
            int maxDistance = 0;

            foreach (Direction dir in input)
            {
                MoveStep(dir, ref childLocation);

                Point myLocation = new Point(0, 0);
                int moveCount = ComputeDistance(myLocation, childLocation);

                if (moveCount > maxDistance)
                {
                    maxDistance = moveCount;
                }
            }

            

            Console.WriteLine($"The max child distance is {maxDistance} steps away");
            Console.ReadKey(true);
        }

        private static int ComputeDistance(Point myLocation, Point childLocation)
        {
            int moveCount = 0;

            while (myLocation.X != childLocation.X || myLocation.Y != childLocation.Y)
            {
                if (myLocation.X < childLocation.X)
                {
                    if (myLocation.Y < childLocation.Y)
                    {
                        myLocation.Offset(1, 1);
                    }
                    else if (myLocation.Y > childLocation.Y)
                    {
                        myLocation.Offset(1, -1);
                    }
                    else
                    {
                        myLocation.Offset(1, 0);
                    }
                }
                else if (myLocation.X > childLocation.X)
                {
                    if (myLocation.Y < childLocation.Y)
                    {
                        myLocation.Offset(-1, 1);
                    }
                    else if (myLocation.Y > childLocation.Y)
                    {
                        myLocation.Offset(-1, -1);
                    }
                    else
                    {
                        myLocation.Offset(-1, 0);
                    }
                }
                else
                {
                    if (myLocation.Y < childLocation.Y)
                    {
                        myLocation.Offset(0, 1);
                    }
                    else if (myLocation.Y > childLocation.Y)
                    {
                        myLocation.Offset(0, -1);
                    }
                }

                moveCount++;
            }
            return moveCount;
        }

        private static void MoveStep(Direction dir, ref Point location)
        {
            switch (dir)
            {
                case Direction.n:
                    location.Offset(0, 1);
                    break;

                case Direction.ne:
                    location.Offset(1, 1);
                    break;

                case Direction.nw:
                    location.Offset(-1, 1);
                    break;

                case Direction.s:
                    location.Offset(0, -1);
                    break;

                case Direction.se:
                    location.Offset(1, -1);
                    break;

                case Direction.sw:
                    location.Offset(-1, -1);
                    break;

                case Direction.e:
                    location.Offset(1, 0);
                    break;

                case Direction.w:
                    location.Offset(-1, 0);
                    break;

                default:
                    throw new Exception("something bad happened");
            }
        }

        public enum Direction
        {
            n,
            ne,
            nw,
            s,
            se,
            sw,
            e,
            w
        }
    }
}
