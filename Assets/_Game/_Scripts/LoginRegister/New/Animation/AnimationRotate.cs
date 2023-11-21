using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationRotate : MonoBehaviour
{
    [SerializeField] private Vector3 positionToRotate;
    [SerializeField] private float rotateTime = 1f;

    [Button]
    public void StatRotate()
    {
        transform.DOLocalRotate(positionToRotate, rotateTime);
    }
}