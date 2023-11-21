using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class State
{
    public string nameState;
    public bool readyToExitEnd;
    public bool readyToStartEnd;
    [SerializeField] private UnityEvent _eventEnter;
    [SerializeField] private UnityEvent _eventExit;

    public bool CheckExitEnd()
    {
        return readyToExitEnd;
    }

    public bool CheckStartEnd()
    {
        return readyToStartEnd;
    }

    public void EnterState()
    {
        readyToStartEnd = false;
        _eventEnter?.Invoke();
    }

    public void ExitState()
    {
        readyToExitEnd = false;
        _eventExit?.Invoke();
    }
}