using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


public class ButtonNextAction : Activity
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private float fadeDuration = .5f;

    public override void StartActivity()
    {
        _canvasGroup.DOFade(1, fadeDuration);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public override void EndActivity()
    {
        _event?.Invoke();
        DeactivateButton();
        NextActivity(null);
    }

    public void DeactivateButton()
    {
        _canvasGroup.DOFade(0, fadeDuration);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}