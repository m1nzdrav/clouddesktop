using DG.Tweening;
using UnityEngine;

public class RotatePrefab : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float timeAnimation;

    public void Rotate(float angle)
    {
        target.DORotate(new Vector3(0, angle, 0), timeAnimation);
    }
}