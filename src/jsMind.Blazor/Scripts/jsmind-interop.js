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

    const mm = window.jsMind.show(options, mind);

    // ReadOnly
    setReadOnly(mm, mindMapOptions.readOnly);

    // MultiSelect
    mm["multiSelect"] = mindMapOptions.multiSelect;

    const eventHandler = async function (type, data) {
        // show: 1, resize: 2, edit: 3, select: 4
        try {
            switch (type) {
                case 1:
                    //dotnetReference.invokeMethodAsync("OnShowCallback", data);
                    break;

                case 2:
                    await dotnetReference.invokeMethodAsync("OnResizeCallback", { evt: "resize", containerId: containerId });
                    break;

                case 3:
                    await dotnetReference.invokeMethodAsync("OnEditCallback", data);
                    break;

                case 4:
                    await dotnetReference.invokeMethodAsync("OnSelectCallback", data);
                    break;
            }
        }
        catch (e) {
            console.warn(e);
        }
    }

    // Custom
    if (mindMapOptions.multiSelect) {
        MindMap.clearSelect(containerId);

        const mousedownHandleMultiSelect = function (e) {
            if (!mm.options.default_event_handle.enable_mousedown_handle) {
                return;
            }

            e.preventDefault();

            const element = e.target || event.srcElement;
            const id = this.view.get_binded_nodeid(element);
            if (id && element.tagName.toLowerCase() === "jmnode") {
                const node = mm.get_node(id);

                let selectedNodeId;

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

    // Call a callback to indicate that the MindMap is completely shown (and the instances correctly updated with the containerId)
    dotnetReference.invokeMethodAsync("OnShowCallback", { evt: "done", containerId: containerId });
}

MindMap.destroy = function (containerId) {
    instances[containerId] = null;
    delete instances[containerId];
}

MindMap.addNode = function (containerId, parentId, id, topic, data) {
    instances[containerId] && instances[containerId].add_node(parentId, id, topic, data);
}

MindMap.updateNode = function (containerId, id, topic) {
    instances[containerId] && instances[containerId].update_node(id, topic);
}

MindMap.removeNode = function (containerId, id) {
    instances[containerId] && instances[containerId].remove_node(id);
}

MindMap.expandNode = function (containerId, id) {
    instances[containerId] && instances[containerId].expand_node(id);
}

MindMap.collapseNode = function (containerId, id) {
    instances[containerId] && instances[containerId].collapse_node(id);
}

MindMap.expand = function (containerId) {
    instances[containerId] && instances[containerId].expand_all();
}

MindMap.expandToDepth = function (containerId, depth) {
    instances[containerId] && instances[containerId].expand_to_depth(depth);
}

MindMap.collapse = function (containerId) {
    instances[containerId] && instances[containerId].collapse_all();
}

MindMap.selectNode = function (containerId, id) {
    instances[containerId] && instances[containerId].select_node(id);
}

MindMap.selectNodes = function (containerId, nodes) {
    if (instances[containerId] && instances[containerId].multiSelect) {
        instances[containerId].selectedNodes = [];

        nodes.forEach(node => {
            const foundNode = instances[containerId].get_node(node.id);
            updateSelectedClass(foundNode, true);

            instances[containerId].selectedNodes.push(node.id);
        });
    }
}

MindMap.getNode = function (containerId, id) {
    return instances[containerId] ? mapNode(instances[containerId].get_node(id)) : undefined;
}

MindMap.clearSelect = function (containerId) {
    const mm = instances[containerId];
    if (mm) {
        if (mm.multiSelect) {
            mm.selectedNodes.forEach(node => {
                updateSelectedClass(mm.get_node(node), false);
            });
            mm.selectedNodes = [];
        } else {
            mm.select_clear();
        }
    }
}

MindMap.setTheme = function (containerId, theme) {
    instances[containerId] && instances[containerId].set_theme(theme);
}

MindMap.disableEdit = function (containerId) {
    instances[containerId] && instances[containerId].disable_edit();
}

MindMap.enableEdit = function (containerId) {
    instances[containerId] && instances[containerId].enable_edit();
}

MindMap.isEditable = function (containerId) {
    return instances[containerId] ? instances[containerId].get_editable() : false;
}

MindMap.setReadOnly = function (containerId, isReadOnly) {
    return instances[containerId] ? setReadOnly(instances[containerId], isReadOnly) : false;
}

updateSelectedClass = function (node, set) {
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

setReadOnly = function (mm, isReadOnly) {
    mm.options.default_event_handle = {
        enable_mousedown_handle: !isReadOnly,
        enable_click_handle: !isReadOnly,
        enable_dblclick_handle: !isReadOnly
    };
}