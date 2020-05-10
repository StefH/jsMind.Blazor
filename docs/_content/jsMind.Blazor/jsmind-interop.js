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
        "data": mindMapData.data,
    };

    const options = {
        container: containerId,
        editable: mindMapOptions.editable,
        theme: mindMapOptions.theme
    }

    // Keep a reference to the javascript MindMap object
    instances[containerId] = window.jsMind.show(options, mind);

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

    instances[containerId].add_event_listener(eventHandler);
};

MindMap.destroy = function (containerId) {
    instances[containerId] = null;
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