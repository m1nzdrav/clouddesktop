using System;
using _Game._Scripts;
using UnityEngine;


public class CookieController : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
// #if UNITY_WEBGL && !UNITY_EDITOR
//         GetRefCookie();
// #endif
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }
// #if UNITY_WEBGL && !UNITY_EDITOR
    // private void GetRefCookie()
    // {
    //     string test = OnGetCookieClick(Config.KEY_REF);
    //     if (!string.IsNullOrEmpty(test) )
    //     {
    //         Debug.LogError("referal already set" + OnGetCookieClick(Config.KEY_REF));
    //         return;
    //     }
    //
    //     string refString = OnGetAllUrlParams(Config.KEY_REF);
    //
    //    
    //     if (refString != null && refString.Length == 8)
    //         OnSetCookieClick(Config.KEY_REF, refString);
    //     else
    //         OnSetCookieClick(Config.KEY_REF, Config.STARTER_REF);
    //
    //     RegisterSingleton._instance.Connector.SendRef(refString);
    // }

// #endif


    public static string OnGetCookieClick(string name)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return HttpCookie.GetCookie(name);
#else
        return Config.STARTER_REF;
#endif
    }
    public static string OnGetCookieInvitation()
    {
// #if UNITY_WEBGL && !UNITY_EDITOR
//       return  OnGetAllUrlParams(Config.KEY_REF);
// #else
        return Config.SCENE_INVITATION;
// #endif
    }

    public static void OnSetCookieClick(string name, string value)
    {
        HttpCookie.SetCookie(name, value);
    }

    public void OnTimeSetCookieClick(string name, string time)
    {
        double seconds;
        if (!double.TryParse(time, out seconds))
            return;

        HttpCookie.SetCookie(
            name,
            time,
            DateTime.Now.AddSeconds(seconds)
        );
    }

    public static string OnGetAllUrlParams(string name)
    {
        return HttpCookie.GetAllUrlParams(name);
    }
}