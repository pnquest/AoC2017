using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>();
            LoadTree(nodes);

            TreeNode bottomNode = nodes.Select(n => n.Value).First(n => n.Parents.Count == 0 && n.Children.Count > 0);

            TreeNode curBottom = bottomNode;

            while (curBottom.Children.Any(b => !b.ChildrenAreBalanced))
            {
                curBottom = curBottom.Children.First(f => !f.ChildrenAreBalanced);
            }

            Console.WriteLine($"The Unbalanced Node is {curBottom.Name}. Its children are");
            foreach (TreeNode child in curBottom.Children)
            {
                Console.WriteLine($"{child.Name}... Tree Weight: {child.TreeWeight} Weight: {child.Weight}");
            }
            Console.ReadKey(true);
        }

        private static void LoadTree(Dictionary<string, TreeNode> nodes)
        {
            Dictionary<TreeNode, string> supports = new Dictionary<TreeNode, string>();
            Regex inputRegex = new Regex(@"(?<name>[^ ]+) \((?<weight>\d+)\)(?: -> ){0,1}(?<supporting>.*)");
            foreach (string line in FileIterator.Create("input.txt"))
            {
                Match m = inputRegex.Match(line);
                string name = m.Groups["name"].Captures[0].Value;
                int weight = int.Parse(m.Groups["weight"].Captures[0].Value);
                string supporters = m.Groups["supporting"].Captures[0].Value;

                TreeNode node = new TreeNode(name, weight);
                nodes.Add(name, node);
                if (!string.IsNullOrEmpty(supporters))
                {
                    supports.Add(node, supporters);
                }
            }

            foreach (KeyValuePair<TreeNode, string> pair in supports)
            {
                TreeNode node = pair.Key;

                foreach (string supported in pair.Value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()))
                {
                    TreeNode other = nodes[supported];
                    other.Parents.Add(node);
                    node.Children.Add(other);
                }
            }
        }
    }
}
