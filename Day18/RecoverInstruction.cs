using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Day18
{
    public class RecoverInstruction : IInstruction
    {
        public string Value { get; }

        public RecoverInstruction(string value, Emulator parent)
        {
            Value = value;
            Parent = parent;
        }

        public Task<bool> Execute(ConcurrentQueue<long> incoming,  ConcurrentQueue<long> outgoing, Dictionary<string, long> registers)
        {
            return Task.Run(async () =>
            {
                int count = 0;
                long res = 0;
                while (!incoming.TryDequeue(out res))
                {
                    if (count++ == 600)
                    {
                        throw new TimeoutException("timeout");
                    }
                    await Task.Delay(150).ConfigureAwait(false);
                }

                registers[Value] = res;
                Parent.StackPointer++;

                return true;
            });
        }

        public Emulator Parent { get; }
    }
}
