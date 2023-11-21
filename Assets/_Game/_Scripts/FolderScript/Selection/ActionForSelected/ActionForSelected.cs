using System;
using UnityEngine;
using UnityEngine.Events;

public class ActionForSelected : MonoBehaviour
{
    [SerializeField] private SelectedWindow selectedWindow;
    [SerializeField] private UnityEvent actionSelect, actionDeSelect;

    private void Start()
    {
        if (selectedWindow != null) selectedWindow.Subscribe(ChangeActivate);
    }

    private void ChangeActivate(bool _selectTrigger)
    {
        if (_selectTrigger)
            actionSelect.Invoke();
        else
            actionDeSelect.Invoke();
    }
}