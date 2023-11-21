using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationGlow : MonoBehaviour
{
    [SerializeField] private Image glow;
    [SerializeField] private float animationDuration=1;

    public void AnimGlow()
    {
        bool temp = glow.gameObject.activeSelf;
        glow.gameObject.SetActive(true);
        glow.DOFade(1, animationDuration).OnComplete(() =>
        {
            glow.DOFade(0, animationDuration);
            glow.gameObject.SetActive(temp);
        });
    }
}