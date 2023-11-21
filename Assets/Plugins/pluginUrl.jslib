var pluginUrl = {
    OpenNewTab : function(url)
    {
        url = Pointer_stringify(url);
        window.open(url,'_blank');
    },
    GetOldUrlPath : function()
    {
        window.alert(window.history.state)
        window.alert(window.history.state.prevUrl)
        return window.history.state.prevUrl;
    },
};
mergeInto(LibraryManager.library, pluginUrl);