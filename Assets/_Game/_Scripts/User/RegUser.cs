using System;
using System.Collections;
using _Game._Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;


public class RegUser : MonoBehaviour, ISingleton
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
    public void SendNumber(string number, Action callback = null)
    {
        StartCoroutine(SendNumberAwait(number, callback));
    }

    IEnumerator SendNumberAwait(string number, Action callback = null)
    {
        WWWForm Data = new WWWForm();

        Data.AddField("number", number);
        Data.AddField("referal", CookieController.OnGetCookieClick(Config.KEY_REF));
        Debug.LogError("tetst");
        UnityWebRequest Query = UnityWebRequest.Post(Config.URL_REG_USER, Data);

        yield return Query.SendWebRequest();

        if (Query.isHttpError)
        {
            Debug.LogError("Server does not respond : " + Query.error);
        }
        else if (Query.downloadHandler.text == "1")
        {
            Debug.LogError("truuuuuuuueeee");
        }

        Debug.LogError(Query.downloadHandler.text);
    }
}