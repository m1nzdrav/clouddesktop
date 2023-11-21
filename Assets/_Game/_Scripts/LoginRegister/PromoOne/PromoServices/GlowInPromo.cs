using System;
using System.Collections;
using _Game._Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlowInPromo : MonoBehaviour
{
    [SerializeField] private Transform _dotTransform;
    [SerializeField] private bool isPlayed = false;
    [SerializeField] private bool alreadyStart = false;
    [SerializeField] private float animationTime = 1f;
    private AudioSource _audioSource;
    private int _countGlow = 0;

    public void SetGlow(Transform dotTransform)
    {
        _dotTransform = dotTransform;
    }

    public void StartGlow()
    {
        // _dotTransform = dotTransform;
        CanvasGroup canvasGroup = _dotTransform.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        _audioSource = _dotTransform.GetComponent<AudioSource>();
        alreadyStart = true;
        StartCoroutine(AnimationBreath());
    }

    public void FastStartGlow()
    {
        CanvasGroup canvasGroup = _dotTransform.GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        _audioSource = _dotTransform.GetComponent<AudioSource>();
        alreadyStart = true;
        _dotTransform.DOScale(Vector3.one * 2.5f, animationTime);
        // StartCoroutine(AnimationBreath());
    }

    private void OnEnable()
    {
        if (alreadyStart && !isPlayed) StartCoroutine(AnimationBreath());
    }

    public void StopGlow()
    {
        isPlayed = true;
    }

    private IEnumerator AnimationBreath()
    {
        _dotTransform.DOScale(Vector3.one * 2.5f, animationTime);


        if (_countGlow <= Config.MAX_COUNT_SOUND_GLOW)
        {
            _audioSource.Play();
            _countGlow++;
        }

        yield return new WaitForSeconds(animationTime);

        _dotTransform.DOScale(Vector3.one, animationTime)
            .OnComplete((() =>
            {
                if (!isPlayed && gameObject.activeInHierarchy)
                {
                    StartCoroutine(AnimationBreath());
                }
            }));
    }
}