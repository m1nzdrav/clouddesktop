using System;
using System.Collections;
using _Game._Scripts;
using Doozy.Engine.Events;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeLanguageLoginScene : MonoBehaviour
{
    [SerializeField] private PromoOneInstantiate _instantiate;
    [SerializeField] private PromoOneInstantiate _instantiateForPromo;
    [SerializeField] private ContentSetting _firstContentSetting;
    [SerializeField] private ContentSetting _contentSetting;
    [SerializeField] private ContentSetting _testContentSetting;
    [SerializeField] private ContentSetting _folderContentSetting;
    [SerializeField] private ContentSetting _subFollowContentSetting;
    [SerializeField] private ContentSetting _balanceContentSetting;
    [SerializeField] private ContentSetting _parentContentSetting;
    [SerializeField] private ContentSetting _invitationContentSetting;
    

    [SerializeField] private FirstOpen firstOpen;
    [SerializeField] private Text loginTextPlaceholder;
    [SerializeField] private bool firstChange = false;
    public static string currentLanguage;
    public static bool isFirst = false;
    [SerializeField] private string currentJsonFileName;
    [SerializeField] private bool firstPromo = true;
    private UnityEvent<String> _unityEvent;
    [SerializeField] private GameEvent _eventChangeLanguage;

    private bool _isTest;
    private bool _isArabic;
    [SerializeField] private float firstWaitTimer;

    public string CurrentLanguage => currentLanguage;


    [Button]
    public void Test()
    {
        Debug.LogError(isFirst);
        isFirst = true;
    }

    public void FirstChange(bool _first)
    {
        isFirst = _first;
    }

    public void RegisterEvent(Action<string> action)
    {
        if (_unityEvent == null) _unityEvent = new StringEvent();

        _unityEvent.AddListener(action.Invoke);
    }


    #region Language

//     public void Russian()
//     {
//         _isArabic = false;
//         currentLanguage = "Russian";
// #if UNITY_WEBGL && !UNITY_EDITOR
//                 RegisterSingleton._instance.User.UpdateUserData(language: currentLanguage);
// #endif
//
//         // RegisterSingleton._instance.Connecter.SendLanguage(currentLanguage);
//         // _unityEvent.Invoke(currentLanguage);
//         firstOpen?.Enter();
//
//         if (isFirst)
//         {
//             RegisterSingleton._instance.BreadcrumbsOnLoad.SetTargetBreadValue("LoginSceneSlava", 0);
//             isFirst = false;
//             RegisterSingleton._instance.InitLoader.LoadScene("LoginSceneSlava");
//         }
//         else
//             isFirst = true;
//
//         // ClearPrewAndSetNew("Russian");
//         // PromoLanguage();
//     }

    public void Arabic()
    {
        // _isTest = true;
        _isArabic = true;
        ClearPrewAndSetNew("Arabic");

        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }

    public void Chinese()
    {
        // _isTest = true;
        _isArabic = false;
        ClearPrewAndSetNew("Chinese");


        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }

    public void English()
    {
        // _isTest = true;
        _isArabic = false;
        ClearPrewAndSetNew("English");


        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }


    public void French()
    {
        // _isTest = true;
        _isArabic = false;

        ClearPrewAndSetNew("French");

        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }


    public void German()
    {
        // _isTest = true;
        _isArabic = false;
        ClearPrewAndSetNew("German");
        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }


    public void Italian()
    {
        // _isTest = true;
        _isArabic = false;
        ClearPrewAndSetNew("Italian");
        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }


    public void Portuguese()
    {
        // _isTest = true;
        _isArabic = false;
        ClearPrewAndSetNew("Portuguese");
        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }


    public void Turkish()
    {
        // _isTest = true;
        _isArabic = false;
        ClearPrewAndSetNew("Turkish");
        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }

    public void Spanish()
    {
        // _isTest = true;
        _isArabic = false;
        ClearPrewAndSetNew("Spanish");
        if (isFirst)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetTargetBreadValue("LoginSceneSlava", 0);
            isFirst = false;
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("LoginSceneSlava");
        }
        else
            isFirst = true;
    }

    #endregion

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        string tempLanguage = CookieController.OnGetCookieClick(Config.KEY_LANGUAGE);
        currentLanguage =
            tempLanguage.IsNullOrWhitespace() ? Config.DEFAULT_LANGUAGE : tempLanguage;
#endif
#if !UNITY_WEBGL && UNITY_EDITOR
        currentLanguage = PlayerPrefs.GetString(Config.KEY_LANGUAGE, Config.DEFAULT_LANGUAGE);
