using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ActionAfterWait : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    public void StartAction(float wait)
    {
        StartCoroutine(Waiter(wait));
    }

    IEnumerator Waiter(float wait)
    {
        yield return new WaitForSeconds(wait);
        action?.Invoke();
    }
}