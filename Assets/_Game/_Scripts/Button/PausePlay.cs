using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class PausePlay : MonoBehaviour
{
    [SerializeField] private Image play;
    [SerializeField] private Image pause;


    public void ChangePauseAndPlay()
    {
        if (pause.enabled)
        {
            SetPlay();
        }
        else
        {
            SetPause();
        }
    }

    public void SetPlay()
    {
        pause.enabled = false;
        play.enabled = true;
    }


    public void SetPause()
    {
        play.enabled = false;
        pause.enabled = true;
    }
}