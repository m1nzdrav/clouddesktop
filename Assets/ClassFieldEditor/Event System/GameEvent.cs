using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class GameEvent 
{
    List<EventListener> _eventListeners = new List<EventListener>();

    [Title("Description", bold: false, horizontalLine: false)] [HideLabel] [MultiLineProperty(5)]
    public string Description;

    public bool log = false;

    public GameEvent()
    {
        _eventListeners = new List<EventListener>();
    }

    [Button(ButtonSizes.Large)]
    public void Raise()
    {
        // if (log)
        //     Debug.Log(this.name + " event raised");

        for (int i = 0; i < _eventListeners.Count; i++)
        {
            _eventListeners[i].OnEventRaised();
        }
    }

    public void Register(EventListener listener)
    {
   
        if (!_eventListeners.Contains(listener))
        {
            _eventListeners.Add(listener);
        }
    }

    public void DeRegister(EventListener listener)
    {
        if (_eventListeners.Contains(listener))
        {
            _eventListeners.Remove(listener);
        }
    }
}