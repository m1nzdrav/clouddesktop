using System;

using UnityEngine;


public class MYTMenu : MonoBehaviour
{
    [SerializeField] private RotatePlanes rotatePlanes;
    [SerializeField] private ButtonRotateGlow currentButton;
    [SerializeField] private HighlightsButtonRotate currentHighlightsButtonRotate;

    public void Rotate(StateRotate stateRotate, Action action, ButtonRotateGlow currentButton)
    {
        ChangeCurrentButton(currentButton);
        if (rotatePlanes.CurrentSettingsRotate.StateRotate == stateRotate)
        {
            rotatePlanes.ChangeRotateVertical();
            action?.Invoke();
        }
        else
        {
            rotatePlanes.RotateToFrontier(stateRotate, action);
        }
    }

    private void ChangeCurrentButton(ButtonRotateGlow newCurrentButton)
    {
        if (currentButton == null)
        {
            currentButton = newCurrentButton;
        }
        else if (currentButton != newCurrentButton)
        {
            // currentButton.Block = false;
            currentButton = newCurrentButton;
            // newCurrentButton.Block = !newCurrentButton.Block;
        }

        // currentButton.Block = !currentButton.Block;
    }

    public void GoHome(ButtonRotateGlow buttonRotateGlow)
    {
        ChangeCurrentButton(buttonRotateGlow);
        rotatePlanes.RotateHome();
    }

    public void ChangeHighlightButton(HighlightsButtonRotate newButton)
    {
        currentHighlightsButtonRotate?.OffPressed();
        currentHighlightsButtonRotate = newButton;
    }
}