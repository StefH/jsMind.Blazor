using System;
using JsMind.Blazor.Models;

namespace JsMind.Blazor.Events
{
    public class MindMapEventArgs : EventArgs
    {
        public MindMapBaseNode? Node { get; set; }
    }
}