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
    public partial class MindMapContainer : ComponentBase
    {
        private readonly string _containerId = "jsMind_container_" + Guid.NewGuid();

        private DotNetObjectReference<MindMapContainer> _dotNetObjectReference;

        [Inject]
        private IJSRuntime Runtime { get; set; }

        [Parameter]
        public EventCallback<MindMapEventArgs> OnSelectNode { get; set; }
        
        [Parameter]
        public MindMapOptions Options { get; set; }

        [Parameter]
        public MindMapData Data { get; set; }

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created <c>label</c> element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        protected override Task OnInitializedAsync()
        {
            _dotNetObjectReference = DotNetObjectReference.Create(this);

            return base.OnInitializedAsync();
        }

        /// <inheritdoc />
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
                await Runtime.InvokeVoidAsync("MindMap.show", _dotNetObjectReference, _containerId, Options, Data);
            }
        }
    }
}