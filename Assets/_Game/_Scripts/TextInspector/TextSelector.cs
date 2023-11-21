using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextSelector : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Re_InputField _inputField;

    public void OnPointerDown(PointerEventData eventData)
    {
        //TextInspector.SelectText(TextForSelection);
        TextInspector.SelectField(_inputField);
    }
}

