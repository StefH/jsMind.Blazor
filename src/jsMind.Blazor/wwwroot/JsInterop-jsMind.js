﻿var MindMap = MindMap || {};

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

    console.log(mindMapData);
    console.log(mind);

    const options = {
        container: containerId,
        editable: mindMapOptions.editable,
        theme: mindMapOptions.theme
    }

    const mindmap = window.jsMind.show(options, mind);

    const eventHandler = async function (type, data) {
        // show: 1, resize: 2, edit: 3, select: 4
        switch (type) {
            case 1:
                await dotnetReference.invokeMethodAsync('OnShowCallback', data);
                break;

            case 2:
                await dotnetReference.invokeMethodAsync('OnResizeCallback', data);
                break;

            case 3:
                await dotnetReference.invokeMethodAsync('OnEditCallback', data);
                break;

            case 4:
                await dotnetReference.invokeMethodAsync('OnSelectCallback', data);
                break;
        }

    }
    mindmap.add_event_listener(eventHandler);

    //mindmap.add_node("sub2", "sub23", "new node", { "background-color": "red" });
    //mindmap.set_node_color('sub21', 'green', '#ccc');
};