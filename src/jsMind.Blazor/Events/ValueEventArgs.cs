using System;

namespace JsMind.Blazor.Events
{
    public class ValueEventArgs<T> : EventArgs
    {
        public T? Value { get; set; }
    }
}