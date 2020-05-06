using System.Text.Json.Serialization;

namespace JsMind.Blazor.Models
{
    public class MindMapArrayNode : MindMapBaseNode
    {
        [JsonPropertyName("isroot")]
        public bool IsRoot { get; set; }
    }
}