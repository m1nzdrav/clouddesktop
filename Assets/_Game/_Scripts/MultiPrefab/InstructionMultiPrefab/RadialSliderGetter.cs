
using UnityEngine;

public class RadialSliderGetter : MonoBehaviour
{
    [SerializeField] private RadialSliderSender _radialSliderSender;
    
    public void SetListener(RadialSliderSender sender)
    {
        _radialSliderSender = sender;

    }

    public void UpdateRadialSlider()
    {
        _radialSliderSender.UpdateTime();
    }
    
}