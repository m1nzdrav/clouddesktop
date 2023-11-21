using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ChangeTexture : MonoBehaviour
{
        [SerializeField] private Texture active, passive;
        [SerializeField] private Graphic point;

        public void ChangeToActive()
        {
                // point. = active;
        }

}