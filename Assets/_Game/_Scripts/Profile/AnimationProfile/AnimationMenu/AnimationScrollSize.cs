using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationScrollSize : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private bool canScroll;
    [SerializeField] private Vector2 mouseScroll;
    [SerializeField] private float time;
    public Action<float> eventScroll;
    [SerializeField] private Vector3 basePosition;

    private void Start()
    {
        basePosition = rectTransform.localPosition;
        eventScroll += AnimateStep;
    }

    public float Time
    {
        get => time;
        set { time = value; }
    }

    private void Update()
    {
        if (!canScroll || Input.mouseScrollDelta.y == 0) return;

        mouseScroll = Input.mouseScrollDelta;
        time = (time + mouseScroll.y / 100);
        Debug.LogError(mouseScroll.y);
        eventScroll(mouseScroll.y / 10);
    }

    private void AnimateStep(float newStep)
    {
        Vector3 scale = new Vector3(newStep, newStep, 0);
        rectTransform.localScale += scale;
        Vector3 temp =new Vector3(Screen.width/2 - Input.mousePosition.x,Screen.height/2 - Input.mousePosition.y) ;

        
        Debug.LogError(rectTransform.localPosition + " " +
                       "" + basePosition +
                       "" + Vector3.Lerp(rectTransform.localPosition, temp, newStep));
        
        if (newStep > 0)
            rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, temp, newStep);
        else
            rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, basePosition, -newStep);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canScroll = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canScroll = false;
    }
}