
using Sirenix.OdinInspector;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private State firstOpen;
    [SerializeField] private State exit;

    public void ChangeStateEnterReady()
    {
        stateMachine._currentState.readyToStartEnd = true;
    }
    public void ChangeStateExitReady()
    {
        stateMachine._currentState.readyToExitEnd = true;
    }
    [Button]
    public void Init()
    {
        stateMachine = new StateMachine(firstOpen);
    }

    [Button]
    public void ChangeToExit()
    {
        stateMachine.StartState(exit);
    }

    [Button]
    public void ChangeToStart()
    {
        stateMachine.StartState(firstOpen);
    }
}