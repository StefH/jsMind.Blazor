using JsMind.Blazor.Constants;

namespace JsMind.Blazor.Models
{
    public class MindMapOptions
    {
        public bool Editable { get; set; }

        public bool Readonly { get; set; }

        public bool MultiSelect { get; set; }

        public string Theme { get; set; } = MindMapThemes.Primary;
    }
}