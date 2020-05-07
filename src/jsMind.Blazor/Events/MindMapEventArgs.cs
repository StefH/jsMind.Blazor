using System;
using JsMind.Blazor.Models;

namespace JsMind.Blazor.Events
{
    public class MindMapEventArgs<T> : EventArgs where T : MindMapBaseNode
    {
        public T? Node { get; set; }
    }
}