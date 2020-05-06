namespace JsMind.Blazor.Models
{
    public abstract class MindMapBaseNode
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public bool Expanded { get; set; }

        public string Direction { get; set; } = MindMapNodeDirection.Right;
    }
}