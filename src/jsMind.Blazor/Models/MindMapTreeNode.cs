using System.Collections.Generic;

namespace JsMind.Blazor.Models
{
    public class MindMapTreeNode : MindMapBaseNode
    {
        public ICollection<MindMapTreeNode> Children { get; set; } = new List<MindMapTreeNode>();
    }
}