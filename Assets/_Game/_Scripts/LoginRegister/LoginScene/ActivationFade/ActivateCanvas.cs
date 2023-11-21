using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class ActivateCanvas : Activity
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _animationDuration = 1;

    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent _unityEvent;

    [SerializeField, FoldoutGroup("Event")]
    private UnityEvent _unityEventEndActivity;

    [SerializeField] private float fastActivateDuration = .3f;

    public void FastActivate()
    {
        _canvasGroup.DOFade(1, fastActivateDuration);
        _canvasGroup.blocksRaycasts = true;
        OnInteractable();
    }
    public void FastFade()
    {
        _canvasGroup.DOFade(1, fastActivateDuration);
       
    }

    public void Activate()
    {
        OnFade();
        OnRaycast();
        OnInteractable();
        _unityEvent?.Invoke();
    }

    public void OnFade()
    {
        _canvasGroup.DOFade(1, _animationDuration).OnComplete(EndActivity);
    }

    public override void StartActivity()
    {
        Activate();
    }

    public override void EndActivity()
    {
        _unityEventEndActivity?.Invoke();
        // NextActivity(null);
    }

    public void DeActivate()
    {
        OffFade();
        OffInteractable();
        OffInteractable();
    }

    public void OffFade()
    {
        _canvasGroup.DOFade(0, _animationDuration);
    }

    public void DeActivate(float value)
    {
        _canvasGroup.DOFade(value, _animationDuration);
        _canvasGroup.blocksRaycasts = false;
        OffInteractable();
    }

    public void OnInteractable()
    {
        _canvasGroup.interactable = true;
    }

    public void OnRaycast()
    {
        _canvasGroup.blocksRaycasts = true;
    }

    public void OffInteractable()
    {
        _canvasGroup.interactable = false;
    }

    public void OffRaycast()
    {
        _canvasGroup.blocksRaycasts = false;
    }
}