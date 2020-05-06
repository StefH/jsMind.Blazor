using System;
using System.Collections.Generic;

namespace JsMind.Blazor.Models
{
    public class MindMapData
    {
        public MindMapTreeNode RootNode { get; set; }

        public ICollection<MindMapArrayNode> Nodes { get; set; }

        public object Data => (object) RootNode ?? Nodes;

        public string Format
        {
            get
            {
                if (RootNode is { })
                {
                    return "node_tree";
                }

                if (Nodes is { })
                {
                    return "node_array";
                }

                throw new NotSupportedException();
            }
        }
    }
}