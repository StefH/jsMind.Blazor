using System.Linq;
using System.Threading.Tasks;
using JsMind.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace JsMind.Blazor.Components
{
    public class MindMapTreeContainer : MindMapContainer<MindMapTreeNode>
    {
        [Parameter]
        public MindMapTreeData Data { get; set; }

        protected override object MindMapData => Data;

        public override async ValueTask AddNode(MindMapTreeNode parent, MindMapTreeNode node)
        {
            await AddNodeInternal(parent, node);

            parent.Children.Add(node);
            node.Parent = parent;
        }

        public override async ValueTask RemoveNode(MindMapTreeNode node)
        {
            await RemoveNodeInternal(node);

            var existingNode = FindTreeNode(Data.RootNode, node.Id);
            if (existingNode is { })
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
            return FindTreeNode(Data.RootNode, id);
        }

        private MindMapTreeNode? FindTreeNode(MindMapTreeNode node, string id)
        {
            if (node.Id == id)
            {
                return node;
            }

            foreach (var childNode in node.Children)
            {
                var match = FindTreeNode(childNode, id);
                if (match is { })
                {
                    return match;
                }
            }

            return null;
        }
    }
}