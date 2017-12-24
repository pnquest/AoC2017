using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    public class Bridge
    {
        private List<Link> _available;
        public List<Link> Used { get; }
        public List<Bridge> SubBridges { get; }
        public bool IsComplete { get; private set; }

        public int MyScore => Used.Sum(u => u.Strength);
        public int MaxScore => SubBridges.Select(s => s.MaxScore).Append(MyScore).Max();

        public Bridge(IEnumerable<Link> available)
        {
            _available = available.ToList();
            Used = new List<Link>();
            SubBridges = new List<Bridge>();
            IsComplete = false;
        }

        private Bridge(List<Link> available, List<Link> used, List<Bridge> subBridges, bool isComplete)
        {
            _available = available.Select(s => s).ToList();
            Used = used.Select(s => s).ToList();
            SubBridges = subBridges.Select(s => s).ToList();
            IsComplete = isComplete;
        }

        public Bridge GetLongest()
        {
            var others = SubBridges.Select(b => b.GetLongest());
            Bridge[] vals = others.Append(this).ToArray();
            return vals.OrderByDescending(b => b.Used.Count).ThenByDescending(b => b.MyScore).First();
        }

        public bool IsAllComplete()
        {
            if (IsComplete)
            {
                foreach (Bridge bridge in SubBridges)
                {
                    if (!bridge.IsAllComplete())
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private void UsePart(Link l)
        {
            _available.Remove(l);
            if (Used.Any())
            {
                l.UseSide(Used.Last().FirstFreeSide().Value);
            }
            else
            {
                l.UseSide(0);
            }
            Used.Add(l);
        }

        public void SolveNext()
        {
            foreach (Bridge bridge in SubBridges.Where(b => !b.IsAllComplete()))
            {
                bridge.SolveNext();
            }
            Link[] avail;
            var used = Used;
            if (!used.Any())
            {
                avail = _available.Where(a => a.HasFreeSide(0)).ToArray();
            }
            else
            {
                avail = _available.Where(a => a.HasFreeSide(used.Last().FirstFreeSide().Value)).ToArray();
            }

            if (avail.Length == 0)
            {
                IsComplete = true;
                return;
            }

            for (int i = 1; i < avail.Length; i++)
            {
                Bridge sub = new Bridge(_available, Used, new List<Bridge>(), IsComplete);
                sub.UsePart(avail[i]);
                SubBridges.Add(sub);
            }

            UsePart(avail[0]);
        }
    }
}
