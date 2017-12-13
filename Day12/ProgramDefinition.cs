using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    public class ProgramDefinition
    {
        public int Id { get; }
        public List<ProgramDefinition> Links { get; } = new List<ProgramDefinition>();
        public Guid GroupId { get; private set; }

        public ProgramDefinition(int id)
        {
            Id = id;
        }

        private IEnumerable<ProgramDefinition> GetLinkedProgramsInternal()
        {
            if (GroupId == default(Guid))
            {
                yield return this;
            }
            foreach (ProgramDefinition link in Links.Where(l => l.GroupId == default(Guid)))
            {
                foreach (ProgramDefinition def in link.GetLinkedProgramsInternal())
                {
                    if (def.GroupId == default(Guid))
                    {
                        def.GroupId = GroupId;
                        yield return def;
                    }
                }
            }
            
        }

        public void AssignGroups()
        {
            GroupId = Guid.NewGuid();
            var programDefinitions = GetLinkedProgramsInternal().ToArray();
        }

        protected bool Equals(ProgramDefinition other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProgramDefinition) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
