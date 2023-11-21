using System;
using System.Collections;
using System.Collections.Generic;
using _Game._Scripts;
using _Game._Scripts.Volume;
using UnityEngine;

[RequireComponent(typeof(CircleSettings), typeof(AudioSource))]
public class CircleAudioAnimation : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CircleSettings _circleSettings;


    public void StartAnimation()
    {
        try
        {
            List<Circle> circles = _circleSettings.CircleFactory.GetCircles(_circleSettings);

            List<Transform> stockObj = new List<Transform>(circles.Count);
            List<float> stockSize = new List<float>(circles.Count);

            foreach (Circle circle in circles)
            {
                stockObj.Add(circle.transform);
                stockSize.Add(circle.transform.localScale.x);
            }

            (RegisterSingleton._instance.GetSingleton(typeof(AudioEqualizer)) as AudioEqualizer)?.SetAudio(stockObj,
                stockSize, _audioSource, 2f);

            (RegisterSingleton._instance.GetSingleton(typeof(VolumeController)) as VolumeController)?.VolumeDown();
            _audioSource.Play();
        }
        catch (Exception e)
        {
            _audioSource.Play();
            Debug.LogWarning(e);
        }

        if (_audioSource.clip == null) return;

        (RegisterSingleton._instance.GetSingleton(typeof(VolumeController)) as VolumeController)?.VolumeDownForTimer(
            _audioSource.clip.length +
            Config.PAUSE_AFTER_CHANGE_VOLUME);
    }
}