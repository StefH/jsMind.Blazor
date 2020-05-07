using System.Text.Json.Serialization;

namespace JsMind.Blazor.Models
{
    public class MindMapTreeData : MindMapData
    {
        public override string Format => "node_tree";

        [JsonPropertyName("data")]
        public MindMapTreeNode RootNode { get; set; }
    }
}