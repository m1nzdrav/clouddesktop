using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.Button.Continent
{
    public class ChangeSpriteStock : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private List<Sprite> stockSprites;
        [SerializeField] private int count = 0;

        public int Count
        {
            get
            {
                count++;

                if (count >= stockSprites.Count)
                {
                    count = 0;
                }

                return count;
            }
        }

        public void ChangeSpriteButton()
        {
            image.sprite = stockSprites[Count];
        }
    }
}