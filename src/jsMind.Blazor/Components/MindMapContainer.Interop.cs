using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace JsMind.Blazor.Components
{
    public partial class MindMapContainer<T>
    {
        private DotNetObjectReference<MindMapContainer<T>> _dotNetObjectReference;

        private ValueTask Show()
        {
            return Runtime.InvokeVoidAsync("MindMap.show", _dotNetObjectReference, _containerId, Options, MindMapData);
        }

        public abstract ValueTask AddNode(T parent, T node);

        protected ValueTask AddNodeInternal(T parent, T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.addNode", _containerId, parent.Id, node.Id, node.Topic, node.Data);
        }

        public abstract ValueTask RemoveNode(T node);

        protected ValueTask RemoveNodeInternal(T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.removeNode", _containerId, node.Id);
        }

        public ValueTask SelectNode(T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.selectNode", _containerId, node.Id);
        }

        public ValueTask SelectNodes(List<T> nodes)
        {
            SelectedNodes = nodes;
            return Runtime.InvokeVoidAsync("MindMap.selectNodes", _containerId, nodes);
        }

        public ValueTask<T> GetNode(string id)
        {
            return Runtime.InvokeAsync<T>("MindMap.getNode", _containerId, id);
        }

        public ValueTask ClearSelect()
        {
            return Runtime.InvokeVoidAsync("MindMap.clearSelect", _containerId);
        }

        public ValueTask SetTheme(string theme)
        {
            return Runtime.InvokeVoidAsync("MindMap.setTheme", _containerId, theme);
        }

        public ValueTask SetEditable(bool edit)
        {
            Options.Editable = edit;

            return Runtime.InvokeVoidAsync(edit ? "MindMap.enableEdit" : "MindMap.disableEdit", _containerId);
        }

        public async ValueTask<bool> IsEditable()
        {
            Options.Editable = await Runtime.InvokeAsync<bool>("MindMap.isEditable", _containerId);

            return Options.Editable;
        }

        public ValueTask ExpandNode(T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.expandNode", _containerId, node.Id);
        }

        public ValueTask CollapseNode(T node)
        {
            return Runtime.InvokeVoidAsync("MindMap.collapseNode", _containerId, node.Id);
        }

        public ValueTask Expand()
        {
            return Runtime.InvokeVoidAsync("MindMap.expand", _containerId);
        }

        public ValueTask ExpandToDepth(int depth)
        {
            return Runtime.InvokeVoidAsync("MindMap.expandToDepth", _containerId, depth);
        }

        public ValueTask Collapse()
        {
            return Runtime.InvokeVoidAsync("MindMap.collapse", _containerId);
        }

        public async ValueTask SetReadonly(bool @readonly)
        {
            Options.Readonly = @readonly;

            await Runtime.InvokeVoidAsync("MindMap.setReadOnly", _containerId, @readonly);

            if (@readonly)
            {
                await SetEditable(false);
            }
        }

        public bool IsReadonly()
        {
            return Options.Readonly;
        }

        private ValueTask DisposeMindMap()
        {
            return Runtime.InvokeVoidAsync("MindMap.destroy", _containerId);
        }
    }
}