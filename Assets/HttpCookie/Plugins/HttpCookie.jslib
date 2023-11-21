var LibraryHttpCookie = {

getHttpCookies: function()
{
	var cookies = document.cookie;
	var length = lengthBytesUTF8(cookies) + 1;
	var buffer = _malloc(length);
	stringToUTF8(cookies, buffer, length);
	return buffer;
},

getHttpCookie: function(nameArg)
{
	var name = Pointer_stringify(nameArg);
	var cookie = document.cookie;
	var search = name + "=";
	var setStr = "";
	var offset = 0;
	var end = 0;
	if (cookie.length > 0)
	{
		offset = cookie.indexOf(search);
		if (offset != -1)
		{
			offset += search.length;
			end = cookie.indexOf(";", offset);
			if (end == -1)
			{
				end = cookie.length;
			}
			setStr = unescape(cookie.substring(offset, end));
		}
	}

	var length = lengthBytesUTF8(setStr) + 1;
	var buffer = _malloc(length);
	stringToUTF8(setStr, buffer, length);
	return buffer;
},

setHttpCookie: function(nameArg, valueArg, expiresArg, pathArg, domainArg, secureArg)
{
	var name = Pointer_stringify(nameArg);
	var value = Pointer_stringify(valueArg);
	var expires = Pointer_stringify(expiresArg);
	var path = Pointer_stringify(pathArg);
	var domain = Pointer_stringify(domainArg);
	var secure = Pointer_stringify(secureArg);
	document.cookie = name + "=" + escape(value) +
		((expires) ? "; expires=" + expires : "") +
		((path) ? "; path=" + path : "") +
		((domain) ? "; domain=" + domain : "") +
		((secure) ? "; secure" : "");
},

getURL: function (key) 
{
    var p = window.location.search;
    p = p.match(new RegExp(key + '=([^&=]+)'));
    return p ? p[1] : false;
},

getAllUrlParams: function (key) {

var name = Pointer_stringify(key);

  // извлекаем строку из URL или объекта window
  var queryString = window.location.search.slice(1);

  // объект для хранения параметров


  // если есть строка запроса
  if (queryString) {

    // данные после знака # будут опущены
    queryString = queryString.split('#')[0];

    // разделяем параметры
    var arr = queryString.split('&');

    for (var i=0; i<arr.length; i++) {
      // разделяем параметр на ключ => значение
      var a = arr[i].split('=');

      // обработка данных вида: list[]=thing1&list[]=thing2
      var paramNum = undefined;
      var paramName = a[0].replace(/\[\d*\]/, function(v) {
        paramNum = v.slice(1,-1);
        return '';
      });

      // передача значения параметра ('true' если значение не задано)
      var paramValue = typeof(a[1])==='undefined' ? true : a[1];

      // преобразование регистра
      paramName = paramName.toLowerCase();
      paramValue = paramValue.toLowerCase();

      // если ключ параметра уже задан
      if (paramName == name) {
      var length = lengthBytesUTF8(paramValue) + 1;
      	var buffer = _malloc(length);
      	stringToUTF8(paramValue, buffer, length);
      
      return buffer;
      
    }

  }

 
  }
   return "notFound";
}


};

mergeInto(LibraryManager.library, LibraryHttpCookie);