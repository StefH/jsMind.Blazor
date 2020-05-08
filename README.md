# jsMind.Blazor
A Blazor JSInterop wrapper for [jsMind](https://github.com/hizzgdev/jsmind)

### Live Demo
https://stefh.github.io/jsMind.Blazor (TODO)

### Usage

#### Install the NuGet

```
PM> Install-Package jsMind.Blazor
```

#### Add the required javascripts and stylesheet to _Host.cshtml (Server) or index.html (WebAssembly)
``` diff
<head>
. . .
+    <script type="text/javascript" src="_content/jsMind.Blazor/js/jsmind.js"></script>
+    <script type="text/javascript" src="_content/jsMind.Blazor/js/jsmind.draggable.js"></script>
+    <script type="text/javascript" src="_content/jsMind.Blazor/js/jsmind-interop.js"></script>

+    <link type="text/css" rel="stylesheet" href="_content/jsMind.Blazor/css/jsmind.css" />
</head>
```

#### Add the required imports to the _Imports.razor
``` diff
. . .
@using Microsoft.JSInterop
+ @using JsMind.Blazor.Components
+ @using JsMind.Blazor.Models
+ @using JsMind.Blazor.Events
+ @using JsMind.Blazor.Constants
```

#### Use the MindMapContainer
`razor-html`
``` html
@page "/"

<!-- Add the component -->
<MindMapTreeContainer @ref="_myTreeNodeContainer"
                      Options="@_options"
                      Data="@_treeData"
                      OnShow="@OnShowTree"
                      style="border:solid 1px blue; background:#f4f4f4;" />

```
`razor - @code`
``` c#
{
    private MindMapTreeContainer _myTreeNodeContainer;

    // Define the MindMapOptions
    readonly MindMapOptions _options = new MindMapOptions
    {
        Editable = false,
        Theme = MindMapThemes.Primary
    };

    // Define some MindMapTreeData
    readonly MindMapTreeData _treeData = new MindMapTreeData
    {
        RootNode = new MindMapTreeNode
        {
            Id = "root",
            Topic = "-Root-",
            Children = new List<MindMapTreeNode>
            {
                new MindMapTreeNode
                {
                    Id = "sub1.0",
                    Topic = "sub1.0-right"
                },
                new MindMapTreeNode
                {
                    Id = "sub1.1",
                    Topic = "sub1.1-right",
                    Children = new List<MindMapTreeNode>
                    {
                        new MindMapTreeNode
                        {
                            Id = "sub1.1a",
                            Topic = "sub1.1a-right"
                        }
                    }
                },
                new MindMapTreeNode
                {
                    Id = "sub2",
                    Topic = "sub2-left",
                    Direction = MindMapNodeDirection.Left
                }
            }
        }
    };

    async Task OnShowTree(EventArgs args)
    {
        // When the MindMap is displayed, expand all nodes
        await _myTreeNodeContainer.Expand();
    }
}
```