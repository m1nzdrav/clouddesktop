using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideButtona : MonoBehaviour
{
    [SerializeField] private bool _isShowed = true;
    [SerializeField] private GameObject needHideAndShow;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowHideObject); 
    }

    public void ShowHideObject()
    {
        if (_isShowed)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        _isShowed = true;
        needHideAndShow.SetActive(true);
        
    }

    public void Hide()
    {
        _isShowed = false;
        needHideAndShow.SetActive(false);
    }
}