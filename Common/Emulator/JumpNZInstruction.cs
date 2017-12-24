using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Emulator
{
    public class JumpNZInstruction : IInstruction
    {
        public string Value { get; }
        public string Offset { get; }

        public JumpNZInstruction(string value, string offset, Emulator parent)
        {
            Value = value;
            Offset = offset;
            Parent = parent;
        }

        public Task<bool> Execute(ConcurrentQueue<long> incoming, ConcurrentQueue<long> outgoing, Dictionary<string, long> registers)
        {
            long value = 0;
            long offset = 0;
            if (long.TryParse(Value, out long val))
            {
                value = val;
            }
            else
            {
                if (!registers.ContainsKey(Value))
                {
                    registers.Add(Value, 0);
                }

                value = registers[Value];
            }

            if (long.TryParse(Offset, out long off))
            {
                offset = off;
            }
            else
            {
                if (!registers.ContainsKey(Offset))
                {
                    registers.Add(Offset, 0);
                }

                offset = registers[Offset];
            }

            if (value != 0)
            {
                Parent.StackPointer += (int)offset;
            }
            else
            {
                Parent.StackPointer++;
            }

            return Task.FromResult(false);
        }

        public Emulator Parent { get; }
    }
}
