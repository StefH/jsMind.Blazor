using System.Collections.Generic;
using JsMind.Blazor.Models;

namespace JsMind.Blazor.Events
{
    public class MindMapAddNodeEventArgs<T> : MindMapEventArgs<T> where T : MindMapBaseNode
    {
        public string ParentId { get; set; }

        public string NodeId { get; set; }

        public string Topic { get; set; }

        public IDictionary<string, string> Data { get; set; }
    }
}