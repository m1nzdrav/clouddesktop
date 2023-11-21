using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace _Game._Scripts.LoginRegister.PromoOne.AnimationSystem.AnimationType
{
    public class AnimationShakeCircle : MonoBehaviour
    {
        [SerializeField] private float strength;
        [SerializeField] private int vibrato;

        [Button]
        public void Animate(float time)
        {
            GetComponentsInChildren<Transform>().ForEach(x => x.DOShakeScale(time, strength, vibrato));
        }

        [Button]
        public void AnimatePunch(float time)
        {
            GetComponentsInChildren<Transform>().ForEach(x => x.DOPunchScale(Vector3.up, time, vibrato, strength));
        }

        [Button]
        public void AnimateJump(float time)
        {
            GetComponentsInChildren<Transform>().ForEach(x =>
                x.DOLocalJump(x.transform.localPosition + Vector3.up, strength, vibrato, time));
        }
    }
}