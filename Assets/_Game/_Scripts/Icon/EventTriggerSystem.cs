using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventTriggerSystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Enter/Exit")] 
    [SerializeField] private IconFullController _iconFullController;
    [SerializeField] private IconPanel _iconPanel;

    [Header("Drag")] 
    [SerializeField] private IconModel _iconModel;

    [Header("Click")] 
    [SerializeField] protected IconGlowController _iconGlowController;

    
    public UnityEvent ActionEnter;
    public UnityEvent ActionExit;
    public UnityEvent ActionClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _iconFullController?.EnterPointer();
        _iconPanel?.PointerEnterIcon();

        ActionEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _iconFullController?.ExitPointer();
        _iconPanel?.PointerExitIcon();

        ActionExit?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //_iconGlowController.SetActiveGlow();

        ActionClick?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _iconModel?.SetJson();
    }
}
