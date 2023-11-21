using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShowHide
{
    bool NeedShow { get; set; }
    bool IsShowed { get; set; }
 
    float HideAnimationDuration{ get; set; }
    void Show(bool isButton=false);
    void Hide();
    float ShowChildFolder();

    float HideChildFolder();

    void ShowHideObject();
    void ShowObject(bool isButton=false);
    void HideObject();
    
}