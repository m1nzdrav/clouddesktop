using System;
using DG.Tweening;
using UnityEngine;

public class AnimArea : MonoBehaviour
{
    [SerializeField] private float animationDuration=0.5f;
    [SerializeField] private RectTransform _rectTransform;
    private Vector3 zeroScale = new Vector3(1,0,1);
    private void OnEnable()
    {
        _rectTransform.localScale = zeroScale;
        _rectTransform.DOScaleY(1, animationDuration);
    }
}
