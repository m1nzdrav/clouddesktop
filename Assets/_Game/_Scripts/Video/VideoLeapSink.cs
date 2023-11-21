using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Video;

public class VideoLeapSink : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoSource;
    [SerializeField] private CanvasGroup cover;
    [SerializeField] private ActivateCanvas videoTexture;
    [SerializeField] private TimingVideo timingVideo;

    [SerializeField, FoldoutGroup("NameVideo")]
    private string nameVideo;

    [SerializeField, FoldoutGroup("NameVideo")]
    private string formatVideo = ".mp4";

    public void PrepareVideo()
    {
        videoSource.url = Application.streamingAssetsPath + "/" + nameVideo +
                          ChangeLanguageLoginScene.currentLanguage + formatVideo;
        videoSource.Prepare();
    }


    [Button]
    public void StartFromTiming(TimingVideo timingVideo)
    {
        StopAllCoroutines();
        cover.DOFade(0, .5f);
        videoTexture.Activate();
        StartCoroutine(IEStartTiming(timingVideo));
    }

    IEnumerator IEStartTiming(TimingVideo timingVideo)
    {
        videoSource.time = timingVideo.start;

        yield return new WaitForEndOfFrame();

        videoSource.Play();

        yield return new WaitForSeconds(timingVideo.end - timingVideo.start);

        videoSource.Pause();
    }
}