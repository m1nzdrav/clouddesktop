using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public enum HandlerType
{
    TopRight,
    Right,
    BottomRight,
    Bottom,
    BottomLeft,
    Left,
    TopLeft,
    Top
}

[RequireComponent(typeof(EventTrigger))]
public class FlexibleResizeHandler : MonoBehaviour
{
    public HandlerType Type;
    public RectTransform Target;
    [SerializeField] private float  _minWidth=600;
    [SerializeField] private float  _minHight=150;
    private EventTrigger _eventTrigger;

    void Start()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.AddEventTrigger(OnDrag, EventTriggerType.Drag);
    }

    void OnDrag(BaseEventData data)
    {
        PointerEventData ped = (PointerEventData) data;
        Debug.Log("Max " + Target.offsetMax);
        Debug.Log("min " + Target.offsetMin);
        Debug.Log("ped "+ ped.delta);
        

        switch (Type)
        {
            case HandlerType.TopRight:

                if (Math.Abs(Target.offsetMax.y- Target.offsetMin.y) > _minHight || ped.delta.y>0)
                    Target.offsetMax = new Vector2(Target.offsetMax.x, Target.offsetMax.y + ped.delta.y);

                if (Math.Abs(Target.offsetMax.x - Target.offsetMin.x) >  _minWidth|| ped.delta.x>0)
                    Target.offsetMax = new Vector2(Target.offsetMax.x + ped.delta.x, Target.offsetMax.y);

                break;
            case HandlerType.Right:

                if (Math.Abs(Target.offsetMax.x - Target.offsetMin.x) >  _minWidth|| ped.delta.x>0)
                    Target.offsetMax = new Vector2(Target.offsetMax.x + ped.delta.x, Target.offsetMax.y);

                break;
            case HandlerType.BottomRight:

                if (Math.Abs(Target.offsetMax.y - Target.offsetMin.y) > _minHight || ped.delta.y<0)
                    Target.offsetMin = new Vector2(Target.offsetMin.x, Target.offsetMin.y + ped.delta.y);

                if (Math.Abs(Target.offsetMax.x - Target.offsetMin.x) >  _minWidth|| ped.delta.x>0)
                    Target.offsetMax = new Vector2(Target.offsetMax.x + ped.delta.x, Target.offsetMax.y);
                break;
            case HandlerType.Bottom:

                if (Math.Abs(Target.offsetMax.y - Target.offsetMin.y) > _minHight || ped.delta.y<0)
                    Target.offsetMin = new Vector2(Target.offsetMin.x, Target.offsetMin.y + ped.delta.y);
                break;
            case HandlerType.BottomLeft:

                if (Math.Abs(Target.offsetMax.y - Target.offsetMin.y) > _minHight || ped.delta.y<0)
                    Target.offsetMin = new Vector2(Target.offsetMin.x, Target.offsetMin.y + ped.delta.y);

                if (Math.Abs(Target.offsetMax.x - Target.offsetMin.x) >  _minWidth|| ped.delta.x<0)
                    Target.offsetMin = new Vector2(Target.offsetMin.x + ped.delta.x, Target.offsetMin.y);

                break;
            case HandlerType.Left:

                if (Math.Abs(Target.offsetMax.x - Target.offsetMin.x) >  _minWidth|| ped.delta.x<0)
                    Target.offsetMin = new Vector2(Target.offsetMin.x + ped.delta.x, Target.offsetMin.y);
                break;
            case HandlerType.TopLeft:

                if (Math.Abs(Target.offsetMax.y - Target.offsetMin.y) > _minHight || ped.delta.y>0)
                    Target.offsetMax = new Vector2(Target.offsetMax.x, Target.offsetMax.y + ped.delta.y);

                if (Math.Abs(Target.offsetMax.x - Target.offsetMin.x) >  _minWidth|| ped.delta.x<0)
                    Target.offsetMin = new Vector2(Target.offsetMin.x + ped.delta.x, Target.offsetMin.y);

                break;
            case HandlerType.Top:

                if (Math.Abs(Target.offsetMax.y - Target.offsetMin.y) > _minHight || ped.delta.y>0)
                    Target.offsetMax = new Vector2(Target.offsetMax.x, Target.offsetMax.y + ped.delta.y);

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}