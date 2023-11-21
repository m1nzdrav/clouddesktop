using UnityEngine;
using UnityEngine.EventSystems;

namespace _Game._Scripts.Button.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
        , IPointerClickHandler
    {
        // [SerializeField] private ClipContainer clip;
        [SerializeField] private AudioClip enter;
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioSource source;
        [SerializeField] private bool needHover = true;

        public bool NeedHover
        {
            get => needHover;
            set => needHover = value;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!needHover || enter == null || source == null)
                return;

            source.clip = enter;
            source.Play();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (click == null || source == null) return;

            source.clip = click;
            source.Play();
        }
    }
}