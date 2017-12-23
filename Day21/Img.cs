using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    public class Img
    {
        public  List<List<string>> Rows { get; private set; } = new List<List<string>>();
        public int Row { get; set; }
        public int Col { get; set; }
        public Img(string input)
        {
            foreach (string row in input.Split("/"))
            {
                Rows.Add(row.ToCharArray().Select(c => c.ToString()).ToList());
            }
        }

        private Img(IEnumerable<List<string>> rows)
        {
            foreach (List<string> row in rows)
            {
                Rows.Add(row.Select(s => s).ToList());
            }
        }

        public Img Flip()
        {
            Img ret = new Img(Rows);

            int flipIdx = ret.Rows.First().Count - 1;

            foreach (List<string> row in ret.Rows)
            {
                string cE = row[flipIdx];
                string cF = row[0];
                row.RemoveAt(flipIdx);
                row.RemoveAt(0);
                row.Insert(0, cE);
                row.Add(cF);
            }

            return ret;
        }

        public Img Rotate()
        {
            Img ret = new Img(Rows);

            int cnt = ret.Rows.First().Count;

            List<List<string>> flipped = new List<List<string>>();

            for (int i = 0; i < cnt; i++)
            {
                List<string> list = ret.Rows.Select(s => s[i]).ToList();
                list.Reverse();
                flipped.Add(list);
            }

            ret.Rows = flipped;

            return ret;
        }

        public Img Assemble(IEnumerable<Img> others)
        {
            var imageGroups = others.Append(this).GroupBy(g => g.Row).OrderBy(g => g.Key);

            List<List<string>> final = new List<List<string>>();
            foreach (IGrouping<int, Img> imageGroup in imageGroups)
            {
                List<List<string>> assembled = new List<List<string>>();
                for (int i = 0; i < this.Rows.Count; i++)
                {
                    assembled.Add(new List<string>());
                }
                foreach (Img img in imageGroup.OrderBy(g => g.Col))
                {
                    for (int i = 0; i < img.Rows.Count; i++)
                    {
                        assembled[i].AddRange(img.Rows[i]);
                    }
                }

                final.AddRange(assembled);
            }

            return  new Img(final);
        }

        public IEnumerable<Img> Split(int size)
        {
            for (int yOffset = 0; yOffset < Rows.Count; yOffset += size)
            {
                for (int xOffset = 0; xOffset < Rows.Count; xOffset += size)
                {
                    List<List<string>> sub = new List<List<string>>();
                    for (int i = 0; i < size; i++)
                    {
                        sub.Add(new List<string>());
                    }
                    for (int y = 0; y < size; y++)
                    {
                        for (int x = 0; x < size; x++)
                        {
                            sub[y].Add(Rows[y + yOffset][x + xOffset]);
                        }
                    }

                     yield return new Img(sub)
                    {
                        Row = yOffset / size,
                        Col = xOffset / size
                    };
                }
            }
        }

        public override string ToString()
        {
            return string.Join("/", Rows.Select(r => string.Join(string.Empty, r)));
        }
    }
}
