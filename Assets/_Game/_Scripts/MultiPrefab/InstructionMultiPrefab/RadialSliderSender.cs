using System;
using Michsky.UI.ModernUIPack;
using UnityEngine;


public class RadialSliderSender : MonoBehaviour
{
    [SerializeField] private RadialSlider _radialSlider;
    [SerializeField] private MultiPrefabInstruction _multiPrefabInstruction;
    [SerializeField] private RadialSliderGetter _getter;

    private void Awake()
    {
        _getter = _radialSlider.GetComponent<RadialSliderGetter>();
    }


    public void SetRadial()
    {
        _getter.SetListener(this);
        _radialSlider.maxValue = _multiPrefabInstruction.MAXTime;
    }

    public void UpdateRadialValue()
    {
        // SetRadial();
        _radialSlider.SliderValue = _multiPrefabInstruction.CurrentTime;
        _radialSlider.UpdateUI();
    }

    public void UpdateTime()
    {
        _multiPrefabInstruction.SetTimer(_radialSlider.currentValue);
    }
}