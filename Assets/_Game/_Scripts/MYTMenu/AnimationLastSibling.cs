using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AnimationLastSibling : MonoBehaviour
{
    [SerializeField] private CanvasGroup currentCanvas;
    [SerializeField] private float zeroFadeTime = .1f;
    [SerializeField] private float oneFadeTime = .5f;

    public void SetLastSibling()
    {
        currentCanvas.DOFade(0, zeroFadeTime).OnComplete(() =>
        {
            transform.SetAsLastSibling();
            currentCanvas.DOFade(1, oneFadeTime);
        });
    }
}