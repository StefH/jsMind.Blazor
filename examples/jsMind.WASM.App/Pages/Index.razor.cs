using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsMind.Blazor.Components;
using JsMind.Blazor.Constants;
using JsMind.Blazor.Events;
using JsMind.Blazor.Models;

namespace jsMind.WASM.App.Pages;

public partial class Index
{
    private MindMapTreeContainer _myTreeNodeContainer = null!;
    private MindMapArrayContainer _myArrayNodeContainer = null!;
    private string _selectedNodeId = string.Empty;
    private string[] _selectedNodeIds = Array.Empty<string>();

    private readonly MindMapOptions _options = new()
    {
        Editable = false,
        MultiSelect = true,
        Theme = MindMapThemes.Primary
    };

    private readonly MindMapTreeData _treeData = new()
    {
        RootNode = new MindMapTreeNode
        {
            Id = "root",
            Topic = "-Root-",
            Children = new List<MindMapTreeNode>
            {
                new()
                {
                    Id = "sub1.0",
                    Topic = "sub1.0-right"
                },
                new()
                {
                    Id = "sub1.1",
                    Topic = "sub1.1-right",
                    Children = new List<MindMapTreeNode>
                    {
                        new()
                        {
                            Id = "sub1.1a",
                            Topic = "sub1.1a-right"
                        },
                        new()
                        {
                            Id = "sub1.1b",
                            Topic = "sub1.1b-right"
                        }
                    }
                },
                new()
                {
                    Id = "sub2",
                    Topic = "sub2-left",
                    Direction = MindMapNodeDirection.Left
                }
            }
        }
    };

    private readonly MindMapArrayData _arrayData1 = new()
    {
        Nodes = new List<MindMapArrayNode>
        {
            new()
            {
                IsRoot = true,
                Id = "root",
                Topic = "-Root-"
            },
            new()
            {
                Id = "sub1",
                Topic = "sub1-right",
                ParentId = "root"
            },
            new()
            {
                Id = "sub2",
                Topic = "sub2-left",
                ParentId = "root",
                Direction = MindMapNodeDirection.Left
            }
        }
    };

    private readonly MindMapArrayData _arrayData2 = new()
    {
        Nodes = new List<MindMapArrayNode>()
    };

    protected override Task OnInitializedAsync()
    {
        var root = new MindMapArrayNode
        {
            IsRoot = true,
            Id = "root",
            Topic = "-Root-"
        };
        _arrayData2.Nodes.Add(root);

        var sub1 = new MindMapArrayNode
        {
            Id = "sub1",
            Topic = "sub1-right",
            Parent = root
        };

        var sub2 = new MindMapArrayNode
        {
            Id = "sub2",
            Topic = "sub2-left",
            Parent = root,
            Direction = MindMapNodeDirection.Left
        };
        _arrayData2.Nodes.Add(sub1);
        _arrayData2.Nodes.Add(sub2);

        return base.OnInitializedAsync();
    }

    private Task OnResize(ValueEventArgs<string> args)
    {
        Console.WriteLine($"OnResize for {args.Value}");

        return Task.CompletedTask;
    }

    private async Task OnShowTree(ValueEventArgs<string> args)
    {
        Console.WriteLine($"OnShowTree for {args.Value}");
        if (_myTreeNodeContainer.Nodes is not null)
        {
            foreach (var node in _myTreeNodeContainer.Nodes)
            {
                Console.WriteLine($"Node : {node.Id} - {node.Topic}");
            }
        }

        var found = await _myTreeNodeContainer.GetNode("root");
        Console.WriteLine("found = " + found.Id);
        var selected = new List<MindMapTreeNode>
        {
            _treeData.RootNode,
            _treeData.RootNode.Children.Last()
        };
        await _myTreeNodeContainer.SelectNodes(selected);
        _selectedNodeIds = selected.Select(n => n.Id).ToArray();

        await _myTreeNodeContainer.Expand();
    }

    private void OnSelectTreeNode(MindMapSingleSelectEventArgs<MindMapTreeNode> args)
    {
        _selectedNodeId = args.Node != null ? args.Node.GetType().Name + " - " + args.Node.Id + " and selected = " + args.Selected : "/none/";
    }

    private void OnMultiSelectNodes(MindMapMultiSelectEventArgs<MindMapTreeNode> args)
    {
        _selectedNodeId = args.Node != null ? args.Node.GetType().Name + " - " + args.Node.Id : "/none/";
        _selectedNodeIds = args.Nodes.Select(n => n.Id).ToArray();
    }

    private async Task AddTreeNode()
    {
        var newTreeNode = new MindMapTreeNode
        {
            Id = "newTreeId",
            Topic = "new Tree node"
        };

        await _myTreeNodeContainer.SetEditable(true);
        await _myTreeNodeContainer.AddNode(_treeData.RootNode, newTreeNode);
        await _myTreeNodeContainer.SetEditable(false);
    }

    private async Task UpdateRootNode()
    {
        await _myTreeNodeContainer.SetEditable(true);
        await _myTreeNodeContainer.UpdateNodeTopic(_treeData.RootNode, "Updated Root");
        await _myTreeNodeContainer.SetEditable(false);
    }

    private async Task RemoveTreeNode()
    {
        var node = new MindMapTreeNode
        {
            Id = "newTreeId"
        };

        await _myTreeNodeContainer.SetEditable(true);
        await _myTreeNodeContainer.RemoveNode(node);
        await _myTreeNodeContainer.SetEditable(false);
    }

    private async Task SelectTreeNode()
    {
        await _myTreeNodeContainer.SelectNode(_treeData.RootNode);
    }

    private async Task AddArrayNode()
    {
        var newArrayNode = new MindMapArrayNode
        {
            Id = "newArrayId",
            Topic = "new Array node",
            Data = new Dictionary<string, string> { { "background-color", "darkred" } }
        };

        await _myArrayNodeContainer.SetEditable(true);
        await _myArrayNodeContainer.AddNode(_arrayData1.Nodes.First(), newArrayNode);
        await _myArrayNodeContainer.SetEditable(false);
    }

    private async Task UpdateArrayRootNode()
    {
        var node = await _myArrayNodeContainer.GetNode("root");

        await _myArrayNodeContainer.SetEditable(true);
        await _myArrayNodeContainer.UpdateNodeTopic(node, "Updated array Root");
        await _myArrayNodeContainer.SetEditable(false);
    }

    private async Task ClearSelect()
    {
        await _myTreeNodeContainer.ClearSelect();
        _selectedNodeIds = _myTreeNodeContainer.SelectedNodes.Select(n => n.Id).ToArray();
    }

    private async Task MindMapClickable()
    {
        await _myTreeNodeContainer.SetReadOnly(false);
    }

    private async Task MindMapNotClickable()
    {
        await _myTreeNodeContainer.SetReadOnly(true);
    }

    private async Task CollapseTree()
    {
        await _myTreeNodeContainer.Collapse();
    }
}