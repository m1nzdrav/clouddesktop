using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeParentIfDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ParentRegister parentRegister;
    [SerializeField] private Transform globalParent;
    private Transform _oldParent;


    public void OnBeginDrag(PointerEventData eventData)
    {
//        GetComponent<Image>().raycastTarget = false;
//        _oldParent = transform.parent;
//        transform.SetParent(globalParent);
//
//        // отключение интерактивности канваса
//        _oldParent.GetComponent<CanvasGroup>().blocksRaycasts = false;
//
//        GetComponent<Button>().interactable = false;
//        ;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag ");
//        _panelRegister.DraggbleFolder(transform);
//        GetComponent<Image>().raycastTarget = true;
//        //включение интерактивности канваса
////        Target.GetComponent<Transform>().parent.gameObject.GetComponent<CanvasGroup>().interactable = true;
//        _oldParent.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
//
//        GetComponent<Button>().interactable = true;
    }
}