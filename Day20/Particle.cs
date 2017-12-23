using System;
using System.Numerics;

namespace Day20
{
    public class Particle
    {
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; }
        public Vector3 Acceleration { get;}

        public Particle(Vector3 position, Vector3 velocity, Vector3 acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }

        public void MoveParticle()
        {
            Velocity += Acceleration;
            Position += Velocity;
        }

        public float distanceFromZero => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
    }
}
