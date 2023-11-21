using System;
using System.Collections;
using _Game._Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;


public class Connector : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }
    [Button]
    public void SendNumber()
    {
        StartCoroutine(Sender());
    }

    [Button]
    public void SendCode( string code)
    {
        StartCoroutine(SendCodeAwait(CookieController.OnGetCookieClick(Config.KEY_REF), code));
    }

    public void SendRef(string referal)
    {
        StartCoroutine(SendRefAwait(referal));
    }

    public void GoLink(string referal)
    {
        StartCoroutine(SendRefAwait(referal));
    }
    public void SendLanguage(string language)
    {
        CookieController.OnSetCookieClick(Config.KEY_LANGUAGE, language);
        StartCoroutine(SendLanguageAwait(language));
    }

    public void SendUserData(UserData userData)
    {
        StartCoroutine(SendLanguageAwait(userData));
    }

    [Button]
    public void CheckCookie()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(null, Config.URL_CHECK_CHECKCOOKIE, null);
    }

    IEnumerator Sender()
    {
        WWWForm Data = new WWWForm();

        Data.AddField("number", "79260447778");
        UnityWebRequest Query = UnityWebRequest.Post(Config.URL_SEND_MAIN, Data);
        Query.SetRequestHeader("Access-Control-Allow-Origin","*");
        Query.SetRequestHeader("Access-Control-Allow-Methods","*");
        Query.SetRequestHeader("Access-Control-Allow-Headers","Origin, X-Requested-With, Content-Type, Accept, Authorization");

        yield return Query.SendWebRequest();

        if (Query.isHttpError)
        {
            Debug.LogError("Server does not respond : " + Query.error);
        }
        else
        {
            if (Query.downloadHandler.text == "good") // что нам должен ответить сервер на наши данные
            {
                Debug.LogError("Server responded correctly");
            }
            else
            {
                Debug.LogError("Server responded : " + Query.downloadHandler.text);
            }
        }
    }

    IEnumerator SendCodeAwait(string number, string code)
    {
        WWWForm Data = new WWWForm();
        Data.AddField("number", number);
        Data.AddField("code", code);
        UnityWebRequest Query = UnityWebRequest.Post(Config.URL_CHECK_CHECKCODE, Data);
        Query.SetRequestHeader("Access-Control-Allow-Origin","*");
        Query.SetRequestHeader("Access-Control-Allow-Methods","*");
        Query.SetRequestHeader("Access-Control-Allow-Headers","Origin, X-Requested-With, Content-Type, Accept, Authorization");

        yield return Query.SendWebRequest();

        if (Query.isHttpError)
        {
            Debug.LogError("Server does not respond : " + Query.error);
        }
        else
        {
            if (Query.downloadHandler.text == "good") // что нам должен ответить сервер на наши данные
            {
                Debug.LogError("Server responded correctly");
            }
            else
            {
                Debug.LogError("Server responded : " + Query.downloadHandler.text);
            }
        }
    }

    IEnumerator SendRefAwait(string referal)
    {
        WWWForm Data = new WWWForm();

        Data.AddField("ref", referal);
        UnityWebRequest Query = UnityWebRequest.Post(Config.URL_REF_SAVE, Data);
        Query.SetRequestHeader("Access-Control-Allow-Origin","*");
        Query.SetRequestHeader("Access-Control-Allow-Methods","*");
        Query.SetRequestHeader("Access-Control-Allow-Headers","Origin, X-Requested-With, Content-Type, Accept, Authorization");

        yield return Query.SendWebRequest();

        if (Query.isHttpError)
        {
            Debug.LogError("Server does not respond : " + Query.error);
        }
        else
        {
            (RegisterSingleton._instance.GetSingleton(typeof(User)) as User).UserData.selfReferal = Query.downloadHandler.text;
            CookieController.OnSetCookieClick(Config.KEY_SELF_REF,
                (RegisterSingleton._instance.GetSingleton(typeof(User)) as User)?.UserData.selfReferal);
        }
    }

    IEnumerator SendLanguageAwait(string language)
    {
        WWWForm Data = new WWWForm();

        Data.AddField("language", language);
        Data.AddField("selfRef", CookieController.OnGetCookieClick(Config.KEY_SELF_REF));
        UnityWebRequest Query = UnityWebRequest.Post(Config.URL_SAVE_LANGUAGE, Data);
        Query.SetRequestHeader("Access-Control-Allow-Origin","*");
        Query.SetRequestHeader("Access-Control-Allow-Methods","*");
        Query.SetRequestHeader("Access-Control-Allow-Headers","Origin, X-Requested-With, Content-Type, Accept, Authorization");

        yield return Query.SendWebRequest();

        if (Query.isHttpError)
        {
            Debug.LogError("Server does not respond : " + Query.error);
        }
    }

    IEnumerator SendLanguageAwait(UserData userData)
    {
        WWWForm Data = new WWWForm();

        Data.AddField("userData", userData.GetData());
        UnityWebRequest Query = UnityWebRequest.Post(Config.URL_SAVE_LANGUAGE, Data);
        Query.SetRequestHeader("Access-Control-Allow-Origin","*");
        Query.SetRequestHeader("Access-Control-Allow-Methods","*");
        Query.SetRequestHeader("Access-Control-Allow-Headers","Origin, X-Requested-With, Content-Type, Accept, Authorization");

        yield return Query.SendWebRequest();

        if (Query.isHttpError)
        {
            Debug.LogError("Server does not respond : " + Query.error);
        }
    }

   
    #region newConnect

    public void SendRequest(string data, string link, Action<string> callback)
    {
        StartCoroutine(SendRequestAwait(data, link, callback));
    }

    private IEnumerator SendRequestAwait(string data, string link, Action<string> callback)
    {
        WWWForm Data = new WWWForm();
        Data.AddField("data", data);
        UnityWebRequest Query = UnityWebRequest.Post(link, Data);
        Query.SetRequestHeader("Access-Control-Allow-Origin","*");
        Query.SetRequestHeader("Access-Control-Allow-Methods","*");
        Query.SetRequestHeader("Access-Control-Allow-Headers","Origin, X-Requested-With, Content-Type, Accept, Authorization");
        
        yield return Query.SendWebRequest();

        if (Query.isHttpError)
        {
            Debug.LogError("Server does not respond : " + Query.error);
        }
        else
        {
            callback?.Invoke(Query.downloadHandler.text);
        }
    }

    #endregion
}