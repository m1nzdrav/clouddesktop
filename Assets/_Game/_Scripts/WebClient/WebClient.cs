using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebClient : MonoBehaviour
{
    private static string s_serverPath = "http://195.24.65.200:32760";
    private static string s_jwt = "none";

    public static string Jwt
    {
        get => s_jwt;
    }

    private static string CheckJwt(string jwt)
    {
        if (jwt == "not assigned")
            return s_jwt;
        return jwt;
    }

    public static IEnumerator GetWeatherForecast()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get($"{s_serverPath}/WeatherForecast"))
        {
            Debug.Log("sendRequest");
            yield return unityWebRequest.SendWebRequest();
            Debug.Log(unityWebRequest.downloadHandler.text);
            List<string> forecast = JsonUtility.FromJson<List<string>>(unityWebRequest.downloadHandler.text);
        }
    }

    public static IEnumerator CreateNewUser(DbUser user, Action callback)
    {
        yield return WebUtilities.CustomRequest(
            request => { Debug.Log(request.responseCode); Debug.Log(request.downloadHandler.text); callback?.Invoke(); },
            url: $"{s_serverPath}/Users",
            bodyType: "application/json",
            body: JsonUtility.ToJson(user));
        /*var bodyData = JsonUtility.ToJson(user);
        using (var request = new UnityWebRequest())
        {
            request.url = $"{s_serverPath}/Users";
            request.method = "POST";
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(bodyData) ? null : Encoding.UTF8.GetBytes(bodyData));
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 60;
            yield return request.SendWebRequest();
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
            callback?.Invoke(Convert.ToInt32(request.downloadHandler.text));
        }*/
    }

    public static IEnumerator AuthoriseUserByFlashCall(Action<string> requestCallback, Action<string> passCallback, string number)
    {
        using (var requestRequest = UnityWebRequest.Get($"{s_serverPath}/RequestFlashCall?number={number}"))
        {
            yield return requestRequest.SendWebRequest();
            Debug.Log(requestRequest.downloadHandler.text);
            if (!requestRequest.downloadHandler.text.Contains("error"))
            {
                requestCallback.Invoke(requestRequest.downloadHandler.text);
                using (var passRequest = UnityWebRequest.Get($"{s_serverPath}/PassFlashCall?number={number}&code=none"))
                {
                    yield return passRequest.SendWebRequest();
                    Debug.Log(passRequest.downloadHandler.text);
                    passCallback.Invoke(passRequest.downloadHandler.text);
                }
            }
        }
    }

    public static IEnumerator RequestFlashCall(Action<string> callback, string number)
    {
        using (var request = UnityWebRequest.Get($"{s_serverPath}/RequestFlashCall?number={number}"))
        {
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            callback.Invoke(request.downloadHandler.text);
        }
    }

    public static IEnumerator PassFlashCall(Action<string> callback, string number, string code = "none")
    {
        using (var passRequest = UnityWebRequest.Get($"{s_serverPath}/PassFlashCall?number={number}&code={code}"))
        {
            yield return passRequest.SendWebRequest();
            Debug.Log(passRequest.downloadHandler.text);
            callback.Invoke(passRequest.downloadHandler.text);
        }
    }

    public static IEnumerator DeleteFlashCall(Action<int> callback, string number)
    {
        using (var request = UnityWebRequest.Delete($"{s_serverPath}/DeleteFlashCall?number={number}"))
        {
            yield return request.SendWebRequest();
            Debug.Log($"Delete flashCall result: {request.responseCode}");
            callback.Invoke(Convert.ToInt32(request.responseCode));
        }
    }

    public static IEnumerator AuthoriseUserByPassword(Action<string> callback, 
        string name = "none", string password = "none", string email = "none", string number = "none")
    {
        using (var request = UnityWebRequest.Get($"{s_serverPath}/Authorisation?username={name}&password={password}&email={email}&number={number}"))
        {
            yield return request.SendWebRequest();
            while (!request.isDone)
            {
                continue;
            }
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
            s_jwt = request.downloadHandler.text;
            callback?.Invoke(s_jwt);
        }
    }

    public static IEnumerator GetUser(Action<DbUser> callback, string jwt = "not assigned")
    {
        jwt = CheckJwt(jwt);
        using (UnityWebRequest request = UnityWebRequest.Get($"{s_serverPath}/Users"))
        {
            request.SetRequestHeader("Authorization", "Bearer " + jwt);
            yield return request.SendWebRequest();
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
        }
    }

    public static IEnumerator UpdateUser(Action<string> callback, DbUser user, string jwt = "not assigned")
    {
        jwt = CheckJwt(jwt);
        string body = JsonUtility.ToJson(user);
        using (var request = new UnityWebRequest())
        {
            request.url = $"{s_serverPath}/Users";
            request.method = "PUT";
            request.downloadHandler = new DownloadHandlerBuffer();
            request.uploadHandler = new UploadHandlerRaw(string.IsNullOrEmpty(body) ? null : Encoding.UTF8.GetBytes(body));
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + jwt);
            request.timeout = 60;
            yield return request.SendWebRequest();
            while (!request.isDone){
                Debug.Log(request.downloadProgress);
            }
            if (request.isNetworkError)
            {
                Debug.Log("networking error");
            }
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
            s_jwt = request.downloadHandler.text;
            callback?.Invoke(s_jwt);
        }
    }

    public static IEnumerator DeleteUser(Action callback, string jwt = "not assigned")
    {
        jwt = CheckJwt(jwt);
        using (UnityWebRequest request = UnityWebRequest.Delete($"{s_serverPath}/Users"))
        {
            request.SetRequestHeader("Authorization", "Bearer " + jwt);
            yield return request.SendWebRequest();
            Debug.Log(request.responseCode);
        }
    }

    public static IEnumerator AuthTest(string jwt)
    {
        using (var request = new UnityWebRequest())
        {

            request.url = $"{s_serverPath}/AuthTest";
            request.method = "GET";
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + jwt);
            request.timeout = 60;
            yield return request.SendWebRequest();
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
        }
    }
}
