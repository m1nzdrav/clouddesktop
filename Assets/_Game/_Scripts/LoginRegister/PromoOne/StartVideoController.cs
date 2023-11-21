using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartVideoController : MonoBehaviour
{
    [SerializeField] private List<VideoClip> _clips;
    [SerializeField] private Transform _video;
    [SerializeField] private VideoPlayer _videoPlayer;

    private int _clipCount = 0;

    public void ShowVideo()
    {
        StartCoroutine(Animation(_video.localScale, Vector3.one));
    }

    public void StartVideo()
    {
        _videoPlayer.Pause();
        _videoPlayer.clip = _clips[_clipCount];
        _videoPlayer.Play();
        _clipCount++;
    }

    public void HideVideo()
    {
        StartCoroutine(Animation(_video.localScale, Vector3.zero));
    }

    private IEnumerator Animation(Vector3 from, Vector3 to)
    {
        var time = 0f;
        const float period = 1f;

        while (time < period)
        {
            time += Time.deltaTime;
            var lTime = time / period;

            var lVector = Vector3.Lerp(from, to, lTime);
            _video.localScale = lVector;

            yield return null;
        }
    }
}
