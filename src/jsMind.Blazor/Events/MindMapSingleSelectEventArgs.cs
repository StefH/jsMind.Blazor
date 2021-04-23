using JsMind.Blazor.Models;

namespace JsMind.Blazor.Events
{
    public class MindMapSingleSelectEventArgs<T> : MindMapEventArgs<T> where T : MindMapBaseNode
    {
        public bool Selected { get; set; }
    }
}