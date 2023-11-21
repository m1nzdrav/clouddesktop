using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;


public class CustomVideoPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string nameVideo;

    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent afterLoad;

    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent afterEnd;

    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent play;

    private void Start()
    {
        videoPlayer.url = Application.streamingAssetsPath + "/" + nameVideo;
        videoPlayer.prepareCompleted += AfterLoadVideo;
        videoPlayer.loopPointReached += EndVideo;
        videoPlayer.Prepare();
    }

    [Button]
    public void PlayVideo()
    {
        play.Invoke();
        videoPlayer.Play();
    }

    private void AfterLoadVideo(VideoPlayer vp)
    {
        afterLoad.Invoke();
    }

    private void EndVideo(VideoPlayer vp)
    {
        afterEnd.Invoke();
    }
}