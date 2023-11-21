using System;
using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> needLock;
    [SerializeField] private bool isLock;
    [SerializeField] private TopPanelSetParametrs topPanel;
    [SerializeField] private FolderModel mainFolderModel;


    private void Awake()
    {
       
    }

    public bool IsLock
    {
        get => isLock;
        set
        {
            isLock = value;

            if (mainFolderModel == null)
            {
                mainFolderModel = topPanel.MainFolder.GetComponent<FolderModel>();
            }

            mainFolderModel.IsLock = value;
        }
    }

    public void Click()
    {
        if (isLock)
        {
            IsLock = false;
            // UnLockObject();
        }
        else
        {
            IsLock = true;
            // LockObject();
        }

        // _managerJson.UpdateJson(topPanel.transform.parent.gameObject);
    }

    public void LockObject()
    {
        IsLock = true;
        foreach (var VARIABLE in needLock)
        {
            VARIABLE.interactable = false;
        }
    }

    public void UnLockObject()
    {
        IsLock = false;
        foreach (var VARIABLE in needLock)
        {
            VARIABLE.interactable = true;
        }
    }
}