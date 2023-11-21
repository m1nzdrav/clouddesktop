using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDemo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private MoveCursor _moveCursor;
    [SerializeField] private VideoManager _videoManager;
    public void StartMoveCursor()
    {
        gameObject.GetComponent<Button>().interactable = false;
        //Deactivate video and Activate VidoeCover
        _videoManager.StopAll();
        _videoManager.ActivateAllBlackCover();
        // activate custom cursor & start him
        _moveCursor.GetComponent<CanvasController>().FadeIn();
        _moveCursor.StartVideo();
        //Deactivate standart cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.LogError("StartCursor");
        //Start move custom cursor
        _moveCursor.startMove();
    }

    public void ShowCursorAfterPlayVideo()
    {
        // Activate standart cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
}