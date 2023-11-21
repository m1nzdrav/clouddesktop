using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


    public class MoveAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 positionToMove;
        [SerializeField] private float moveTime = 1f;

        [Button]
        public void StatMove()
        {
            transform.DOLocalMove(positionToMove, moveTime);
        }
    }