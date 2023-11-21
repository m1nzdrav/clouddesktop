using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.FolderScript.GridButton
{
    public class ChangerSprite : MonoBehaviour
    {
        [SerializeField] private Image goalSprite;
        [SerializeField] private bool isLocked;

        public bool IsLocked
        {
            set
            {
                isLocked = value;
                if (!value) ChangeWhite();
            }
        }

        [Header("White and Colored sprite")] [SerializeField]
        private WhiteAndColored buttonSprite;


        public void ChangeColored()
        {
            if (!isLocked)
            {
                goalSprite.sprite = buttonSprite.colored;
            }
        }

        public void ChangeWhite()
        {
            if (!isLocked)
            {
                goalSprite.sprite = buttonSprite.white;
            }
        }

        public void SetColored(Sprite sprite)
        {
            buttonSprite.colored = sprite;
        }

        public void SetWhite(Sprite sprite)
        {
            buttonSprite.white = sprite;
        }
    }
}