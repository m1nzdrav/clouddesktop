using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class AnimationEqualizerCircle : MonoBehaviour
{
    [SerializeField] private GameObject _audioSource;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private bool needInParent = true;
    [SerializeField] private Transform parentCircle;
    [SerializeField] private List<Transform> circles;
    [SerializeField] private float frequency;

    public void StartAnimation()
    {
        if (needInParent)
        {
            circles = parentCircle.GetComponentsInChildren<Transform>().ToList();
        }

        List<float> stockSize = new List<float>(circles.Count);
        stockSize.AddRange(circles.Select(circle => circle.localScale.x));
        
        if (_videoPlayer != null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            (RegisterSingleton._instance.GetSingleton(typeof(AudioEqualizer)) as AudioEqualizer)?.SetVideo(circles, stockSize,
                _videoPlayer, frequency);
#else
            (RegisterSingleton._instance.GetSingleton(typeof(AudioEqualizer)) as AudioEqualizer)?.SetAudio(circles, stockSize,
                _audioSource.GetComponent<AudioSource>(), frequency);
#endif
        }
        else
        {
            (RegisterSingleton._instance.GetSingleton(typeof(AudioEqualizer)) as AudioEqualizer)?.SetAudio(circles, stockSize,
                _audioSource.GetComponent<AudioSource>(), frequency);
        }
    }

    public void EndAnimation()
    {
        if (_videoPlayer != null) (RegisterSingleton._instance.GetSingleton(typeof(AudioEqualizer)) as AudioEqualizer)?.ResetVideo();

        (RegisterSingleton._instance.GetSingleton(typeof(AudioEqualizer)) as AudioEqualizer)?.ResetAudio();
    }
}