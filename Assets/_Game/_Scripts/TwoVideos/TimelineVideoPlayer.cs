using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Video;

public class TimelineVideoPlayer : PrefabPlay
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private bool needLoad = true;
    [SerializeField] private string nameVideo;
    [SerializeField] private int number;
    [SerializeField] private UnityEvent prepareEvent;

    [Button]
    public void LoadVideo()
    {
        // _videoPlayer.Stop();
        // // Debug.LogError("Load");
        // if (needLoad)
        //     _videoPlayer.url = nameVideo;
        //
        // _videoPlayer.prepareCompleted += EndReached;
        // _videoPlayer.Prepare();
    }

    private void Start()
    {
        if (needLoad)
            _videoPlayer.url = Application.streamingAssetsPath+"/"+nameVideo;
        // _videoPlayer.clip = RegisterSingleton._instance.LanguageBundleSaver.VideoAb.LoadAllAssets<VideoClip>()[number];
        _videoPlayer.prepareCompleted += EndReached;
        _videoPlayer.Prepare();
    }

    void EndReached(VideoPlayer vp)
    {
        Debug.LogError("load " + gameObject.name);
    }
[Button]
    public override void Play()
    {
        _videoPlayer.Play();
    }

    public override void Prepare()
    {
        // if (needLoad)
        //     // _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, nameVideo);
        //     _videoPlayer.clip = RegisterSingleton._instance.LanguageBundleSaver.FindVideo(number);
        //
        //

        prepareEvent?.Invoke();
        // StartCoroutine(WaitPrepare());
    }

    public override void Off()
    {
        _videoPlayer.Stop();
    }

    public override void Pause()
    {
        _videoPlayer.Pause();
    }

    public override void SetTime(float time)
    {
        _videoPlayer.time = time;
    }

    public override float GetMaxTime()
    {
        if (_videoPlayer == null)
            return 0;

        return (float)_videoPlayer.length;
    }

    IEnumerator WaitPrepare()
    {
        while (!_videoPlayer.isPrepared)
        {
            yield return null;
        }

        prepareEvent?.Invoke();
    }

    [Button]
    public void Test()
    {
        // Debug.LogError("Test -------------- "+ _videoPlayer.GetTargetAudioSource(0).GetSpectrumData().frequency);
    }
}