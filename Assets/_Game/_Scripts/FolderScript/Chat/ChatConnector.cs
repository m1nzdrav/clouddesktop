using System;
using System.Collections;
using System.Collections.Generic;
using _Game._Scripts;
using UnityEngine;
using UnityEngine.Networking;


public class ChatConnector : MonoBehaviour
{
    public void SendRequest(string data, string link, Action<string> callback)
    {
        StartCoroutine(SendRequestAwait(data, link, callback));
    }

    private IEnumerator SendRequestAwait(string data, string link, Action<string> callback)
    {
        WWWForm Data = new WWWForm();
        Data.AddField("data", data);
        // Data.AddField();
        
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

    public void SendNumber(string answer, Action<string> callback)
    {
        NumberReferal refAnswer = new NumberReferal
            { number = answer, referal = CookieController.OnGetCookieClick(Config.KEY_REF) };
        
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(refAnswer), Config.URL_REG_USER, callback);
    }

    public void SendCode(string answer, Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            { code = answer, referal = CookieController.OnGetCookieClick(Config.KEY_REF) };

        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_CHECK_CHECKCODE,
            callback);
    }

    public void SendRef(string answer, Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            { code = answer, referal = CookieController.OnGetCookieClick(Config.KEY_REF) };

        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_SEND_REF,
            callback);
    }
    public void SendCountry(string answer, Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            { code = answer, referal = CookieController.OnGetCookieClick(Config.KEY_REF) };

        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_SEND_COUNTRY,
            callback);
    }
    public void SendCountryCode(string answer, Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            { code = answer, referal = CookieController.OnGetCookieClick(Config.KEY_REF) };

        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_SEND_COUNTRY_CODE,
            callback);
    }

    public void SendDestination(ContainerString answer, Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            { code = JsonUtility.ToJson(answer), referal = CookieController.OnGetCookieClick(Config.KEY_REF) };
        

        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_SEND_DESTINATION,
            callback);
    }

    public void CheckLocker(string answer, Action<string> callback)
    {
        DBLocker dbLocker = new DBLocker()
            { keyWord = answer, locker = (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.LocalLocker.CurrentLocker.locker };
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(dbLocker), Config.URL_CHECK_LOCKER,
            callback);
    }
    
    public void SendLink(string answer, Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            { code = answer, referal = CookieController.OnGetCookieClick(Config.KEY_REF) };

        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_SEND_LINK,
            callback);
    }

    public void SetEmail(string answer, Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            { code = answer, referal = CookieController.OnGetCookieClick(Config.KEY_REF) };

        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_SET_EMAIL,
            callback);
    }

    public void SendEmail( Action<string> callback)
    {
        CodeReferal codeAnswer = new CodeReferal
            {  referal = CookieController.OnGetCookieClick(Config.KEY_REF) };
    
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(codeAnswer), Config.URL_SEND_EMAIL,
            callback);
    }

    public void GetLocker(Action<string> callback)
    {
        DBLocker dbLocker = new DBLocker()
            { locker = (RegisterSingleton._instance.GetSingleton(typeof(LockerScene)) as LockerScene)?.LocalLocker.CurrentLocker.locker };
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(JsonUtility.ToJson(dbLocker), Config.URL_GET_LOCKER,
            callback);
    }
}