using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using _Game._Scripts;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine.Events;

public class ApplicationManager : MonoBehaviour, ISingleton
{
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void OpenNewTab(string url);
#endif

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("__Internal")]
    private static extern string GetOldUrlPath();

    [field: SerializeField] private KeyCode buttonQuit = KeyCode.Escape;
    [field: SerializeField] private KeyCode buttonReload = KeyCode.R;
    [field: SerializeField] private KeyCode buttonPause = KeyCode.Space;
    [SerializeField] private bool fullScreen;
    [SerializeField] private UnityEvent _eventPause;
    [SerializeField] private bool first = true;
    [SerializeField] private string startScene;

    public string StartScene => startScene;

    [SerializeField] private Texture2D _textureCursor;


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
        else
        {
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
    }

    private void Start()
    {
        
#if UNITY_WEBGL && !UNITY_EDITOR
        string tempLanguage = CookieController.OnGetCookieClick(Config.KEY_LANGUAGE);
        ChangeLanguageLoginScene.currentLanguage =
            tempLanguage.IsNullOrWhitespace() ? Config.DEFAULT_LANGUAGE : tempLanguage;
#else
        ChangeLanguageLoginScene.currentLanguage = PlayerPrefs.GetString(Config.KEY_LANGUAGE, Config.DEFAULT_LANGUAGE);
#endif
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(buttonQuit))
            Quit();

        if (Input.GetKeyDown(buttonReload))
            ReloadScene();

        if (Input.GetKeyDown(buttonPause))
            _eventPause.Invoke();
    }

    public void Quit()
    {
        Screen.fullScreen = false;
        // Application.Quit();
    }

    IEnumerator Retreat()
    {
        Screen.fullScreen = false;
        yield return new WaitForSeconds(0.1f);
        ShowWindow(GetActiveWindow(), 2);
    }

    public void Retrate()
    {
        StartCoroutine(Retreat());
    }

    public void FullScreen(bool isFullScreen)
    {
        if (first)
        {
            first = false;
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
            Screen.fullScreen = isFullScreen;
        }
    }


    public void ReloadScene()
    {
        ChangeLanguageLoginScene.isFirst = false;
        (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene("StartScene");
    }

    public void GoLink(string link)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            OpenNewTab(link);
#else
        Application.OpenURL(link);
#endif
    }

    public void ReloadScene(float timeToReload)
    {
        StartCoroutine(AwaitRestart(timeToReload));
    }

    IEnumerator AwaitRestart(float timeToRestart)
    {
        yield return new WaitForSeconds(timeToRestart);
        ChangeLanguageLoginScene.isFirst = false;
        (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene(startScene);
    }

    [Button]
    public void OpenURL()
    {
        Application.OpenURL("mailto:slav.kova97@gmail.com");
    }
    // private void OnApplicationFocus(bool hasFocus)
    // {
    //     Cursor.SetCursor(_textureCursor, Vector2.zero, CursorMode.Auto);
    // }
}