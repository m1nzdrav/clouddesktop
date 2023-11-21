using DG.Tweening;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class RadialVolumeSlider : MonoBehaviour
{
    [SerializeField] private RadialSlider _radialSlider;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float animationFade = .5f;

    public void UpdateVolumeSlider()
    {
        // canvasGroup.DOFade(1, animationFade).OnComplete(() =>
        // {
            _radialSlider.SliderValue = AudioListener.volume;
            _radialSlider.UpdateUI();
        //     canvasGroup.DOFade(0, animationFade);
        // });
    }
}