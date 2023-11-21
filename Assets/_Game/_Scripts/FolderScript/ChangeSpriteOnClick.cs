using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeSpriteOnClick : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Sprite _pressed;
    [SerializeField] private bool _isClicked;
    [SerializeField] private Image _image;

    [SerializeField] private Sprite _nonPressed;

 

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked)
        {
            _isClicked = false;
            _image.sprite = _nonPressed;
        }
        else
        {
            _isClicked = true;
            _image.sprite = _pressed;
        }
    }
}
