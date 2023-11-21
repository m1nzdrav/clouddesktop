using UnityEngine;
using UnityEngine.Events;

public abstract class EventChild : MonoBehaviour
{
    [SerializeField] private EventListener _listenerAddChild;
    private EventListener _listenerRemoveChild;
    public Parent folderParentChild;

    // public Parent FolderParentChild => folderParentChild;

    public void ResetListener()
    {
        if (_listenerAddChild == null)
        {
            _listenerAddChild = gameObject.AddComponent<EventListener>();
        }

        if (_listenerRemoveChild == null)
        {
            _listenerRemoveChild = gameObject.AddComponent<EventListener>();
        }
    }

    public void AddMethodToListener(UnityAction action)
    {
        ResetListener();

        _listenerAddChild.Response.AddListener(action);
        _listenerRemoveChild.Response.AddListener(action);
    }

    public void RegisterEvent()
    {
        folderParentChild.RegisterAddChild(_listenerAddChild);
        folderParentChild.RegisterRemoveChild(_listenerRemoveChild);
    }
}