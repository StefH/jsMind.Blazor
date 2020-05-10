using System.Linq;
using System.Threading.Tasks;
using JsMind.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace JsMind.Blazor.Components
{
    public class MindMapArrayContainer : MindMapContainer<MindMapArrayNode>
    {
        [Parameter]
        public MindMapArrayData Data { get; set; }

        protected override object MindMapData => Data;

        public override async ValueTask AddNode(MindMapArrayNode parent, MindMapArrayNode node)
        {
            await AddNodeInternal(parent, node);

            node.Parent = parent;

            Data.Nodes.Add(node);
        }

        public override async ValueTask RemoveNode(MindMapArrayNode node)
        {
            await RemoveNodeInternal(node);

            var existingNode = FindNode(node.Id);
            if (existingNode is { })
            {
                Data.Nodes.Remove(existingNode);
            }
        }

        protected override MindMapArrayNode? FindNode(string id)
        {
            return Data.Nodes.First(node => node.Id == id);
        }
    }
}