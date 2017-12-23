using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using Common;

namespace Day20
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex reg = new Regex(@"p=<(?<px>[^,]+),(?<py>[^,]+),(?<pz>[^>]+)>, v=<(?<vx>[^,]+),(?<vy>[^,]+),(?<vz>[^>]+)>, a=<(?<ax>[^,]+),(?<ay>[^,]+),(?<az>[^>]+)>");
            List<Particle> particles = new List<Particle>();
            foreach (string line in FileIterator.Create("./input.txt"))
            {
                Match m = reg.Match(line);

                Vector3 pos = new Vector3(float.Parse(m.Groups["px"].Value), float.Parse(m.Groups["py"].Value), float.Parse(m.Groups["pz"].Value));
                Vector3 vel = new Vector3(float.Parse(m.Groups["vx"].Value), float.Parse(m.Groups["vy"].Value), float.Parse(m.Groups["vz"].Value));
                Vector3 acc = new Vector3(float.Parse(m.Groups["ax"].Value), float.Parse(m.Groups["ay"].Value), float.Parse(m.Groups["az"].Value));

                particles.Add(new Particle(pos, vel, acc));
            }

            RemoveColisions(particles);

            for (int i = 0; i < 10000; i++)
            {
                particles.AsParallel().ForAll(p => p.MoveParticle());
                RemoveColisions(particles);
            }

            Console.WriteLine($"particle count is {particles.Count}");
            Console.ReadKey(true);
        }

        private static void RemoveColisions(List<Particle> particles)
        {
            var colided = particles.GroupBy(p => p.Position).Where(g => g.Count() > 1).SelectMany(g => g);

            foreach (Particle col in colided)
            {
                particles.Remove(col);
            }
        }
    }
}
