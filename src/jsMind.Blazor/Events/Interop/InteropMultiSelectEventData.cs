using System.Collections.Generic;

namespace JsMind.Blazor.Events.Interop
{
    public class InteropMultiSelectEventData
    {
        public string Id { get; set; }

        public ICollection<string> Ids { get; set; }
    }
}