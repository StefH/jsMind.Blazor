using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JsMind.Blazor.Events;
using JsMind.Blazor.Events.Interop;
using Microsoft.JSInterop;

namespace JsMind.Blazor.Components
{
    public partial class MindMapContainer<T>
    {
        [JSInvokable]
        public async ValueTask OnShowCallback(InteropEventData evt)
        {
            switch (evt.Type)
            {
                // this.jm.invoke_event_handle(jm.event_type.show, { evt: 'expand_node', data: [], node: node.id });
                case "expand_node":
                    // todo
                    break;

                // this.jm.invoke_event_handle(jm.event_type.show, { evt: 'collapse_node', data: [], node: node.id });
                case "collapse_node":
                    // todo
                    break;

                // { evt: "done", node: "", data: [ ], containerId: containerId }
                case "done":
                    await OnShow.InvokeAsync(new ValueEventArgs<string>
                    {
                        Value = evt.ContainerId
                    });
                    break;
            }
        }

        [JSInvokable]
        public async ValueTask OnResizeCallback(InteropEventData evt)
        {
            switch (evt.Type)
            {
                // { evt: "resize", data: [], containerId: containerId }
                case "resize":
                    await OnResize.InvokeAsync(new ValueEventArgs<string>
                    {
                        Value = evt.ContainerId
                    });
                    break;
            }
        }

        [JSInvokable]
        public async ValueTask OnEditCallback(InteropNodeEventData evt)
        {
            switch (evt.Type)
            {
                case "add_nodeTODO":
                    // this.invoke_event_handle(jm.event_type.edit, { evt: 'add_node', data: [parent_node.id, nodeid, topic, data], node: nodeid });
                    await OnAddNode.InvokeAsync(new MindMapAddNodeEventArgs<T>
                    {
                        Node = FindNode(evt.NodeId),
                        Parent = FindNode(evt.Data[0].GetString()),
                        // NodeId = evt.Data[1].GetString(),
                        // Topic = evt.Data[2].GetString(),
                        Data = JsonSerializer.Deserialize<IDictionary<string, string>>(evt.Data[3].GetRawText())
                    });
                    break;
            }
        }

        [JSInvokable]
        public async ValueTask OnSelectCallback(InteropNodeEventData evt)
        {
            switch (evt.Type)
            {
                case "select_node":
                    // this.invoke_event_handle(jm.event_type.select, { evt: 'select_node', data: [], node: node.id });

                    var node = FindNode(evt.NodeId);
                    SelectedNodes = new List<T> { node };

                    await OnSelectNode.InvokeAsync(new MindMapSingleSelectEventArgs<T>
                    {
                        Node = node,
                        Selected = node is not null && SelectedNodes.Any(n => n.Id == node.Id)
                    });
                    break;
            }
        }

        [JSInvokable]
        public async ValueTask OnMultiSelectCallback(InteropMultiSelectEventData evt)
        {
            SelectedNodes = evt.Ids.Select(FindNode).Where(node => node != null).Cast<T>().ToList();

            await OnMultiSelectNodes.InvokeAsync(new MindMapMultiSelectEventArgs<T>
            {
                Node = FindNode(evt.Id),
                Nodes = SelectedNodes
            });
        }

        [JSInvokable]
        public async ValueTask OnUnselectCallback(InteropNodeEventData evt)
        {
            SelectedNodes = new List<T>();
            await OnClearSelect.InvokeAsync(new ValueEventArgs<string>
            {
                Value = evt.NodeId
            });
        }

        protected abstract T? FindNode(string id);
    }
}