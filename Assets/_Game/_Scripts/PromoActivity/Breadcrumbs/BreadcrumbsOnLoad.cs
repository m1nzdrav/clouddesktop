using System;
using System.Collections.Generic;
using _Game._Scripts;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreadcrumbsOnLoad : MonoBehaviour, ISingleton
{
    [SerializeField] private List<BreadStock> _breadStocks;


    public string NameComponent
    {
        get => name;
    }

    public void CheckRegister()
    {
/*#if UNITY_WEBGL && !UNITY_EDITOR
        string breadCrumbs;
        
        foreach (BreadStock VARIABLE in _breadStocks)
        {
            breadCrumbs = CookieController.OnGetCookieClick(VARIABLE.nameScene);
            VARIABLE.value = breadCrumbs.IsNullOrWhitespace() ? 0 : int.Parse(breadCrumbs);
        }
#else

        foreach (BreadStock VARIABLE in _breadStocks)
        {
            VARIABLE.value = PlayerPrefs.GetInt(VARIABLE.nameScene, 0);
        }
#endif*/
    }

    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);

        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    public void SetBreadValue(int value)
    {
        _breadStocks.Find(x => x.nameScene == SceneManager.GetActiveScene().name).value = value;
        SaveValue(SceneManager.GetActiveScene().name, value);
    }

    public void SetTargetBreadValue(string target, int value)
    {
        _breadStocks.Find(x => x.nameScene == target).value = value;
        SaveValue(target, value);
    }

    public void SaveValue(string target, int value)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        CookieController.OnSetCookieClick(target, value.ToString());
#endif
#if !UNITY_WEBGL || UNITY_EDITOR
        PlayerPrefs.SetInt(target, value);
#endif
    }

    public int GetBreadValue(string nameScene)
    {
        return _breadStocks.Find(x => x.nameScene == nameScene).value;
    }
}

[Serializable]
public class BreadStock
{
    public string nameScene;
    public int value = 0;
}