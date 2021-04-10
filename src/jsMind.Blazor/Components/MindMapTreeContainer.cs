using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsMind.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace JsMind.Blazor.Components
{
    public class MindMapTreeContainer : MindMapContainer<MindMapTreeNode>
    {
        private MindMapTreeData? _data;

        [Parameter]
        public MindMapTreeData? Data
        {
            get => _data;

            set
            {
                _data = value;
                if (_data is { })
                {
                    Nodes = Flatten(new[] { _data.RootNode });
                }
            }
        }

        public IEnumerable<MindMapTreeNode>? Nodes { get; private set; }

        protected override object? MindMapData => _data;

        public override async ValueTask AddNode(MindMapTreeNode parent, MindMapTreeNode node)
        {
            await AddNodeInternal(parent, node);

            parent.Children.Add(node);
            node.Parent = parent;
        }

        public override async ValueTask RemoveNode(MindMapTreeNode node)
        {
            await RemoveNodeInternal(node);

            var existingNode = FindTreeNode(_data?.RootNode, node.Id);
            if (existingNode is not null)
            {
                foreach (var childNode in existingNode.Children.ToList())
                {
                    await RemoveNode(childNode);
                }

                if (existingNode.Parent is MindMapTreeNode parent)
                {
                    parent.Children.Remove(existingNode);
                }
            }
        }

        protected override MindMapTreeNode? FindNode(string id)
        {
            return FindTreeNode(_data?.RootNode, id);
        }

        private MindMapTreeNode? FindTreeNode(MindMapTreeNode? node, string id)
        {
            if (node is null)
            {
                return null;
            }

            if (node.Id == id)
            {
                return node;
            }

            foreach (var childNode in node.Children)
            {
                var match = FindTreeNode(childNode, id);
                if (match is not null)
                {
                    return match;
                }
            }

            return null;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/11830174/how-to-flatten-tree-via-linq
        /// </summary>
        private static IEnumerable<MindMapTreeNode> Flatten(IEnumerable<MindMapTreeNode> nodes) => 
            nodes
                .SelectMany(c => Flatten(c.Children))
                .Concat(nodes);
    }
}