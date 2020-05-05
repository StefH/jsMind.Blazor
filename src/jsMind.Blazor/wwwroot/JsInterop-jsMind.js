var MindMap = MindMap || {};

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

    let mindmap = window.jsMind.show(options, mind);

    //mindmap.add_node("sub2", "sub23", "new node", { "background-color": "red" });
    //mindmap.set_node_color('sub21', 'green', '#ccc');
};