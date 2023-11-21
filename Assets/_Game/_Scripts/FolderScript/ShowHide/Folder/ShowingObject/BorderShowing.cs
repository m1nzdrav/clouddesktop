using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game._Scripts.FolderScript.ShowHide.Folder.ShowingObject
{
    public class BorderShowing: MonoBehaviour,IPointerDownHandler,IPointerUpHandler

    {
        [SerializeField] private ShowOn[] showingObject;
        [SerializeField] private EventTrigger myEventTrigger;

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var VARIABLE in showingObject)
            {
                VARIABLE?.Enter(myEventTrigger);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (var VARIABLE in showingObject)
            {
                VARIABLE?.Exit(myEventTrigger);
            }
        }
    }
}