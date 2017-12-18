using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day18
{
    public class Emulator
    {
        public long LastPlayed { get; private set; }
        public long LastRecovered { get; private set; }
        public ConcurrentQueue<long> SendQueue { get; } = new ConcurrentQueue<long>();
        public ConcurrentQueue<long> ReceiveQueue { get; set; }
        public bool Terminated { get; private set; }
        public int StackPointer { get; set; } = 0;
        public int SendCount { get; set; } = 0;
        private long _programId;

        private Dictionary<string, long> _registers = new Dictionary<string, long>();
        private List<IInstruction> _instructions = new List<IInstruction>();

        public Emulator(long programId)
        {
            _registers["p"] = programId;
            _programId = programId;
        }

        public async Task NextInst()
        {
            try
            {
                Console.WriteLine($"program {_programId} running {StackPointer}");
                await _instructions[StackPointer].Execute(ReceiveQueue, SendQueue, _registers).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Terminated = true;
            }
            
        }

        public void LoadInstruction(string inst)
        {
            string[] instParts = inst.Split(' ');
            if (inst.StartsWith("snd"))
            {
                _instructions.Add(new SoundInstruction(inst.Split(' ')[1].Trim(), this));
            }
            else if (inst.StartsWith("set"))
            {
                _instructions.Add(new SetInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
            }
            else if (inst.StartsWith("add"))
            {
                _instructions.Add(new AddInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
            }
            else if (inst.StartsWith("mul"))
            {
                _instructions.Add(new MulInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
            }
            else if (inst.StartsWith("mod"))
            {
                _instructions.Add(new ModInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
            }
            else if (inst.StartsWith("rcv"))
            {
                _instructions.Add(new RecoverInstruction(instParts[1].Trim(), this));
            }
            else if (inst.StartsWith("jgz"))
            {
                _instructions.Add(new JumpInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
            }
            else
            {
                throw new ArgumentException("bad input");
            }
        }

        public void LoadInstruction(IInstruction inst)
        {
            _instructions.Add(inst);
        }
    }
}
