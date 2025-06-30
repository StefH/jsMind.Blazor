# jsMind.Blazor
A Blazor JSInterop wrapper for [jsMind](https://github.com/hizzgdev/jsmind).

[![NuGet](https://img.shields.io/nuget/v/jsMind.Blazor)](https://www.nuget.org/packages/jsMind.Blazor)

![Example](https://raw.githubusercontent.com/StefH/jsMind.Blazor/master/resources/example.png "example")

### Live Demo
https://stefh.github.io/jsMind.Blazor

### Supported Functionality
See [Wiki : Supported-Functionality](https://github.com/StefH/jsMind.Blazor/wiki/Supported-Functionality)

### Usage

#### Install the NuGet

```
PM> Install-Package jsMind.Blazor
```

#### Add the required javascripts and stylesheet to _Host.cshtml (Server) or index.html (WebAssembly)
``` diff
<head>
. . .
+    <script type="text/javascript" src="_content/jsMind.Blazor/jsmind.min.js"></script>
+    <script type="text/javascript" src="_content/jsMind.Blazor/jsmind-interop.min.js"></script>

+    <link type="text/css" rel="stylesheet" href="_content/jsMind.Blazor/jsmind.min.css" />
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


## Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=StefH) and [Dapper Plus](https://dapper-plus.net/?utm_source=StefH) are major sponsors and proud to contribute to the development of **jsMind.Blazor**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StefH/resources/main/sponsor/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=StefH)

[![Dapper Plus](https://raw.githubusercontent.com/StefH/resources/main/sponsor/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=StefH)