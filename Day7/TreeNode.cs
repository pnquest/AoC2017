using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    public class TreeNode
    {
        public string Name { get; }
        public int  Weight { get; }
        public int TreeWeight => _lazyWeight.Value;
        public List<TreeNode> Parents { get; } = new List<TreeNode>();
        public List<TreeNode> Children { get; } = new List<TreeNode>();

        public bool ChildrenAreBalanced
        {
            get
            {
                if (!Children.Any())
                    return true;

                int firstWeight = Children.First().TreeWeight;

                return Children.All(c => c.TreeWeight == firstWeight);
            }
        }

        public bool IsBalanced
        {
            get
            {
                if (!Parents.Any())
                {
                    return true;
                }

                return Parents.First().Children.All(c => c.TreeWeight == TreeWeight);
            }
        }

        private readonly Lazy<int> _lazyWeight;

        public TreeNode(string name, int weight)
        {
            Name = name;
            Weight = weight;
            _lazyWeight = new Lazy<int>(() => Children.Sum(c => c.TreeWeight) + Weight);
        }


    }
}
