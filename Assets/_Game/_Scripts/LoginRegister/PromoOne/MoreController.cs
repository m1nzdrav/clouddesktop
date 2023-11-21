using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoreController : MonoBehaviour, IPointerClickHandler
{
    public Paragraph Subs;

    //public MoreController NextMore;
    public Text Text;

    private bool _active = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_active)
        {
            MoreСlick();
        }
    }

    public void SetActivity(bool activity)
    {
        _active = activity;
    }

    public void MoreСlick()
    {
        transform.localScale = Vector3.zero;

        //StartCoroutine(GoUpStr());
        StartCoroutine(ShowText());
    }

    public void SetDefaultParameters()
    {
        var getSettings = GetSettings.Instance;
        var scrollRect = getSettings.ScrollRect;
        var verticalLayoutGroup = getSettings.VerticalLayoutGroup;

        verticalLayoutGroup.enabled = true;
        scrollRect.enabled = true;
    }

    //IEnumerator GoUpStr()
    //{
    //    var getSettings = GetSettings.Instance;
    //    var scrollRect = getSettings.ScrollRect;
    //    var rectTransform = getSettings.RectTransform;
    //    var verticalLayoutGroup = getSettings.VerticalLayoutGroup;

    //    verticalLayoutGroup.enabled = false;
    //    scrollRect.enabled = false;

    //    var pivot = rectTransform.pivot;
    //    var pos = rectTransform.anchoredPosition;
    //    rectTransform.pivot = new Vector2(pivot.x, 1f);
    //    rectTransform.anchoredPosition = new Vector2(0, pos.y / 2);


    //    var time = 0f;
    //    const float period = 0.5f;

    //    var startPos = rectTransform.anchoredPosition;
    //    var endPos = Vector2.zero;

    //    while (time < period)
    //    {
    //        time += Time.deltaTime;

    //        var lTime = time / period;

    //        var lVector = Vector2.Lerp(startPos, endPos, lTime);
    //        rectTransform.anchoredPosition = lVector;

    //        yield return null;
    //    }

    //    StartCoroutine(ShowText());

    //    yield return new WaitForSeconds(1f);

    //    //verticalLayoutGroup.enabled = true;
    //    scrollRect.enabled = true;
    //}

    IEnumerator ShowText()
    {
        var counter = Subs.Paragraphs.Count - 1;

        for (int i = 0; i < counter; i++) //todo запихнуть в StartMoreAnimation?
        {
            var item = Subs.Paragraphs[i];

            //item.SrtActiveFalse();
            item.GetComponent<Animation>().Play("TextAnim"); //todo прокинуть
            item.gameObject.SetActive(true);

            if (item.PromoLinks.Text.text == "") continue;

            item.Dot.localScale = Vector3.zero;
            item.PromoLinks.Text.text = "";

            item.Dot.gameObject.SetActive(true);
        }

        Subs.Paragraphs[counter + 1].gameObject.SetActive(false);

        // DotsController.Instance.StartMoreAnimation(Subs);
        //NextMore.gameObject.SetActive(true);
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;

        yield return null;
    }
}