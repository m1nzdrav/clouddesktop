using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ObjectScaleAnimation : MonoBehaviour
{
    [SerializeField] private Transform scaledObject;
    [SerializeField] private float timeScale = 1f;

    [SerializeField] private UnityEvent _event;

    [Button]
    public void SetAlpha(float _endScale)
    {
        scaledObject.DOScale(_endScale, timeScale).OnComplete(() => _event?.Invoke());
    }
    public void SetAlphaVector(Vector3 _endScale)
    {
        scaledObject.DOScale(_endScale, timeScale).OnComplete(() => _event?.Invoke());
    }
}