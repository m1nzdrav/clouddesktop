using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class AnimationBreathImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float animBreath = 1;

    public void StartAnimation()
    {
        if (!_image.gameObject.activeSelf) _image.gameObject.SetActive(true);

        _image.DOFade(1, animBreath).OnComplete(() => { _image.DOFade(0, animBreath); });
    }
}