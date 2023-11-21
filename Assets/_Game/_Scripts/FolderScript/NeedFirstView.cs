using System;
using System.Collections;
using System.Collections.Generic;
using Project;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NeedFirstView : MonoBehaviour, IBeginDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private MaskDragArea _maskDragArea;
    private FlexibleDraggableObject _flexibleDraggableObject;
    [SerializeField] private List<Transform> _objectOnFirstView;
    [SerializeField] private bool _needMask=true;
    [SerializeField] private bool _needFirstView = true;




    public List<Transform> ObjectOnFirstView
    {
        get => _objectOnFirstView;
        set => _objectOnFirstView = value;
    }

    public bool NeedMask
    {
        get => _needMask;
        set => _needMask = value;
    }

    private void Awake()
    {
        
        // _maskDragArea = FindObjectOfType<MaskDragArea>();
        _flexibleDraggableObject = GetComponent<FlexibleDraggableObject>();
        if (_objectOnFirstView == null)
            _objectOnFirstView .Add(_flexibleDraggableObject.Target.transform); 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       
        if (_flexibleDraggableObject!=null&& _flexibleDraggableObject.CanDrag&& _needMask)
        {
//            _maskDragArea.OnMask();
        }

        if (_needFirstView)
        {
            foreach (var VARIABLE in _objectOnFirstView)
            {
                try
                {
                    VARIABLE.SetAsLastSibling();
                }
                catch (Exception e)
                {
 
                }
                
            }
            
        }
    }




    public void OnPointerDown(PointerEventData eventData)
    {
        if (_needFirstView)
        {
            foreach (var VARIABLE in _objectOnFirstView)
            {
                try
                {
                    VARIABLE.SetAsLastSibling();
                }
                catch (Exception e)
                {

                }
                
            }
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        try
        {
//            _maskDragArea.OffMask();
        }
        catch (Exception e)
        {
            Debug.LogError("Нет "+e);
        }
        
    }
} 