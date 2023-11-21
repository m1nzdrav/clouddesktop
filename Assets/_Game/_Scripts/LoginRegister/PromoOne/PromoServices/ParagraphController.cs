using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParagraphController : MonoBehaviour
{
    [SerializeField] private PromoOneInstantiate _promoOneInstantiate;

    [SerializeField] private Paragraph _orders = new Paragraph();

    private Coroutine _animation;

    public void OpenCloseParagraph(Paragraph childs, bool isOpen, bool stop, VerticalLayoutGroup verticalLayoutGroup)
    {
        if (childs.Paragraphs.Count == 0) return;

        if (_orders.Paragraphs.Count == 0)
        {
            _promoOneInstantiate.PromoStrAction.CheckNesting(GetComponent<PromoOneStrController>());
        }

        _orders = childs;

        if (stop)
        {
            StopAllCoroutines();
        }

        StartCoroutine(OpenAnimation(isOpen, verticalLayoutGroup));


        if (!isOpen)
        {
            foreach (var child in childs.Paragraphs)
            {
                // Debug.LogError(child.SaveText);
                OpenCloseParagraph(child.Nesting, isOpen, false, verticalLayoutGroup);

                var settingsForSixPage = child.GetComponent<SettingsForLoginScene>();

                if (settingsForSixPage != null)
                {
                    settingsForSixPage.OpenLock = false;
                }
            }
        }

        //var fromPos = _orders[0].transform.position;

        //var strTo = _orders[_orders.Count - 1];
        //var toPos = strTo.transform.position;
        //toPos.x = openLinePos.x;
    }

    IEnumerator OpenAnimation(bool isOpen, VerticalLayoutGroup verticalLayoutGroup)
    {
        float toScale;
        float strHeight;

        var subStr = _orders.Paragraphs[0];
        var subStrTransform = subStr.transform;

        if (!isOpen)
        {
            toScale = 0f;
        }
        else
        {
            toScale = 1f;
        }


        for (var i = 0; i < _orders.Paragraphs.Count; i++)
        {
            PromoOneStrController sub = _orders.Paragraphs[i];

            if (!isOpen)
            {
                strHeight = 0f;
            }
            else
            {
                strHeight = sub.StrHeight;
            }

            if (sub.IconContainer != null)
            {
                var openIconContainer = sub.IconContainer;
                if (openIconContainer != null)
                {
                    openIconContainer.Open(sub.StrHeight, isOpen);
                }
            }

            if (sub.DropdownCode != null)
            {
                sub.DropdownCode.SetWidth();

                continue;
            }

            if (sub.InputText != null)
            {
                sub.InputText.ShowHideInput();

                continue;
            }

            if (sub.SaveText == "Скоро")
            {
                sub.SettingsForLoginScene.ShowLine();
                sub.Active = false;
            }

            StartCoroutine(ShowSubString(sub, toScale, strHeight, verticalLayoutGroup));
        }


        yield return null;
    }

    public IEnumerator ShowSubString(PromoOneStrController subStr, float to, float strHeight,
        VerticalLayoutGroup verticalLayoutGroup)
    {
        var time = 0f;
        const float period = 2f;
        var subRT = subStr.Rect;
        var scale = subStr.transform.localScale;
        var sizeDelta = subRT.sizeDelta;

        if (subStr.PromoLinks.Text != null)
        {
            subStr.PromoLinks.Text.text = subStr.SaveText; //todo не уводить в "" строку ранее
            subStr.Dot.localScale = Vector3.one;
        }

        while (time < period)
        {
            time += Time.deltaTime;
            var lTime = time / period;

            var lValueScale = Mathf.Lerp(scale.y, to, lTime);
            var lValueSizeDelta = Mathf.Lerp(sizeDelta.y, strHeight, lTime);

            sizeDelta.y = lValueSizeDelta;
            subRT.sizeDelta = sizeDelta;

            scale.y = lValueScale;
            subStr.transform.localScale = scale;

            verticalLayoutGroup.SetLayoutVertical();

            yield return null;
        }
    }
}