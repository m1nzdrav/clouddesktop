using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.EventSystems;

public class DoubleInputField : InputField
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        (RegisterSingleton._instance.GetSingleton(typeof(TextInspector_Singleton)) as TextInspector_Singleton).DoubleText = true;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        Debug.Log("deselect");
        
        //RegisterSingleton._instance.TextInspectorSingleton.TextInspector.ChangeTextSize_InputType();
        (RegisterSingleton._instance.GetSingleton(typeof(TextInspector_Singleton)) as TextInspector_Singleton).DoubleText = false;
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        
        Debug.Log("submit");
        
        //RegisterSingleton._instance.TextInspectorSingleton.TextInspector.ChangeTextSize_InputType();
        (RegisterSingleton._instance.GetSingleton(typeof(TextInspector_Singleton)) as TextInspector_Singleton).DoubleText = false;
    }
}
