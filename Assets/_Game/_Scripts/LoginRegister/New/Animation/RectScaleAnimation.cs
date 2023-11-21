using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class RectScaleAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform target;
    [SerializeField] private CanvasGroup targetCanvas;
    [SerializeField] private Vector2 baseOffsetMax;
    [SerializeField] private Vector2 baseOffsetMin;
    [SerializeField] private float timeDesAppear = .5f;
    [SerializeField] private float timeAppear = .5f;

    private void Start()
    {
        baseOffsetMax = target.offsetMax;
        baseOffsetMin = target.offsetMin;
    }

    [Button]
    public void Animate()
    {
        targetCanvas.DOFade(0, timeDesAppear).OnComplete(() =>
        {
            target.offsetMax = baseOffsetMax;
            target.offsetMin = baseOffsetMin;
            targetCanvas.DOFade(1, timeAppear);
        });
    }
}