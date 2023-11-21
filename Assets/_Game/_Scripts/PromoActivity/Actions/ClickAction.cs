using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickAction : Activity
{
    [SerializeField] private Button triggerObject;
    [SerializeField] private UnityEvent _startEvent;
    [SerializeField] private UnityEvent _endEvent;

    public override void StartActivity()
    {
        Debug.LogError("AddListener");
        triggerObject.onClick.AddListener(EndActivity);
        _startEvent.Invoke();
    }

    public override void EndActivity()
    {
        triggerObject.onClick.RemoveListener(EndActivity);
        _endEvent.Invoke();
        NextActivity(null);
    }
}