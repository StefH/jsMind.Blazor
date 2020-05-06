using System.Collections.Generic;

namespace JsMind.Blazor.Events
{
    public class MindMapAddNodeEventArgs: MindMapEventArgs
    {
        public string ParentId { get; set; }

        public string NodeId { get; set; }

        public string Topic { get; set; }

        public IDictionary<string, string> Data { get; set; }
    }
}