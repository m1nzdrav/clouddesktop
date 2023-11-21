using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskControl_cell : MonoBehaviour
{
    [HideInInspector] public GridControllerInFolderDesktop gridControllerInFolderDesktop;
    RectMask2D rectMask2D;

    private void OnEnable()
    {
        rectMask2D = GetComponent<RectMask2D>();
        SwitchMask(gridControllerInFolderDesktop.ShowNamespaces);
        gridControllerInFolderDesktop.onSwitchVisibility += SwitchMask;
    }

    private void OnDisable()
    {
        gridControllerInFolderDesktop.onSwitchVisibility -= SwitchMask;
    }

    /*На случай если OnEnable/OnDisable нужно будет заменить
    public void MaskEventInitial() 
    {
        SwitchMask(gridControllerInFolderDesktop.showNamespaces);
        gridControllerInFolderDesktop.onSwitchVisibility += SwitchMask;
    }

    public void MaskEventDeactiv() 
    {
        gridControllerInFolderDesktop.onSwitchVisibility -= SwitchMask;
    }*/

    void SwitchMask(bool showNamespaces)
    {
        if (showNamespaces)
            rectMask2D.enabled = false;
        else
            rectMask2D.enabled = true;
    }
}