#endif
        // currentLanguage = "Russian";
    }

    public void SetLanguageFromDropdown(string language)
    {
        // _isTest = true;
        _isArabic = false;
        currentLanguage = language;

// #if UNITY_WEBGL && !UNITY_EDITOR
//
//         (RegisterSingleton._instance.GetSingleton(typeof(User)) as User)?.UpdateUserData(language: currentLanguage);
// #endif

#if UNITY_WEBGL && !UNITY_EDITOR
        CookieController.OnSetCookieClick(Config.KEY_LANGUAGE, language);
#endif
#if !UNITY_WEBGL && UNITY_EDITOR
        PlayerPrefs.SetString(Config.KEY_LANGUAGE,language);
#endif
        
        firstOpen?.Enter();
        ClearPrewAndSetNew(language);
    }
   
    IEnumerator FirstActivate()
    {
        firstChange = false;
        yield return new WaitForSeconds(firstWaitTimer);

        ClearPrewAndSetNew(currentLanguage);
    }

    public void SpawnFirst()
    {
        // currentLanguage = "Russian";
        // _unityEvent.Invoke(currentLanguage);
        currentJsonFileName = "RealityScene_Reality_" + currentLanguage;
        _contentSetting.Inst(_isArabic, currentJsonFileName);
    }

    private void ClearPrewAndSetNew(string language)
    {
        currentLanguage = language;
#if UNITY_WEBGL && !UNITY_EDITOR
                (RegisterSingleton._instance.GetSingleton(typeof(User)) as User)?.UpdateUserData(language: language);
#endif
        _unityEvent?.Invoke(currentLanguage);
        firstOpen?.Enter();

        if (isFirst)
        {
            // RegisterSingleton._instance.BreadcrumbsOnLoad.SetTargetBreadValue("LoginSceneSlava", 0);
            // isFirst = false;
            Debug.LogError(SceneManager.GetActiveScene().name);
            (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
            isFirst = true;

       
        // _contentSetting?.Inst(_isArabic, currentJsonFileName);
    }



    public void RevertSub()
    {
        _contentSetting.ClearSubs();
        firstChange = true;
    }
//todo убрать, тк json не используется

    [Button]
    public void RabbitSub()
    {
        _instantiate.StopAndClearAll();
        currentJsonFileName = "IntroJson2_" + currentLanguage;
        _isTest = true;
        if (_isTest)
        {
            _isTest = false;
            _contentSetting.Inst(_isArabic,
                currentJsonFileName);
        }
        else
        {
            _instantiate.JsonString = Resources.Load<TextAsset>(currentJsonFileName).text;
        }
    }

    [Button]
    public void StartSub()
    {
        _instantiate.StopAndClearAll();
        currentJsonFileName = "RealityScene_YouAreStarting_" + currentLanguage;
        _isTest = true;
        if (_isTest)
        {
            _isTest = false;
            _contentSetting.Inst(_isArabic,
                currentJsonFileName);
        }
        else
        {
            _instantiate.JsonString = Resources.Load<TextAsset>(currentJsonFileName).text;
        }
    }

    [Button]
    public void ObjectControlSub()
    {
        _instantiate.StopAndClearAll();
        currentJsonFileName = "IntroJson4_" + currentLanguage;
        _isTest = true;
        if (_isTest)
        {
            _isTest = false;
            _contentSetting.Inst(_isArabic, currentJsonFileName);
        }
        else
        {
            _instantiate.JsonString = Resources.Load<TextAsset>(currentJsonFileName).text;
        }
    }

    [Button]
    public void PromoLanguage()
    {
        _isTest = true;
        // _unityEvent.Invoke(currentLanguage);
        _testContentSetting.Inst(_isArabic, "RealityScene_Reality_" + currentLanguage);
        firstPromo = false;
    }

    [Button]
    public void BalancePromo()
    {
        _isTest = true;
        _balanceContentSetting.Inst(_isArabic, "IntroJsonBalance_" + currentLanguage);
        firstPromo = false;
    }

    [Button]
    public void ParentPromo()
    {
        _isTest = true;
        _parentContentSetting.Inst(_isArabic, "IntroJson4_" + currentLanguage);
    }


    public void ReversPromo()
    {
        _testContentSetting.ClearSubs();
    }


    [Button]
    public void SubFolder()
    {
        _isTest = true;
        if (!firstPromo) return;

        _folderContentSetting.Inst(_isArabic, "RealityScene_YouAreStarting_" + currentLanguage);
        firstPromo = false;
    }

    [Button]
    public void SubFollow()
    {
        _isTest = true;

        _subFollowContentSetting.Inst(_isArabic, "MainScene_Follow_" + currentLanguage);
    }

    public void ReversSubFolder()
    {
        _folderContentSetting.ClearSubs();
    }

    [Button]
    public void InvitationPromo()
    {
        _isTest = true;
        
        _invitationContentSetting.Inst(_isArabic,Config.SCENE_INVITATION + currentLanguage);
        firstPromo = false;
    }
    public void TokensalePromo()
    {
        _isTest = true;
        
        _invitationContentSetting.Inst(_isArabic,Config.SCENE_TOKENSALE + currentLanguage);
        firstPromo = false;
    }

    [Button]
    public void FirstPromo(bool fast = false)
    {
        _isTest = true;
        // if (!firstPromo) return;
        // _unityEvent.Invoke(currentLanguage);
        if (!fast)
        {
            _firstContentSetting.Inst(_isArabic, "MainScene_3DPlatfrom_" + currentLanguage);
        }
        else
        {
            _firstContentSetting.FastInst(_isArabic, "MainScene_3DPlatfrom_" + currentLanguage);
        }

        // firstPromo = false;
    }
}