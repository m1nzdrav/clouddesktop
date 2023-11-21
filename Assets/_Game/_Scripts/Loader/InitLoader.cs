using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InitLoader : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    [SerializeField] private CanvasLoader _canvasLoader;
    [SerializeField] private UnityEvent actionLoadNewScene;
    [SerializeField] private UnityEvent actionAfterLoadNewScene;

    public CanvasLoader CanvasLoader
    {
        get => _canvasLoader;
        set => _canvasLoader = value;
    }

    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
    }
    
    private AsyncOperation asyncLoad;

    [Button]
    public void LoadScene(String sceneName)
    {
        actionLoadNewScene?.Invoke();
        _canvasLoader.LoadNewScene((() =>
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BackgroundSingleton)) as BackgroundSingleton)?.ActivateBackCamera(() =>
            {
                StartCoroutine(LoadYourAsyncScene(sceneName));
            });
        }));
    }

    private IEnumerator LoadYourAsyncScene(string sceneName)
    {
        asyncLoad = SceneManager.LoadSceneAsync((RegisterSingleton._instance.GetSingleton(typeof(LanguageBundleSaver)) as LanguageBundleSaver)?.FindScene(sceneName));
        asyncLoad.allowSceneActivation = false;

        yield return new WaitWhile(() =>
        {
            // Debug.LogError(asyncLoad.progress);
            return asyncLoad.progress < .9f;
        });
        // while (asyncLoad.progress < .9f)
        // {
        //     yield return new WaitForSeconds(.5f);
        // }

        PlayScene();

        // OnLoaded();
    }

    [Button]
    public void PlayScene()
    {
        asyncLoad.allowSceneActivation = true;
        actionAfterLoadNewScene?.Invoke();
        RegisterSingleton._instance.CheckRegister();
    }

    public void PlayAfterLoad()
    {
        // _canvasGroup.DOFade(0, 1f);
    }

    IEnumerator PlaySceneCorotine()
    {
        yield return new WaitForSeconds(3);

        PlayAfterLoad();
    }
}