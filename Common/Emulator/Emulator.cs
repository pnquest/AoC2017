using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Emulator
{
    public class Emulator
    {
        public Dictionary<string, int> DebugCounts { get; } = new Dictionary<string, int>();
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

        public Emulator(Dictionary<string, long> registers)
        {
            _registers = registers;
        }

        public long GetRegisterValue(string reg)
        {
            return _registers[reg];
        }

        public async Task NextInst()
        {
            try
            {
                Console.WriteLine($"program {_programId} running {StackPointer}");
                IInstruction instruction = _instructions[StackPointer];

                if (!DebugCounts.ContainsKey(instruction.GetType().Name))
                {
                    DebugCounts.Add(instruction.GetType().Name, 0);
                }
                DebugCounts[instruction.GetType().Name]++;
                await instruction.Execute(ReceiveQueue, SendQueue, _registers).ConfigureAwait(false);
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
                _instructions.Add(new JumpGZInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
            }
            else if (inst.StartsWith("sub"))
            {
                _instructions.Add(new SubInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
            }
            else if (inst.StartsWith("jnz"))
            {
                _instructions.Add(new JumpNZInstruction(instParts[1].Trim(), instParts[2].Trim(), this));
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
