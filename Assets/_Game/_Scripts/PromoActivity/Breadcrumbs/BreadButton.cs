using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.PromoActivity
{
    public class BreadButton : MonoBehaviour
    {
        [SerializeField] private int countBread;

        public int CountBread => countBread;

        [SerializeField] private Image _imageGlow;
        [SerializeField] private float timeToAnimation = .5f;
        [SerializeField] private Color colorActivate;
         [SerializeField] private Color colorReActivate;
        

        public void Activate()
        {
            _imageGlow.DOColor(colorActivate, timeToAnimation);
        }
        public void ReActivate()
        {
            _imageGlow.DOColor(colorReActivate, timeToAnimation);
        }
    }
}