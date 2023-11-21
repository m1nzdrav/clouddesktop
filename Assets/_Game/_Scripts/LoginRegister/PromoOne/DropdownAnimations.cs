using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DropdownAnimations : MonoBehaviour
{
    [SerializeField] private CircleSettings circleSettings;
    [SerializeField] private GameObject _dropdownTrigger;

    public void ShowHideCircles()
    {
        if (_dropdownTrigger.activeSelf)
        {
            //_circleManager.ShowCircles();
        }
        else
        {
            //_circleManager.HideCircles();
        }
    }
}
