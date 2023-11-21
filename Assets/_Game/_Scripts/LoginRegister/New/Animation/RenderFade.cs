using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace _Game._Scripts.LoginRegister.New.Animation
{
    public class RenderFade : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float timeFade = 1f;
        [SerializeField] private float FastTimeFade = 0f;
        [SerializeField] private UnityEvent _event;

        [Button]
        public void SetAlpha(float alpha)
        {
            _renderer.material.DOFade(alpha, timeFade).OnComplete(() => _event?.Invoke());
        }

        public Tween Test(float alpha)
        {
            return  _renderer.material.DOFade(alpha, timeFade).OnComplete(() => _event?.Invoke());
        }
        [Button]
        public void FastSetAlpha(float alpha)
        {
            _renderer.material.DOFade(alpha, FastTimeFade);
        }
    }
}