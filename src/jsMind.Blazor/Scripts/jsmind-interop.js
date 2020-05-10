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

        const mousedown_handle = function (e) {
            e.preventDefault();

            const element = e.target || event.srcElement;
            const id = this.view.get_binded_nodeid(element);
            if (id && element.tagName.toLowerCase() === "jmnode") {
                const node = mm.get_node(id);

                // If already selected: remove from selected list and remove class
                if (mm.selectedNodes.includes(id)) {
                    node._data.view.element.className = node._data.view.element.className.replace(/\s*selected\b/g, "");

                    mm.selectedNodes.pop(id);
                } else {
                    node._data.view.element.className += " selected";

                    mm.selectedNodes.push(id);
                }

                //instances[containerId].select_clear();

                //mm.selectedNodes.forEach(selectedId => {
                //    const selectedNode = instances[containerId].get_node(selectedId);
                //    selectedNode._data.view.element.className += " selected";
                //});
            }
        }

        mm.view.add_event(mm, "mousedown", mousedown_handle);
    }

    mm.add_event_listener(eventHandler);

    // Keep a reference to the javascript MindMap object
    instances[containerId] = mm;
}

MindMap.destroy = function (containerId) {
    instances[containerId] = null;
    delete instances[containerId];
}

MindMap.addNode = function (containerId, id, parentId, topic, data) {
    instances[containerId].add_node(id, parentId, topic, data);
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

        //instances[containerId].select_clear();

        nodes.forEach(node => {
            const foundNode = instances[containerId].get_node(node.id);
            foundNode._data.view.element.className += " selected";
            //instances[containerId].clear_node_custom_style(foundNode);

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