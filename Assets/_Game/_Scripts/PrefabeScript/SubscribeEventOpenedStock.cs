using _Game._Scripts.PrefabeScript;
using UnityEngine;
using UnityEngine.Events;

public abstract class SubscribeEventOpenedStock:MonoBehaviour
{
    private EventListener _listenerOpenedStock;
    private EventListener _listenerRemoveStock;

   

    public void ResetListener()
    {
        if (_listenerOpenedStock == null)
        {
            _listenerOpenedStock = gameObject.AddComponent<EventListener>();
        }

        if (_listenerRemoveStock == null)
        {
            _listenerRemoveStock = gameObject.AddComponent<EventListener>();
        }
    }

    protected void AddMethodToListener(UnityAction OpenedTrigger,UnityAction RemoveTrigger)
    {
        if (_listenerOpenedStock == null || _listenerRemoveStock == null)
        {
            ResetListener();
        }

        _listenerOpenedStock.Response.AddListener(OpenedTrigger);
        _listenerRemoveStock.Response.AddListener(RemoveTrigger);
    }

    protected void RegisterEvent()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.RegisterRemoveTrigger(_listenerRemoveStock);
        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.RegisterOpenedTrigger(_listenerOpenedStock);
    }
}