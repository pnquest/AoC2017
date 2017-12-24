using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Emulator
{
    public interface IInstruction
    {
        Emulator Parent { get; }
        Task<bool> Execute(ConcurrentQueue<long> incoming, ConcurrentQueue<long> outgoing, Dictionary<string, long> registers);
    }
}