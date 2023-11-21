using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class LockerPlane : MonoBehaviour

{
    [SerializeField] private CanvasGroup hideCanvasGroup, showCanvasGroup;

    [SerializeField] private float hideTime, showTime;

    [Button]
    public void StartAnimation()
    {
        hideCanvasGroup.DOFade(0, hideTime).OnComplete(() => showCanvasGroup.DOFade(1, showTime));
    }

    [Button]
    public void ReversAnimation()
    {
        showCanvasGroup.DOFade(0, showTime).OnComplete(() => hideCanvasGroup.DOFade(1, hideTime));
    }
}