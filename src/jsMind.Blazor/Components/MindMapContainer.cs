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
    public abstract partial class MindMapContainer<T> : ComponentBase, IDisposable
        where T : MindMapBaseNode
    {
        protected readonly string ContainerId = "jsMind_container_" + Guid.NewGuid();

        private DotNetObjectReference<MindMapContainer<T>> _dotNetObjectReference;

        [Inject]
        private IJSRuntime Runtime { get; set; }

        [Parameter]
        public EventCallback<MindMapEventArgs<T>> OnSelectNode { get; set; }

        [Parameter]
        public EventCallback<MindMapAddNodeEventArgs<T>> OnAddNode { get; set; }

        [Parameter]
        public MindMapOptions Options { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "id", ContainerId);
            builder.AddMultipleAttributes(2, AdditionalAttributes);
            builder.CloseElement();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _dotNetObjectReference = DotNetObjectReference.Create(this);

                await Runtime.InvokeVoidAsync("MindMap.show", _dotNetObjectReference, ContainerId, Options, MindMapData);
            }
        }

        protected abstract object MindMapData { get; }

        public virtual async Task AddNode(T parent, T node)
        {
            await Runtime.InvokeVoidAsync("MindMap.addNode", ContainerId, parent.Id, node.Id, node.Topic, node.Data);
        }

        public void Dispose()
        {
            try
            {
                Runtime.InvokeVoidAsync("MindMap.dispose", ContainerId);
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