using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JsMind.Blazor.Models
{
    public abstract class MindMapData
    {
        //[JsonIgnore]
        //public MindMapTreeNode? RootNode { get; set; }

        //[JsonIgnore]
        //public ICollection<MindMapArrayNode>? Nodes { get; set; }

        //public object? Data => (object?) RootNode ?? Nodes;

        public virtual string Format { get; }
    }
}