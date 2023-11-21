using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class EventListener : MonoBehaviour
{
    public GameEvent[] Events;
    public UnityEvent Response;

    public EventListener()
    {
        Response = new UnityEvent();
    }

    void OnEnable()
    {
        if (Events==null|| Events.Length <= 0) return;
        
        foreach (GameEvent ev in Events)
        {
            ev.Register(this);
        }
    }

    void OnDisable()
    {
        if (Events==null|| Events.Length <= 0) return;
        
        foreach (GameEvent ev in Events)
        {
            ev.DeRegister(this);
        }
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}