using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace JsMind.Blazor.Models
{
    public class MindMapArrayNode : MindMapBaseNode
    {
        private MindMapArrayNode _parent;
        private string _parentId;

        [CanBeNull]
        [JsonIgnore]
        public MindMapArrayNode Parent
        {
            get => _parent;
            set
            {
                _parent = value;
                _parentId = value?.Id;
            }
        }
    

        [JsonPropertyName("parentid")]
        [CanBeNull]
        public string ParentId
        {
            get => Parent?.Id ?? _parentId;

            set
            {
                _parent = null;
                _parentId = value;
            }
        }

        [JsonPropertyName("isroot")]
        public bool IsRoot { get; set; }
    }
}