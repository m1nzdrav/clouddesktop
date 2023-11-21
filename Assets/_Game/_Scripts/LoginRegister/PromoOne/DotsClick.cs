
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class DotsClick : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField] private bool isOpen = false;
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }