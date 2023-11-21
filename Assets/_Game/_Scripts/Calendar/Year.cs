using _Game._Scripts;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class Year : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color baseColor;
    [SerializeField] private Color mainColor;
    [SerializeField] private Text text;
    [SerializeField] private float animationAppearTime = .0015f;

    [Button]
    public TweenerCore<Vector3, Vector3, VectorOptions> SetSelected(int year)
    {
        // text.text = year.ToString();
        return image.transform.DOScale(Vector3.zero, animationAppearTime)
            .OnPlay(() => text.DOText(year.ToString(), animationAppearTime))
            .OnComplete(() =>
            {
                image.color = baseColor;
                image.transform.DOScale(Vector3.one, animationAppearTime);
            });
    }

    public TweenerCore<string, string, StringOptions> SetOnlyText(int year)
    {
        return text.DOText(year.ToString(), animationAppearTime);
    }

    [Button]
    public TweenerCore<Vector3, Vector3, VectorOptions> DeSelect()
    {
        return image.transform.DOScale(Vector3.zero, animationAppearTime)
            .OnComplete(() =>
            {
                image.color = mainColor;
                image.transform.DOScale(Vector3.one, animationAppearTime);
            });
    }
}