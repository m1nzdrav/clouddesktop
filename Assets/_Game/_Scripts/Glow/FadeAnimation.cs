using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] private Image glow;
    [SerializeField] private float maxFade = 1;
    [SerializeField] private float minFade = 0;
    [SerializeField] private float timeToFade = .5f;
    [SerializeField] private bool isFade=true;


    public void ActivateGlow()
    {
        glow.DOFade(maxFade, timeToFade);
    }

    public void DeActivateGlow()
    {
        glow.DOFade(minFade, timeToFade);
    }

    public void ChangeState()
    {
        if (isFade)
        {
            ActivateGlow();
            isFade = false;
        }
        else
        {
            DeActivateGlow();
            isFade = true;
        }
    }
}