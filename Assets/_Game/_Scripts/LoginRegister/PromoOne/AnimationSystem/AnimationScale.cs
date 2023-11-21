using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.LoginRegister.PromoOne.AnimationSystem.AnimationType
{
    public class AnimationScale : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private RectTransform _parentTarget;
        [SerializeField] private ContentSizeFitter _contentSizeFitter;
        [SerializeField] private float _timeToScale = .3f;
        [SerializeField] private bool _flagChangeUp;

        public void ScaleUp()
        {
            _target.DOScale(1, _timeToScale).OnUpdate( Rebuild );
            _flagChangeUp = true;
        }

        private void Rebuild()
        {
            if (_parentTarget != null) _contentSizeFitter.SetLayoutVertical();
        }

        public void ScaleDown()
        {
            _target.DOScale(0, _timeToScale).OnUpdate(Rebuild);
            _flagChangeUp = false;
        }

        public void ChangeScale()
        {
            if (_flagChangeUp)
            {
                ScaleDown();
            }
            else
            {
                ScaleUp();
            }
        }
    }
}