using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game._Scripts.Button
{
    public class ActivateObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject gameObject;
        [SerializeField] private bool needAction = true;


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (needAction) gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (needAction) gameObject.SetActive(false);
        }

        public void SetTriggerAction(bool trigger)
        {
            needAction = trigger;
        }
    }
}
