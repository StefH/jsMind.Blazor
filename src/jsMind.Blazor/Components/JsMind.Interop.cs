using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.JSInterop;

namespace jsMind.Blazor.Components
{
    public partial class Howl
    {
        [JSInvokable]
        public void OnPlayCallback(int soundId, int durationInSeconds)
        {
        }
    }
}