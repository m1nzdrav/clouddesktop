using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Accessible
{
    Access,
    NoAccess
}
public class SetAccesIcon : MonoBehaviour
{
    [SerializeField] private bool Access = true;

    public bool IsAcces
    {
        get => Access;
        set
        {

            Access = value;
            GetComponent<IconFullController>().IsAcces = value;
            GetComponent<ShowHideObjectsScaleAndMove>().IsAccess = value;
        }
    }
}