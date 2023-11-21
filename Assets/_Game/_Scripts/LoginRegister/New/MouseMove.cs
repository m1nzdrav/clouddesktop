using UnityEngine;
using UnityEngine.EventSystems;

public class MouseMove : MonoBehaviour, IDragHandler
{
    [SerializeField]
    [Range(1, 100)]
    private int _speed;

    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var delta = transform.position - mousePos;
        transform.Translate(eventData.delta / (100.1f - _speed));
    }
}
