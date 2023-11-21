using System.Collections;
using System.Collections.Generic;
using _Game._Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextInspector_ZoneControl : MonoBehaviour
{
    [SerializeField] private EventTrigger trigger;
    
    private void Start()
    {
        trigger.triggers.Add(Config.GetEntry(() => { (RegisterSingleton._instance.GetSingleton(typeof(TextInspector_Singleton)) as TextInspector_Singleton).ShouldDeselect = false; }, EventTriggerType.PointerEnter));
        trigger.triggers.Add(Config.GetEntry(() => { (RegisterSingleton._instance.GetSingleton(typeof(TextInspector_Singleton)) as TextInspector_Singleton).ShouldDeselect = true; }, EventTriggerType.PointerExit));
    }
}
