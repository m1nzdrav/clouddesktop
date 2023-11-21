using System;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

public class WebUtilities
{
    public static IEnumerator CustomRequest(Action<UnityWebRequest> callback, string url, string bodyType, string body, string method = "POST")
    {
        using (var request = new UnityWebRequest())
        {
            request.url = url;
            request.method = method;
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(body) ? null : Encoding.UTF8.GetBytes(body));
            request.SetRequestHeader("Accept", bodyType);
            request.SetRequestHeader("Content-Type", bodyType);
            request.timeout = 60;
            yield return request.SendWebRequest();
            callback?.Invoke(request);
        }
    }

    public static IEnumerator PostRequest(Action<UnityWebRequest> callback, string url, string bodyType, string body)
    {
        yield return CustomRequest(callback, url, bodyType, body);
    }

    public static IEnumerator PutRequest(Action<UnityWebRequest> callback, string url, string bodyType, string body)
    {
        yield return CustomRequest(callback, url, bodyType, body, "PUT"); 
    }
}
