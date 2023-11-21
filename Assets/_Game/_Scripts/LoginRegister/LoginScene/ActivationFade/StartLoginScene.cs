using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StartLoginScene : MonoBehaviour
{
    [SerializeField] private List<StartAnimation> Animations;
    [SerializeField] private List<ActivateCanvas> activate;
    [SerializeField] private float animationDuration;
    [SerializeField] private bool first;

    public void StartLogin()
    {
        if (first)
            return;

        first = true;
        activate.ForEach(x => x.Activate());
        Animations.ForEach(x => x.FirstStart());
    }

    public void Reverse()
    {
        first = false;
        StartLogin();
    }
}