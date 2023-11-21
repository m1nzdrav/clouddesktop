using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SearchNoAcces : MonoBehaviour,ISelectHandler,IDeselectHandler
{

    [SerializeField] private Text _noAccesMessage;
    [SerializeField] private Text _normalMessage;
    [SerializeField] private float _animDuration=.5f;
    private Vector3 _accesRotate=new Vector3(0,90,0);
    
    
    

    public void OnSelect(BaseEventData eventData)
    {
        _noAccesMessage.gameObject.SetActive(true);
        
        _normalMessage.transform.DORotate(_accesRotate, _animDuration);
        _noAccesMessage.transform.DORotate(Vector3.zero, _animDuration);
        
        _normalMessage.gameObject.SetActive(false);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _normalMessage.gameObject.SetActive(true);
        
        _noAccesMessage.transform.DORotate(_accesRotate, _animDuration);
        _normalMessage.transform.DORotate(Vector3.zero, _animDuration);
        
        _noAccesMessage.gameObject.SetActive(false);
    }
}
