using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game._Scripts.Button.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ClickTrigger : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioSource source;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (click != null)
            {
                source.clip = click;
                source.Play();
            }
        }
    }
}