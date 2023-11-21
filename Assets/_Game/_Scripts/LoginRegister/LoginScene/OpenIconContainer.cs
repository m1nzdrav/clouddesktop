using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class OpenIconContainer : MonoBehaviour
{
    // [SerializeField] private CanvasGroup _canvasGroup;
    // [SerializeField] private RectTransform _rect;
    [SerializeField] private float animationDuration;

    // [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;

    [SerializeField] private List<ShowMainIcon> listIcon;
    private float _height;
    private float _width;

    public void SetSettings()
    {
        var rect = GetComponent<RectTransform>().rect;

        if (_height == 0)
        {
            _height = rect.height;
            _width = rect.width;
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(_width, 0f);
    }

    public void Close()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(_width, 0f);

        GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void Open(float height, bool isOpen)
    {
        if (!isOpen)
        {
            StartCoroutine(Animation(height, 0f, 0f));


            //_canvasGroup.DOFade(0, animationDuration);
        }
        else
        {
            //_rect.sizeDelta = new Vector2(_width, _height);
            StartCoroutine(Animation(0f, height, 1f));
        }
    }

    IEnumerator Animation(float from, float to, float toFade)
    {
        if (toFade == 0f)
        {
            GetComponent<CanvasGroup>().DOFade(toFade, animationDuration);
            //transform.DOScaleY(toFade, animationDuration);
        }

        var time = 0f;
        var period = animationDuration;
        var scale = transform.localScale;


        while (time < period)
        {
            time += Time.deltaTime;
            var lTime = time / period;

            var lValue = Mathf.Lerp(from, to, lTime);
            var lValueScale = Mathf.Lerp(scale.y, toFade, lTime);

            GetComponent<RectTransform>().sizeDelta = new Vector2(_width, lValue);

            scale.y = lValueScale;
            transform.localScale = scale;

            GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
            GetComponent<VerticalLayoutGroup>().SetLayoutVertical();

            yield return null;
        }

        if (toFade == 1f)
        {
            GetComponent<CanvasGroup>().DOFade(toFade, animationDuration);
            //transform.DOScaleY(toFade, animationDuration);
        }
    }

    public void SetTextIcons(string text)
    {
        listIcon.ForEach(x => x.SetSoonText(text));
    }
}