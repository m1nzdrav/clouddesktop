using UnityEngine;

public class ShowableFolder : MonoBehaviour
{
    public void ShowChildFolder()
    {
        foreach (var VARIABLE in GetComponentsInChildren<IShowHide>())
        {
            if (VARIABLE.NeedShow)
            {
                VARIABLE.ShowHideObject();
                VARIABLE.NeedShow = false;
            }
        }

    }

    public void HideChildFolder()
    {
        foreach (var VARIABLE in GetComponentsInChildren<IShowHide>())

        {
            if (VARIABLE.IsShowed)
            {
                VARIABLE.NeedShow = true;
                VARIABLE.ShowHideObject();
            }
        }
    }
}