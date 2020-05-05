using System;
using System.Collections.Generic;
using JsMind.Blazor.Models;

namespace JsMind.Blazor.Components
{
    public class MindMapData
    {
        public MindMapTreeNode RootNode { get; set; }

        public ICollection<MindMapTreeNode> Nodes { get; set; }

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