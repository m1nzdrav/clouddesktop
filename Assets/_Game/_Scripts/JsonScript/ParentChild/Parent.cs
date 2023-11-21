using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class Parent : MonoBehaviour
{
    [Header("Parent")] [SerializeField] private List<GameObject> childs;
    public List<GameObject> Childs => childs;

    [SerializeField] private Parent myParent;

    public Parent MyParent
    {
        get => myParent;
        set => myParent = value;
    }

    [SerializeField] private ParentHierarchy _parentHierarchy;

    public ParentHierarchy ParentHierarchy => _parentHierarchy;

    public GameObject My
    {
        get => gameObject;
    }

    private GameEvent addChild, removeChild;

    public void RegisterForAllEvent(EventListener listener)
    {
        RegisterAddChild(listener);
        RegisterRemoveChild(listener);
    }

    protected void InstantiateEvent()
    {
        if (addChild != null) return;

        addChild = new GameEvent();
        removeChild = new GameEvent();
    }


    public void RegisterRemoveChild(EventListener listener)
    {
        InstantiateEvent();
        removeChild.Register(listener);
    }

    public void RegisterAddChild(EventListener listener)
    {
        InstantiateEvent();
        addChild.Register(listener);
    }

    public abstract void SpawnChild(GameObject child);

    public void SetChild(GameObject child)
    {
        if (childs.Contains(child))
            return;

        if (addChild == null)
            InstantiateEvent();

        childs.Add(child);
        RaiseEvent(addChild.Raise);
    }

    private void RaiseEvent(UnityAction action)
    {
        if (addChild == null)
        {
            InstantiateEvent();
        }

        action.Invoke();
    }

    public void RemoveChild(GameObject child)
    {
        if (!childs.Contains(child))
            return;

        childs.Remove(child);

        RaiseEvent(removeChild.Raise);
    }
}