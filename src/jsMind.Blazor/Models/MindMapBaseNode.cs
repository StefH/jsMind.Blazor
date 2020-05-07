using System.Collections.Generic;
using System.Text.Json.Serialization;
using JsMind.Blazor.Constants;

namespace JsMind.Blazor.Models
{
    public abstract class MindMapBaseNode
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public bool Expanded { get; set; }

        public string Direction { get; set; } = MindMapNodeDirection.Right;

        [JsonIgnore]
        public IDictionary<string, string>? Data { get; set; }

        private MindMapBaseNode? _parent;
        private string? _parentId;

        [JsonIgnore]
        public MindMapBaseNode? Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                _parentId = value?.Id;
            }
        }
        
        [JsonPropertyName("parentid")]
        public string? ParentId
        {
            get => Parent?.Id ?? _parentId;

            set
            {
                _parent = null;
                _parentId = value;
            }
        }
    }
}