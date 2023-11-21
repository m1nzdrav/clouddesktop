using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameEvent playVideo;
    [SerializeField] private GameEvent pauseVideo;
    [SerializeField] private VideoManager _videoManager;
 
  
[SerializeField]
    private bool firstStart;


    public bool FirstStart
    {
        get => firstStart;
        set => firstStart = value;
    }


    [SerializeField] private Button[] pauseButtons;
    [SerializeField] private Button[] playButtons;


    public void ShowButtons()
    {
        if (_videoPlayer.isPlaying)
            foreach (var VARIABLE in pauseButtons)
            {
                VARIABLE.GetComponent<ButtonController>().ShowButton();
            }
    }

    public void HideButtons()
    {
        if (_videoPlayer.isPlaying)
        {
            foreach (var VARIABLE in pauseButtons)
            {
                VARIABLE.GetComponent<ButtonController>().HideButton();
            }
        }
    }

    public void TogglePlayback(string nameCalledObject)
    {
        var _isClick = FlexibleDraggableObject.IsCkick;

        if (FirstStart && !_videoManager.IsHide)

            if (_isClick)

            {
                if (_videoPlayer.isPlaying)
                {
                    Debug.LogError("pauseAllVideo cause video is playing == " + _videoPlayer.isPlaying + " " +
                                   "I`m called from " + nameCalledObject);
                    pauseVideo.Raise();
                }
                else
                {
                    Debug.LogError("playAllVideo cause video is playing == " + _videoPlayer.isPlaying + " " +
                                   "I`m called from " + nameCalledObject + gameObject.name);
                    playVideo.Raise();
                }
            }
            else
            {
                FlexibleDraggableObject.IsCkick = true;
            }
    }

  
}