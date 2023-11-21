using UnityEngine;
using UnityEngine.EventSystems;

public class ParentSetter : MonoBehaviour,IPointerEnterHandler
{
    [SerializeField] private Transform areaParent;
    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
