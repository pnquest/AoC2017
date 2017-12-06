namespace Day6
{
    public class Bank
    {
        public int Index { get; }
        public int Blocks { get; set; }

        public Bank(int index, int blocks)
        {
            Index = index;
            Blocks = blocks;
        }

        public override string ToString()
        {
            return Blocks.ToString();
        }
    }
}
