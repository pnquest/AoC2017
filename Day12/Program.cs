using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ProgramDefinition> Definitions = new List<ProgramDefinition>();
            foreach (string line in FileIterator.Create("input.txt"))
            {
                string[] halves = line.Split("<->");

                int id = int.Parse(halves[0].Trim());
                int[] links = halves[1].Trim().Split(',').Select(int.Parse).ToArray();

                ProgramDefinition def = Definitions.FirstOrDefault(d => d.Id == id);

                if (def == null)
                {
                    def = new ProgramDefinition(id);
                    Definitions.Add(def);
                }

                foreach (int link in links)
                {
                    ProgramDefinition lnk = Definitions.FirstOrDefault(d => d.Id == link);
                    if (lnk == null)
                    {
                        lnk = new ProgramDefinition(link);
                        Definitions.Add(lnk);
                    }
                    def.Links.Add(lnk);
                }
            }

            foreach (ProgramDefinition definition in Definitions)
            {
                if (definition.GroupId == default(Guid))
                {
                    definition.AssignGroups();
                }
            }

            int groups = Definitions.GroupBy(g => g.GroupId).Count();

            Console.WriteLine($"The number of groups is {groups}");
            Console.ReadKey(true);
        }
    }
}
