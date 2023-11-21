
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour
{
    [field: SerializeField,
            Tooltip("Key presses to catch")]
    private List<KeyCode> keys;

    [field: SerializeField,
            Tooltip("Events to launch")]
    private UnityEvent events;
    
    // Update is called once per frame
    void Update()
    {
        if (keys.Count <= 0)
            return;

        foreach (KeyCode key in keys)
        {
            if(Input.GetKeyDown(key))
            {
                events.Invoke();
                break;
            }
           
        }
    }
    
    
}
