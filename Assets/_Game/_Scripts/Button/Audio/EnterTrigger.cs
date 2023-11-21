using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game._Scripts.Button.Audio
{
    
    [RequireComponent(typeof(AudioSource))]
    public class EnterTrigger : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private AudioClip enter;
        [SerializeField] private AudioSource source;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enter == null)
                return;

            source.clip = enter;
            source.Play();
        }
    }
}