using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeFolderProperties : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ActiveFolderAnimation _folderAnimation;

    public void OnEndDrag(PointerEventData eventData)
    {
        _folderAnimation.StartAnimation();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _folderAnimation.StopAnimation();
    }
}
