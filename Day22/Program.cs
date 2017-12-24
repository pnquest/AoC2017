using System;
using System.Collections.Generic;
using System.Drawing;
using Common;

namespace Day22
{
    class Program
    {
        public enum NodeState
        {
            Clean,
            Weakened,
            Infected,
            Flagged,
        }

        static void Main(string[] args)
        {

            int y = -12;

            Dictionary<Point, NodeState> states = new Dictionary<Point, NodeState>();
            Point cur = new Point(0, 0);
            Facing facing = new Facing();
            int infectCount = 0;

            foreach (string input in FileIterator.Create("./input.txt"))
            {
                int x = -12;
                foreach (char c in input)
                {
                    if (c == '#')
                    {
                        states.Add(new Point(x, y), NodeState.Infected);
                    }
                    else
                    {
                        states.Add(new Point(x, y), NodeState.Clean);
                    }
                    x++;
                }
                y++;
            }

            for (int i = 0; i < 10000000; i++)
            {
                if (!states.ContainsKey(cur))
                {
                    states.Add(cur, NodeState.Clean);
                }

                NodeState curState = states[cur];

                switch (curState)
                {
                    case NodeState.Clean:
                        states[cur] = NodeState.Weakened;
                        facing.TurnLeft();
                        break;

                    case NodeState.Weakened:
                        states[cur] = NodeState.Infected;
                        infectCount++;
                        break;

                    case NodeState.Infected:
                        states[cur] = NodeState.Flagged;
                        facing.TurnRight();
                        break;

                    case NodeState.Flagged:
                        states[cur] = NodeState.Clean;
                        facing.TurnRight();
                        facing.TurnRight();
                        break;
                }

                cur = facing.Move(cur);
            }

            Console.WriteLine($"The answer is {infectCount}");
            Console.ReadKey(true);
        }
    }
}
