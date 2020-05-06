using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JsMind.Blazor.Events;
using JsMind.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace JsMind.Blazor.Components
{
    /// <summary>
    /// Shows a MindMap
    /// </summary>
    public partial class MindMapContainer : ComponentBase, IDisposable
    {
        private readonly string _containerId = "jsMind_container_" + Guid.NewGuid();

        private DotNetObjectReference<MindMapContainer> _dotNetObjectReference;

        [Inject]
        private IJSRuntime Runtime { get; set; }

        [Parameter]
        public EventCallback<MindMapEventArgs> OnSelectNode { get; set; }

        [Parameter]
        public EventCallback<MindMapAddNodeEventArgs> OnAddNode { get; set; }

        [Parameter]
        public MindMapOptions Options { get; set; }

        [Parameter]
        public MindMapData Data { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", _containerId);
            builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.CloseElement();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetObjectReference = DotNetObjectReference.Create(this);

                await Runtime.InvokeVoidAsync("MindMap.show", _dotNetObjectReference, _containerId, Options, Data);
            }
        }

        public async Task AddNode(MindMapTreeNode parent, MindMapTreeNode node)
        {
            await Runtime.InvokeVoidAsync("MindMap.addNode", _containerId, parent.Id, node.Id, node.Topic, node.Data);

            parent.Children.Add(node);
            node.Parent = parent;
        }

        public void Dispose()
        {
            try
            {
                Runtime.InvokeVoidAsync("MindMap.dispose", _containerId);
            }
            catch
            {
                // just continue
            }

            GC.SuppressFinalize(this);

            // Now dispose our object reference so our component can be garbage collected
            _dotNetObjectReference?.Dispose();
        }
    }
}