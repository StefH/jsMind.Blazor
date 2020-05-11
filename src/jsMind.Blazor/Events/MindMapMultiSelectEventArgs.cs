using System.Collections.Generic;
using JsMind.Blazor.Models;

namespace JsMind.Blazor.Events
{
    public class MindMapMultiSelectEventArgs<T> : MindMapEventArgs<T> where T : MindMapBaseNode
    {
        public ICollection<T> Nodes { get; set; }
    }
}