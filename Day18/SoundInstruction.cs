using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day18
{
    public class SoundInstruction : IInstruction
    {
        public string Tone { get; }

        public SoundInstruction(string tone, Emulator parent)
        {
            Tone = tone;
            Parent = parent;
        }

        public Task<bool> Execute(ConcurrentQueue<long> incoming, ConcurrentQueue<long> outgoing, Dictionary<string, long> registers)
        {
            if (long.TryParse(Tone, out long tone))
            {
                outgoing.Enqueue(tone);
            }
            else
            {
                if (!registers.ContainsKey(Tone))
                {
                    registers.Add(Tone, 0);
                }
                long regTone = registers[Tone];
                outgoing.Enqueue(regTone);
            }

            Parent.StackPointer++;
            Parent.SendCount++;
            return Task.FromResult(false);
        }

        public Emulator Parent { get; }
    }
}
