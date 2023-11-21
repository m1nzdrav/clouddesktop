using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ButtonRotateGlow))]
public class GridButton : MonoBehaviour
{
    [SerializeField] private StateRotate state;
    [SerializeField] private UnityEvent _action;
    [SerializeField] private MYTMenu _menu;

    [Button]
    public void Rotate()
    {
        // Debug.LogError(_action == null);
        _menu.Rotate(state, _action.Invoke, GetComponent<ButtonRotateGlow>());
    }
}