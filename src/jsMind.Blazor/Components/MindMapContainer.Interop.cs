using System.Threading.Tasks;
using JsMind.Blazor.Events;
using JsMind.Blazor.Events.Interop;
using Microsoft.JSInterop;

namespace JsMind.Blazor.Components
{
    public partial class MindMapContainer
    {
        [JSInvokable]
        public void OnShowCallback(InteropEventData data)
        {
        }

        [JSInvokable]
        public void OnResizeCallback(InteropEventData data)
        {
        }

        [JSInvokable]
        public void OnEditCallback(InteropEventData data)
        {
        }

        [JSInvokable]
        public async Task OnSelectCallback(InteropEventData data)
        {
            switch (data.Type)
            {
                case "select_node":
                    await OnSelectNode.InvokeAsync(new MindMapEventArgs { NodeId =  data.NodeId });
                    break;
            }
        }
    }
}