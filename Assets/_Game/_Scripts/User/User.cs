using System;
using System.Collections;
using _Game._Scripts;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Networking;


public class User : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    [SerializeField] private string testReferal;

    public void CheckRegister()
    {
// #if UNITY_WEBGL && !UNITY_EDITOR
        // GetUserData();
// #endif
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    [SerializeField] private UserData _userData;

    public UserData UserData => _userData;

    public void UpdateUserData(string language = null, string selfReferal = null, string token = null,
        string bread = null)
    {
        _userData.SetData(language, selfReferal, token, bread);
        // SendUserData();
    }

    private void SetUserData(string _userData)
    {
        try
        {
            this._userData = JsonUtility.FromJson<UserData>(_userData);
            
            if (!this._userData.language.IsNullOrWhitespace())
                ChangeLanguageLoginScene.currentLanguage = this._userData.language;
            
            if (!this._userData.bread.IsNullOrWhitespace())
                (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetBreadValue(Int32.Parse(this._userData.bread));

        }
        catch (Exception e)
        {
        }
    }

    private void SendUserData()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(_userData.GetData(), Config.URL_SET_USERDATA, null);
        // StartCoroutine(SendUserDataAwait());
    }

    // IEnumerator SendUserDataAwait()
    // {
    //     WWWForm Data = new WWWForm();
    //
    //     Data.AddField("userData", _userData.GetData());
    //     UnityWebRequest Query = UnityWebRequest.Post(Config.URL_SET_USERDATA, Data);
    //
    //     yield return Query.SendWebRequest();
    //
    //     if (Query.isHttpError)
    //     {
    //         Debug.LogError("Server does not respond : " + Query.error);
    //     }
    // }

    public void GetUserData()
    {
#if UNITY_WEBGL &&!UNITY_EDITOR
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(CookieController.OnGetCookieClick(Config.KEY_REF), Config.URL_GET_USERDATA, SetUserData);
#else
        (RegisterSingleton._instance.GetSingleton(typeof(Connector)) as Connector)?.SendRequest(testReferal, Config.URL_GET_USERDATA, SetUserData);
#endif
    }


//     IEnumerator GetUserDataAwait()
//     {
//         WWWForm Data = new WWWForm();
// #if UNITY_WEBGL &&!UNITY_EDITOR
//         Data.AddField("selfReferal", CookieController.OnGetCookieClick(Config.KEY_REF));
// #else
//         Data.AddField("selfReferer", testReferal);
// #endif
//         UnityWebRequest Query = UnityWebRequest.Post(Config.URL_GET_USERDATA, Data);
//
//         yield return Query.SendWebRequest();
//
//         if (Query.isHttpError)
//         {
//             Debug.LogError("Server does not respond : " + Query.error);
//         }
//         else
//         {
//             SetUserData(Query.downloadHandler.text);
//         }
//     }
}