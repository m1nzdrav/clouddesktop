using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TwoActionWithWait : Activity
{
    [SerializeField] private float waitTime;

    [SerializeField, FoldoutGroup("First activity")]
    private UnityEvent firstAction;

    [SerializeField, FoldoutGroup("Second activity")]
    private UnityEvent secondAction;

    public void StartAnimation()
    {
        StartCoroutine(CoroutineAnimation());
    }

    IEnumerator CoroutineAnimation()
    {
        StartFirstAction();
        yield return new WaitForSeconds(waitTime);
        StartSecondAction();
    }

    public override void StartActivity()
    {
        StartCoroutine(CoroutineAnimation());
        // StartAnimation();
    }

    public override void EndActivity()
    {
    }

    [Button]
    private void StartFirstAction()
    {
        firstAction?.Invoke();
    }

    [Button]
    private void StartSecondAction()
    {
        secondAction.Invoke();
    }
}