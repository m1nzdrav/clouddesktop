
using UnityEngine;

public class MenuProfile : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private ButtonState currentButtonState;

    public void StartState(ButtonState buttonState)
    {
        if (currentButtonState != null && currentButtonState != buttonState) currentButtonState.IsPressed = false;

        currentButtonState = buttonState;

        if (stateMachine == null)
            stateMachine = new StateMachine(buttonState.State);
        else
            stateMachine.StartState(buttonState.State);
    }

    public void ExitState()
    {
        currentButtonState = null;
        stateMachine.ExitState();
    }
}