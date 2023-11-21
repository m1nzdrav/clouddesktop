using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EventInvoker : MonoBehaviour
{
    [Tooltip("Events to be raised on this object ENABLE")]
    public GameEvent[] EventsOnEnable;
    [Tooltip("Events to be raised on this object ENABLE")]
    public GameEvent[] EventsOnDisable;

    public void RunOnEnable()
    {
        foreach (GameEvent ev in EventsOnEnable) { ev.Raise(); }
    }

    public void RunOnDisable()
    {
        foreach (GameEvent ev in EventsOnEnable) { ev.Raise(); }
    }

    private void OnEnable()
    {
        RunOnEnable();
    }

    private void OnDisable()
    {
        RunOnDisable();
    }
}
