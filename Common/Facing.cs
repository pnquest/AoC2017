using System.Collections.Generic;
using System.Drawing;

namespace Common
{
    public class Facing
    {
        public Direction CurDirection { get; private set; }
        private readonly Dictionary<Direction, Point> TURNS;

        public Facing(Direction starting = Direction.N)
        {
            TURNS = new Dictionary<Direction, Point>
            {
                {Direction.N, new Point(0, -1)},
                {Direction.S, new Point(0, 1)},
                {Direction.E, new Point(1, 0)},
                {Direction.W, new Point(-1, 0)}
            };
        }

        public void TurnRight()
        {
            switch (CurDirection)
            {
                case Direction.N:
                    CurDirection = Direction.E;
                    break;

                case Direction.E:
                    CurDirection = Direction.S;
                    break;

                case Direction.S:
                    CurDirection = Direction.W;
                    break;

                case Direction.W:
                    CurDirection = Direction.N;
                    break;
            }
        }

        public void TurnLeft()
        {
            switch (CurDirection)
            {
                case Direction.N:
                    CurDirection = Direction.W;
                    break;

                case Direction.W:
                    CurDirection = Direction.S;
                    break;

                case Direction.S:
                    CurDirection = Direction.E;
                    break;

                case Direction.E:
                    CurDirection = Direction.N;
                    break;
            }
        }

        public Point Move(Point from, int distance = 1)
        {
            Point working = from;
            Point turn = TURNS[CurDirection];
            working.Offset(turn.X * distance, turn.Y * distance);

            return working;
        }


        public enum Direction
        {
            N,
            S,
            E,
            W,
        }
    }


}
