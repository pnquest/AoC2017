using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day18
{
    public class MulInstruction : IInstruction
    {
        public string Register { get; }
        public string Value { get; }

        public MulInstruction(string register, string value, Emulator parent)
        {
            Register = register;
            Value = value;
            Parent = parent;
        }

        public Task<bool> Execute(ConcurrentQueue<long> incoming, ConcurrentQueue<long> outgoing, Dictionary<string, long> registers)
        {
            if (!registers.ContainsKey(Register))
            {
                registers.Add(Register, 0);
            }

            if (long.TryParse(Value, out long val))
            {
                registers[Register] *= val;
            }
            else
            {
                if (!registers.ContainsKey(Value))
                {
                    registers.Add(Value, 0);
                }

                long regVal = registers[Value];
                registers[Register] *= regVal;
            }

            Parent.StackPointer++;
            return Task.FromResult(false);
        }

        public Emulator Parent { get; }
    }
}
