using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationLight : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private float endValue;
    [SerializeField] private float startValue;
    [SerializeField] private float animDuration;

    [Button]
    public void StartAnim()
    {
        light.DOIntensity(endValue, animDuration).OnComplete(DownIntensity);
    }

    public void UpIntensity()
    {
        light.DOIntensity(endValue, animDuration);
    }

    public void DownIntensity()
    {
        light.DOIntensity(startValue, animDuration);
    }
}