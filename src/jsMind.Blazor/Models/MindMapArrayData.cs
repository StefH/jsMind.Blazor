using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JsMind.Blazor.Models
{
    public class MindMapArrayData : MindMapData
    {
        public override string Format => "node_array";

        [JsonPropertyName("data")]
        public ICollection<MindMapArrayNode> Nodes { get; set; }
    }
}