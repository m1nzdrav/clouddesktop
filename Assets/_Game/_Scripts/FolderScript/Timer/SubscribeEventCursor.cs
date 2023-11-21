
using UnityEngine;
using UnityEngine.Events;

public abstract class SubscribeEventCursor : MonoBehaviour
{
    private EventListener _listenerShowCursor;
    private EventListener _listenerHideCursor;


    public void ResetListener()
    {
        if (_listenerHideCursor == null)
        {
            _listenerHideCursor = gameObject.AddComponent<EventListener>();
        }

        if (_listenerShowCursor == null)
        {
            _listenerShowCursor = gameObject.AddComponent<EventListener>();
        }
    }

    protected void AddMethodToListener(UnityAction HideTrigger, UnityAction ShowTrigger)
    {
        if (_listenerHideCursor == null || _listenerShowCursor == null)
        {
            ResetListener();
        }

        _listenerHideCursor.Response.AddListener(HideTrigger);
        _listenerShowCursor.Response.AddListener(ShowTrigger);
    }

    protected void RegisterEvent()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(TimerToHideCursor)) as TimerToHideCursor)?.FirstSecondEvent.RegisterSecond(_listenerHideCursor);
        (RegisterSingleton._instance.GetSingleton(typeof(TimerToHideCursor)) as TimerToHideCursor)?.FirstSecondEvent.RegisterFirst(_listenerShowCursor);
    }
}