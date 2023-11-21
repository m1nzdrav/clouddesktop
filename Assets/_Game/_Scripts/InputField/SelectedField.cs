using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectedField : MonoBehaviour, IDeselectHandler
    , IPointerClickHandler
{
    [SerializeField] private InputField _inputField;

    [SerializeField] private bool _isSelected;
    [SerializeField] private Color _color;
    private Color _currentColor;


    public void OnDeselect(BaseEventData eventData)
    {
        if (!_inputField.enabled)
        {
            _isSelected = false;
            _inputField.enabled = false;
            SetCurrentColor();
        }
        else
        
            _isSelected = false;
        


    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isSelected)
        {
            _inputField.enabled = true;
            _inputField.OnPointerDown(eventData);
        }
        else
        {
            _isSelected = true;
            _currentColor = GetComponent<Image>().color;
            GetComponent<Image>().color = _color;
        }
    }

    public void SetCurrentColor()
    {
        GetComponent<Image>().color = _currentColor;
    }
}