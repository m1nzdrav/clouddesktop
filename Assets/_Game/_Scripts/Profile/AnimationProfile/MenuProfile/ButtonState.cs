using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonState : MonoBehaviour
{
    [SerializeField] private MenuProfile menuProfile;
    [SerializeField] private State state;
    public State State => state;
    public bool IsPressed;
    [SerializeField] private bool needChangePressed = true;

    [SerializeField] private SelectedWindow selectedWindow;
    [SerializeField] private Button button;

    private void Start()
    {
        selectedWindow?.Subscribe(ChangePressState);
    }

    public void PressButton()
    {
        if (!IsPressed)
            menuProfile.StartState(this);
        else
            menuProfile.ExitState();

        if (button != null)
            StartCoroutine(StopInteractivity());
        
        if (needChangePressed)
            IsPressed = !IsPressed;
    }

    private void ChangePressState(bool value)
    {
        IsPressed = value;
    }

    IEnumerator StopInteractivity()
    {
        button.interactable = false;
        yield return new WaitForSeconds(1.5f);
        button.interactable = true;
    }
}