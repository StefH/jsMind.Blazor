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
            await base.AddNode(parent, node);

            node.Parent = parent;

            Data.Nodes.Add(node);
        }

        protected override MindMapArrayNode? FindNode(string id)
        {
            return Data.Nodes.FirstOrDefault(node => node.Id == id);
        }
    }
}