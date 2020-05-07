using System.Collections.Generic;
using JsMind.Blazor.Models;

namespace JsMind.Blazor.Events
{
    public class MindMapAddNodeEventArgs<T> : MindMapEventArgs<T> where T : MindMapBaseNode
    {
        public T? Parent { get; set; }

        public T? Node { get; set; }

        public IDictionary<string, string> Data { get; set; }
    }
}