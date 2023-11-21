var LibraryWebClipboard = {

 PastClipboard: function() 
{
    var search;
    var saveText = function(text) {
      search = text; 
    };
    
    navigator.clipboard.readText().then(saveText);
	
	var length = lengthBytesUTF8(search) + 1;
	var buffer = _malloc(length);
	stringToUTF8(search, buffer, length);
	return buffer;
}};
mergeInto(LibraryManager.library, LibraryWebClipboard);