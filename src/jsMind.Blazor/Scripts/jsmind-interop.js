var MindMap = MindMap || {};

const instances = {};

MindMap.setDocumentTitle = function (title) {
    document.title = title;
};

MindMap.show = function (dotnetReference, containerId, mindMapOptions, mindMapData) {
    const mind = {
        "meta": {
            "name": containerId,
            "author": "Stef Heyenrath",
            "version": "1.0"
        },
        "format": mindMapData.format,
        "data": mindMapData.data
    };

    const options = {
        container: containerId,
        editable: mindMapOptions.editable,
        theme: mindMapOptions.theme
    }

    const mm = window.jsMind.show(options, mind);;
    mm["multiSelect"] = mindMapOptions.multiSelect;

    // Call a callback to indicate that the MindMap is shown
    dotnetReference.invokeMethodAsync("OnShowCallback", { evt: "done", node: "", data: [] });

    const eventHandler = function (type, data) {
        // show: 1, resize: 2, edit: 3, select: 4
        switch (type) {
            case 1:
                dotnetReference.invokeMethodAsync("OnShowCallback", data);
                break;

            case 2:
                dotnetReference.invokeMethodAsync("OnResizeCallback", data);
                break;

            case 3:
                dotnetReference.invokeMethodAsync("OnEditCallback", data);
                break;

            case 4:
                dotnetReference.invokeMethodAsync("OnSelectCallback", data);
                break;
        }
    }

    // Custom
    if (mindMapOptions.multiSelect) {
        mm.selectedNodes = [];

        const mousedownHandleMultiSelect = function (e) {
            e.preventDefault();

            const element = e.target || event.srcElement;
            const id = this.view.get_binded_nodeid(element);
            if (id && element.tagName.toLowerCase() === "jmnode") {
                const node = mm.get_node(id);

                var selectedNodeId;
                
                // Check if already selected
                const index = mm.selectedNodes.indexOf(id);
                if (index > -1) {
                    // Remove from list
                    mm.selectedNodes.splice(index, 1);

                    // Remove the 'selected' class
                    updateSelectedClass(node, false);

                    // Set selectedId to null
                    selectedNodeId = null;
                } else {
                    // Add to list
                    mm.selectedNodes.push(id);

                    // Set selectedId to this node
                    selectedNodeId = id;
                }

                mm.selectedNodes.forEach(selectedId => {
                    const selectedNode = mm.get_node(selectedId);
                    updateSelectedClass(selectedNode, true);
                });

                dotnetReference.invokeMethodAsync("OnMultiSelectCallback", { id: selectedNodeId, ids: mm.selectedNodes });
            }
        }

        mm.view.add_event(mm, "mousedown", mousedownHandleMultiSelect);
    }

    mm.add_event_listener(eventHandler);

    // Keep a reference to the javascript MindMap object
    instances[containerId] = mm;
}

MindMap.destroy = function (containerId) {
    instances[containerId] = null;
    delete instances[containerId];
}

MindMap.addNode = function (containerId, parentId, id, topic, data) {
    instances[containerId].add_node(parentId, id, topic, data);
}

MindMap.removeNode = function (containerId, id) {
    instances[containerId].remove_node(id);
}

MindMap.expandNode = function (containerId, id) {
    instances[containerId].expand_node(id);
}

MindMap.collapseNode = function (containerId, id) {
    instances[containerId].collapse_node(id);
}

MindMap.expand = function (containerId) {
    instances[containerId].expand_all();
}

MindMap.expandToDepth = function (containerId, depth) {
    instances[containerId].expand_to_depth(depth);
}

MindMap.collapse = function (containerId) {
    instances[containerId].collapse_all();
}

MindMap.selectNode = function (containerId, id) {
    instances[containerId].select_node(id);
}

MindMap.selectNodes = function (containerId, nodes) {
    if (instances[containerId].multiSelect) {
        instances[containerId].selectedNodes = [];

        nodes.forEach(node => {
            const foundNode = instances[containerId].get_node(node.id);
            updateSelectedClass(foundNode, true);

            instances[containerId].selectedNodes.push(node.id);
        });
    }
}

MindMap.getNode = function (containerId, id) {
    return mapNode(instances[containerId].get_node(id));
}

MindMap.clearSelect = function (containerId) {
    instances[containerId].select_clear();
}

MindMap.setTheme = function (containerId, theme) {
    instances[containerId].set_theme(theme);
}

MindMap.disableEdit = function (containerId) {
    instances[containerId].disable_edit();
}

MindMap.enableEdit = function (containerId) {
    instances[containerId].enable_edit();
}

MindMap.isEditable = function (containerId) {
    return instances[containerId].get_editable();
}

updateSelectedClass = function(node, set) {
    if (set && !(/\s*selected\b/i).test(node._data.view.element.className)) {
        node._data.view.element.className += " selected";
    }
    else if (!set) {
        node._data.view.element.className = node._data.view.element.className.replace(/\s*selected\b/ig, "");
    }
}

mapNode = function (node) {
    // Skip children property (TypeError: Converting circular structure to JSON)
    return {
        id: node.id,
        topic: node.topic,
        expanded: node.expanded,
        direction: node.direction,
        data: node.data,
        parentId: node.parentId
    };
}