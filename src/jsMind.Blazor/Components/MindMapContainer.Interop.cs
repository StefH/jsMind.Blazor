using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace JsMind.Blazor.Components
{
    public partial class MindMapContainer<T>
    {
        private DotNetObjectReference<MindMapContainer<T>> _dotNetObjectReference;

        private ValueTask Show()
        {
            return Runtime.InvokeVoidAsync("MindMap.show", _dotNetObjectReference, ContainerId, Options, MindMapData);
        }

        public virtual ValueTask AddNode(T parent, T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.addNode", ContainerId, parent.Id, node.Id, node.Topic, node.Data);
        }

        public ValueTask SelectNode(T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.selectNode", ContainerId, node.Id);
        }

        public ValueTask ClearSelect()
        {
            return Runtime.InvokeVoidAsync("MindMap.clearSelect", ContainerId);
        }

        public ValueTask SetTheme(string theme)
        {
            return Runtime.InvokeVoidAsync("MindMap.setTheme", ContainerId, theme);
        }

        public ValueTask SetEditable(bool edit)
        {
            Options.Editable = edit;

            return Runtime.InvokeVoidAsync(edit ? "MindMap.enableEdit" : "MindMap.disableEdit", ContainerId);
        }

        public async ValueTask<bool> IsEditable()
        {
            Options.Editable = await Runtime.InvokeAsync<bool>("MindMap.isEditable", ContainerId);

            return Options.Editable;
        }

        public ValueTask ExpandNode(T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.expandNode", ContainerId, node.Id);
        }

        public ValueTask CollapseNode(T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.collapseNode", ContainerId, node.Id);
        }

        public ValueTask Expand()
        {
            return Runtime.InvokeVoidAsync("MindMap.expand", ContainerId);
        }

        public ValueTask ExpandToDepth()
        {
            return Runtime.InvokeVoidAsync("MindMap.expandToDepth", ContainerId);
        }

        public ValueTask Collapse()
        {
            return Runtime.InvokeVoidAsync("MindMap.collapse", ContainerId);
        }

        private ValueTask DisposeMindMap()
        {
            return Runtime.InvokeVoidAsync("MindMap.dispose", ContainerId);
        }
    }
}