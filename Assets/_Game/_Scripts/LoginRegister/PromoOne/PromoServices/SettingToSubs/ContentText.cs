using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ContentText
{
    public void SetTextSettings(PromoOneStrController item, Content subOnLoad, bool _isArabic)
    {
        FillText(subOnLoad, item.PromoLinks, _isArabic, true);

        if (subOnLoad.text != "") item.SaveText = subOnLoad.text;

        item.gameObject.name = subOnLoad.text;
        item.StrHeight = subOnLoad.size;

        Color color = new Color(subOnLoad.color.r, subOnLoad.color.g, subOnLoad.color.b, item.LampImage.color.a);
        item.LampImage.color = color;
        item.DotImage.color = color;
        item.Line.color = color;

        if (!subOnLoad.sword)
        {
            item.Line.gameObject.SetActive(false);
            item.WhiteSword.gameObject.SetActive(false);
        }

        if (!_isArabic) return;

        //dot
        var dotRect = item.Dot.GetComponent<RectTransform>();
        var anchoredPosition = dotRect.anchoredPosition;
        anchoredPosition.x *= -1;
        dotRect.pivot = new Vector2(0f, 0.5f);
        dotRect.anchorMin = new Vector2(1f, 0.5f);
        dotRect.anchorMax = new Vector2(1f, 0.5f);
        dotRect.anchoredPosition = anchoredPosition;


        //lamp
        var lampRect = item.Lamp.GetComponent<RectTransform>();
        anchoredPosition = lampRect.anchoredPosition;
        anchoredPosition.x *= -1;
        anchoredPosition.x /= 2;
        lampRect.pivot = new Vector2(0f, 0.5f);
        lampRect.anchorMin = new Vector2(1f, 0.5f);
        lampRect.anchorMax = new Vector2(1f, 0.5f);
        lampRect.anchoredPosition = anchoredPosition;
    }

    private void FillText(Content subOnLoad, PromoLinks str, bool isArabic, bool needSetText)
    {
        str.Text.font = (RegisterSingleton._instance.GetSingleton(typeof(FontSingleton)) as FontSingleton)?.GetFont(subOnLoad.font);
        str.Text.text = needSetText ? subOnLoad.text : "";
        str.Text.GetComponent<RectTransform>().sizeDelta =
            new Vector2(1000, subOnLoad.sizeIsHeight ? subOnLoad.size : subOnLoad.height);
        str.Text.fontSize = subOnLoad.size;

        str.Text.color = new Color(subOnLoad.color.r, subOnLoad.color.g, subOnLoad.color.b, 0);
        str.Alpha = subOnLoad.color.a;
        str.Text.alignment = TextAnchor.MiddleLeft;

        str.Text.alignment = isArabic ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;
    }
}