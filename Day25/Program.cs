using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace Day25
{
    class Program
    {
        private static int _curIndex;

        static void Main(string[] args)
        {
            Dictionary<int, ushort> values = new Dictionary<int, ushort> {{0, 0}};
            _curIndex = 0;
            StateMachine<States, int> machine = new StateMachine<States, int>(States.A);
            ConfigureMachine(machine, values);

            for (int i = 0; i < 12_683_008; i++)
            {
                if (i % 10_000 == 0)
                {
                    Console.WriteLine($"Step {i:N0}");
                }
                machine.Fire(values[_curIndex]);
            }

            int sum = values.Values.Sum(v => (int)v);

            Console.WriteLine($"the sum is {sum}");
            Console.ReadKey(true);
        }

        private static void ConfigureMachine(StateMachine<States, int> machine, Dictionary<int, ushort> values)
        {
            machine.Configure(States.A)
                .OnExit(t =>
                {
                    if (t.Trigger == 0)
                    {
                        values[_curIndex] = 1;
                        MoveRight(values);
                    }
                    else
                    {
                        values[_curIndex] = 0;
                        MoveLeft(values);
                    }
                }).Permit(0, States.B).Permit(1, States.B);

            machine.Configure(States.B)
                .OnExit(t =>
                {
                    if (t.Trigger == 0)
                    {
                        values[_curIndex] = 1;
                        MoveLeft(values);
                    }
                    else
                    {
                        values[_curIndex] = 0;
                        MoveRight(values);
                    }
                }).Permit(0, States.C).Permit(1, States.E);

            machine.Configure(States.C)
                .OnExit(t =>
                {
                    if (t.Trigger == 0)
                    {
                        values[_curIndex] = 1;
                        MoveRight(values);
                    }
                    else
                    {
                        values[_curIndex] = 0;
                        MoveLeft(values);
                    }
                }).Permit(0, States.E).Permit(1, States.D);


            machine.Configure(States.D)
                .OnExit(() =>
                {
                    values[_curIndex] = 1;
                    MoveLeft(values);
                }).Permit(0, States.A).Permit(1, States.A);

            machine.Configure(States.E)
                .OnExit(() =>
                {
                    values[_curIndex] = 0;
                    MoveRight(values);
                }).Permit(0, States.A).Permit(1, States.F);

            machine.Configure(States.F)
                .OnExit(() =>
                {
                    values[_curIndex] = 1;
                    MoveRight(values);
                }).Permit(0, States.E).Permit(1, States.A);
        }

        private static void MoveLeft(Dictionary<int, ushort> values)
        {
            if (!values.ContainsKey(--_curIndex))
            {
                values.Add(_curIndex, 0);
            }
        }

        private static void MoveRight(Dictionary<int, ushort> values)
        {
            if (!values.ContainsKey(++_curIndex))
            {
                values.Add(_curIndex, 0);
            }
        }
    }
}
