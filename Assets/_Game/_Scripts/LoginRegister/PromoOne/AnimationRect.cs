using System;
using DG.Tweening;
using Doozy.Engine.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class AnimationRect : MonoBehaviour
{
    [SerializeField] private RectTransform target;

    [SerializeField, Header("Work offset")]
    private Vector2 offsetMax;

    [SerializeField] private Vector2 offsetMin;

    [SerializeField, FoldoutGroup("Base")] private Vector2 offsetBaseMax;

    [SerializeField, FoldoutGroup("Base")] private Vector2 offsetBaseMin;

    [SerializeField, FoldoutGroup("Base")] private Vector2 anchoredBaseMax;

    [SerializeField, FoldoutGroup("Base")] private Vector2 anchoredBaseMin;

    [SerializeField, FoldoutGroup("Target")]
    private Vector2 offsetTargetMax;

    [SerializeField, FoldoutGroup("Target")]
    private Vector2 offsetTargetMin;

    [SerializeField, FoldoutGroup("Target")]
    private Vector2 anchoredTargetMax;

    [SerializeField, FoldoutGroup("Target")]
    private Vector2 anchoredTargetMin;

    [SerializeField] private bool animTrigger;
    [SerializeField] private float step = .05f;
    [SerializeField] private UnityEvent eventAfterAnim;

    private const double TOLERANCE = .1f;

    [Button]
    public void Test()
    {
        Debug.LogError(target.anchoredPosition);
        Debug.LogError(target.anchorMax);
        Debug.LogError(target.anchorMin);
    }

    [Button]
    public void ToTargetAnimWhitAnchored()
    {
        target.DOAnchorMax(anchoredTargetMax, 1f);
        target.DOAnchorMin(anchoredTargetMin, 1f);

        ToTargetAnim();
    }


    [Button]
    public void ToTargetAnim()
    {
        offsetMax = offsetTargetMax;
        offsetMin = offsetTargetMin;
        StartAnim();
    }

    [Button]
    public void ToBaseAnimWhitAnchored()
    {
        target.DOAnchorMax(anchoredBaseMax, 1f);
        target.DOAnchorMin(anchoredBaseMin, 1f);

        // target.anchorMax = anchoredBaseMax;
        // target.anchorMin = anchoredBaseMin;

        ToBaseAnim();
    }

    [Button]
    public void ToBaseAnim()
    {
        offsetMax = offsetBaseMax;
        offsetMin = offsetBaseMin;
        StartAnim();
    }

    public void StartAnim()
    {
        animTrigger = true;
    }

    private void FixedUpdate()
    {
        if (!animTrigger) return;
        Debug.LogWarning(target.gameObject.name + " " + target.offsetMax + " " + offsetMax + " " + step);
        target.offsetMax = Vector2.Lerp(target.offsetMax, offsetMax, step);
        target.offsetMin = Vector2.Lerp(target.offsetMin, offsetMin, step);

        if (!(Math.Abs(target.offsetMax.x - offsetMax.x) < TOLERANCE) ||
            !(Math.Abs(target.offsetMin.x - offsetMin.x) < TOLERANCE)) return;

        animTrigger = false;
        eventAfterAnim.Invoke();
    }
}