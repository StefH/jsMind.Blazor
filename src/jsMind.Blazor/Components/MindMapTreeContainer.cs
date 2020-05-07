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

        public override async Task AddNode(MindMapTreeNode parent, MindMapTreeNode node)
        {
            await base.AddNode(parent, node);

            parent.Children.Add(node);
            node.Parent = parent;
        }

        protected override MindMapTreeNode? FindNode(string id)
        {
            return FindTreeNode(Data.RootNode, id);
        }

        protected MindMapTreeNode? FindTreeNode(MindMapTreeNode node, string id)
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