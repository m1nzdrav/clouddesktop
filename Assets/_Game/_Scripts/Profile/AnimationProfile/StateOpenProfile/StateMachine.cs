using System;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class StateMachine
{
    [SerializeField] public State _currentState;

    public StateMachine(State state)
    {
        _currentState = state;
        _currentState.EnterState();
    }

    public void StartState(State state)
    {
        ExitState();


        _currentState = state;
        _currentState.EnterState();
    }

    public void ExitState()
    {
        if (_currentState == null)
            return;
        
        _currentState.ExitState();
        _currentState = null;
    }
}