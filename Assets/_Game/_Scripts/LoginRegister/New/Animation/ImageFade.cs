using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ImageFade : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float timeFade = 1f;

    [SerializeField] private UnityEvent _event;

    [Button]
    public void SetAlpha(float alpha)
    {
        _image.DOFade(alpha, timeFade).OnComplete(() => _event?.Invoke());
    }
}