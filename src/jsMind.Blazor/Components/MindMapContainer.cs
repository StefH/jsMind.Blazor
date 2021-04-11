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
    public abstract partial class MindMapContainer<T> : ComponentBase, IDisposable
        where T : MindMapBaseNode
    {
        private readonly string _containerId = "jsMind_container_" + Guid.NewGuid();

        [Inject]
        private IJSRuntime Runtime { get; set; }

        [Parameter]
        public EventCallback<MindMapEventArgs<T>> OnSelectNode { get; set; }

        [Parameter]
        public EventCallback<MindMapMultiSelectEventArgs<T>> OnMultiSelectNodes { get; set; }

        [Parameter]
        public EventCallback<MindMapAddNodeEventArgs<T>> OnAddNode { get; set; }

        [Parameter]
        public EventCallback<ValueEventArgs<string>> OnShow { get; set; }

        [Parameter]
        public MindMapOptions Options { get; set; }

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

                await Show();
            }
        }

        protected abstract object? MindMapData { get; }

        public List<T> SelectedNodes = new List<T>();

        public void Dispose()
        {
            try
            {
                DisposeMindMap();
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