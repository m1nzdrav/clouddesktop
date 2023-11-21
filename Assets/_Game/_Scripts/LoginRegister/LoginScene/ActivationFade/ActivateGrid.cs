
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class ActivateGrid : MonoBehaviour
    {
        [SerializeField] private Vector3 positionStart;
        [SerializeField] private float animationDuration;
        [SerializeField,ReadOnly] private Vector3 currentPosition;

        [Button]
        public void AnimationMove()
        {
            currentPosition = transform.localPosition;
            transform.localPosition = positionStart;
            transform.DOLocalMove(currentPosition, animationDuration);
        }
        public void ReturnToStartPosition()
        {
            transform.DOLocalMove(positionStart, animationDuration).OnComplete((() => transform.localPosition=currentPosition));
        }

        
    }