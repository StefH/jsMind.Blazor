using System.Text.Json.Serialization;

namespace JsMind.Blazor.Events.Interop
{
    public class InteropNodeEventData : InteropEventData
    {
        [JsonPropertyName("node")]
        public string NodeId { get; set; }
    }
}