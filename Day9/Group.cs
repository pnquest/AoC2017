using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    public class Group
    {
        public string Content { get; set; } = string.Empty;
        public List<Group> Groups { get; } = new List<Group>();
        public Group Parent { get; set; }
        public int GarbageCount { get; set; } = 0;

        public int GarbageSum => Groups.Sum(c => c.GarbageSum) + GarbageCount;

        public int Score => (Parent?.Score ?? 0) + 1;

        public int TotalScore => Groups.Sum(s => s.TotalScore) + Score;

        public void AppendContent(char c)
        {
            Content += c;
            if (Parent != null)
            {
                Parent.Content += c;
            }
        }
    }
}
