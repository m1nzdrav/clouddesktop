using System;
using System.Collections.Generic;
using _Game._Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LanguageBundleSaver : MonoBehaviour, ISingleton
{
    public string NameComponent
    {
        get => name;
    }

    [SerializeField] private bool needLoad = true;
    [SerializeField] private BundleDownloader bundleDownloader;
    [SerializeField] private SceneEnum scene;
    [SerializeField] private VideoClip[] _videoClips;
    [SerializeField] private List<string> videoName;

    [SerializeField] private AssetBundle languageAb;
    [SerializeField] private AssetBundle videoAb;
    [SerializeField] private AssetBundle sceneAb;
    public AssetBundle LanguageAb => languageAb;
    public AssetBundle VideoAb => videoAb;

    public AssetBundle SceneAb => sceneAb;

    public SceneEnum SceneEnum => scene;

    public void CheckRegister()
    {
        if (needLoad)
        {
            DownloadAb();
        }
    }

    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);

        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    private void DownloadAb()
    {
        switch (scene)
        {
            case SceneEnum.Demo:
            {
                DemoScene();
                break;
            }
            case SceneEnum.Invitation:
            {
                InvitationScene();
                break;
            }
            case SceneEnum.Presentation:
            {
                PresentationScene();
                break;
            }
            case SceneEnum.Main:
            {
                MainScene();
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        AllScene();
    }

    private void PresentationScene()
    {
        bundleDownloader.Download(Config.URL_BUNDLE_PRESENTATION, SaveLanguageBundle);

        if (!needLoad)
            return;

        bundleDownloader.Download(Config.URL_BUNDLE_SCENE_PRESENTATION, SaveScene);
    }

    private void InvitationScene()
    {
        bundleDownloader.Download(Config.URL_BUNDLE_INVITATION, SaveLanguageBundle);

        if (!needLoad)
            return;

        bundleDownloader.Download(Config.URL_BUNDLE_SCENE_INVITATION, SaveScene);
    }

    private void DemoScene()
    {
        bundleDownloader.Download(Config.URL_BUNDLE_DEMO, SaveLanguageBundle);
        bundleDownloader.Download(Config.URL_BUNDLE_AUDIO, SaveAudioBundle);

        if (!needLoad)
            return;

        bundleDownloader.Download(Config.URL_BUNDLE_SCENE_DEMO, SaveScene);
    }

    private void MainScene()
    {
        bundleDownloader.Download(Config.URL_BUNDLE_MAIN, SaveLanguageBundle);
        bundleDownloader.Download(Config.URL_BUNDLE_AUDIO, SaveAudioBundle);

        if (!needLoad)
            return;

        bundleDownloader.Download(Config.URL_BUNDLE_SCENE_MAIN, SaveScene);
    }

    public void AllScene()
    {
        bundleDownloader.Download(Config.URL_BUNDLE_FONTS, SaveFontsBundle);
        // bundleDownloader.Download(Config.URL_BUNDLE_VIDEO, SaveVideoBundle);
    }

    private void SaveLanguageBundle(AssetBundle assetBundle)
    {
        languageAb = assetBundle;
    }

    private void SaveFontsBundle(AssetBundle assetBundle)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(FontSingleton)) as FontSingleton)?.AddFonts(assetBundle);
    }

    private void SaveAudioBundle(AssetBundle assetBundle)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(MusicStock)) as MusicStock)?.AddBundleMusic(assetBundle);
    }

    private void SaveVideoBundle(AssetBundle assetBundle)
    {
        if (assetBundle == null)
        {
            Debug.LogError("null Bundle");
            return;
        }

        videoAb = assetBundle;
        _videoClips = videoAb.LoadAllAssets<VideoClip>();
    }

    private void SaveScene(AssetBundle assetBundle)
    {
        sceneAb = assetBundle;
    }

    public string FindScene(string name)
    {
        if (!needLoad)
            return name;

        foreach (var VARIABLE in sceneAb.GetAllScenePaths())
        {
            if (VARIABLE.Contains(name))
            {
                return VARIABLE;
            }
        }

        return (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)?.StartScene;
    }

    public VideoClip FindVideo(int number)
    {
        return _videoClips[number];
    }
}