var MindMap = MindMap || {};

MindMap.setDocumentTitle = function (title) {
    document.title = title;
};

MindMap.show = function (containerId, mindMapOptions) {
    const mind = {
        "meta": {
            "name": "demo",
            "author": "Stef",
            "version": "1.0"
        },
        "format": "node_array",
        "data": [
            { "id": "root", "isroot": true, "topic": "jsMind" },

            { "id": "sub1", "parentid": "root", "topic": "sub1", "background-color": "#0000ff" },
            { "id": "sub11", "parentid": "sub1", "topic": "sub11" },
            { "id": "sub12", "parentid": "sub1", "topic": "sub12" },
            { "id": "sub13", "parentid": "sub1", "topic": "sub13" },

            { "id": "sub2", "parentid": "root", "topic": "sub2" },
            { "id": "sub21", "parentid": "sub2", "topic": "sub21" },
            { "id": "sub22", "parentid": "sub2", "topic": "sub22", "foreground-color": "#33ff33" },

            { "id": "sub3", "parentid": "root", "topic": "sub3" }
        ]
    };
    const options = {
        container: containerId,
        editable: true,
        theme: 'primary'
    }

    let mindmap = jsMind.show(options, mind);

    mindmap.add_node("sub2", "sub23", "new node", { "background-color": "red" });
    mindmap.set_node_color('sub21', 'green', '#ccc');
};