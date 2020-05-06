var MindMap = MindMap || {};
var BlazorLocalStorage = BlazorLocalStorage || {};

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

    //console.log(mindMapData);
    //console.log(mind);

    const options = {
        container: containerId,
        editable: mindMapOptions.editable,
        theme: mindMapOptions.theme
    }

    const mm = window.jsMind.show(options, mind);

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
    mm.add_event_listener(eventHandler);

    // Keep a reference to the javascript MindMap object
    instances[containerId] = mm;
};

MindMap.dispose = function (containerId) {
    instances[containerId] = null;
}

MindMap.addNode = function (containerId, id, parentId, topic, data) {
    instances[containerId].add_node(id, parentId, topic, data);
}


















BlazorLocalStorage.get = function (key) {
    return key in localStorage ? JSON.parse(localStorage[key]) : null;
}
BlazorLocalStorage.set = function (key, value) {
    localStorage[key] = JSON.stringify(value);
}
BlazorLocalStorage.delete = function (key) {
    delete localStorage[key];
}