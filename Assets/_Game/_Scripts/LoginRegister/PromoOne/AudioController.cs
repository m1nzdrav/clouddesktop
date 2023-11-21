using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private float _delay = 0.3f;


    void Start()
    {
        StartCoroutine(StartAudio());
    }

    IEnumerator StartAudio()
    {
        yield return new WaitForSeconds(_delay);

        _audio.clip = _clip;
        _audio.Play();
    }
}
