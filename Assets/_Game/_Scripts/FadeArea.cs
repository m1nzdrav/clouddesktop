using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class FadeArea : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> areas;
    [SerializeField] private float animationDuration;

    public void CloseArea(Action action)
    {
        FadingCanvasGroup(0, action);
    }

    public void OpenArea(Action action)
    {
        FadingCanvasGroup(1, action);
    }

    private void FadingCanvasGroup(float endValue, Action action)
    {
        bool firstElem = true;
        foreach (CanvasGroup VARIABLE in areas)
        {
            if (firstElem && action != null)
            {
                firstElem = false;
                VARIABLE.DOFade(endValue, animationDuration).OnComplete(action.Invoke);
            }

            VARIABLE.DOFade(endValue, animationDuration);
        }
    }
}