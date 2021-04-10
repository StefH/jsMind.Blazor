using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsMind.Blazor.Events.Interop
{
    public class InteropEventData
    {
        [JsonPropertyName("evt")]
        public string Type { get; set; }

        public JsonElement[] Data { get; set; }

        [JsonPropertyName("node")]
        public string NodeId { get; set; }

        public string ContainerId { get; set; }
    }
}