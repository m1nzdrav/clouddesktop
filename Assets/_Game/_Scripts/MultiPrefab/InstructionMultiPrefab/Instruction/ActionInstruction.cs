using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ActionInstruction : AInstruction
{
    [SerializeField] private float maxTime;
    public UnityEvent prepareEvent;
    public UnityEvent onEvent;
    public UnityEvent offEvent;
    public UnityEvent pauseEvent;
    public UnityEvent<float> setTimeEvent;


    public override Action OnAction()
    {
        return onEvent.Invoke;
    }

    public override Action OffAction()
    {
        return offEvent.Invoke;
    }

    public override Action GetPause()
    {
        return pauseEvent.Invoke;
    }

    public override Action Restart()
    {
        return offEvent.Invoke;
    }

    public override Action Prepare()
    {
        return prepareEvent.Invoke;
    }

    public override void SetTime(float time)
    {
        setTimeEvent?.Invoke(time);
    }

    public override float GetMaxTime()
    {
        return maxTime;
    }
}