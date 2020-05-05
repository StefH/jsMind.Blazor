using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace JsMind.Blazor.Models
{
    public class MindMapTreeNode
    {
        public string Id { get; set; }

        [NotNull]
        public string Topic { get; set; }

        //[CanBeNull]
        //public MindMapTreeNode Parent { get; set; }

        [JsonPropertyName("parentid")]
        [CanBeNull]
        public string ParentId { get; set; }

        [JsonPropertyName("isroot")]
        public bool IsRoot { get; set; }

        public bool Expanded { get; set; }

        public string Direction { get; set; } = MindMapNodeDirection.Right;

        public ICollection<MindMapTreeNode> Children { get; set; } = new List<MindMapTreeNode>();
    }
}