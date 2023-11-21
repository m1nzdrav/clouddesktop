using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class Activity : MonoBehaviour
{
    [SerializeField] private float timeToWaitRevers;

    [SerializeField, FoldoutGroup("Event activity")]
    private UnityEvent fastAction;

    [SerializeField, FoldoutGroup("Event activity")]
    public UnityEvent reversEnd;

    public bool first = false;
    public abstract void StartActivity();
    public abstract void EndActivity();

    public void ReverseActivity(bool needRevers)
    {
        StartCoroutine(AwaiterReverse(needRevers));
    }

    IEnumerator AwaiterReverse(bool needRevers)
    {
        reversEnd?.Invoke();
        yield return new WaitForSeconds(timeToWaitRevers);


        if (needRevers)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)?.CurrentPromoActivity
                .Revers();
        }
    }

    public void FirstStart()
    {
        first = false;
        StartActivity();
    }

    protected void NextActivity(Action action)
    {
        if (first)
            return;

        first = true;
        action?.Invoke();
        (RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)?.CurrentPromoActivity
            .ChangeActivity();
    }

    public void FastStart()
    {
        fastAction?.Invoke();
    }
}