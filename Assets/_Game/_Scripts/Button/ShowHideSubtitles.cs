using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideSubtitles : MonoBehaviour
{
    [SerializeField] private SubtitlesController _subtitlesController;

    public void ShowHide()
    {
        if (!gameObject.GetComponent<CircleMenuButtonController>().ClickButton)
            ShowSub();
        else
            HideSub();
    }

    public void ShowSub()
    {
        _subtitlesController.Show();
    }

    public void HideSub()
    {
        _subtitlesController.Hide();
    }
}