using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextPageController : MonoBehaviour, IPointerClickHandler
{
    public static PreviousPageController PrevPageController;

    public PromoOneInstantiate promoOneInstantiate;
    public int[] Subs;
    public int PrevSubs;
    public NextPageController PrevButton;
    public NextPageController NextButton;
    public Text Text;

    private bool _active = true;

    public event Action Listener;

    private void OnDestroy()
    {
        if (Listener != null)
        {
            Listener = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_active)
        {
            ButtonClick();
        }

        Listener?.Invoke();
    }

    public void SetActivity(bool activity)
    {
        _active = activity;
    }

    public void ButtonClick()
    {
        HideText(PrevSubs);


        ShowText(Subs);

        //NextButton?.gameObject.SetActive(true);
        //gameObject.SetActive(false);

        //PrevPageController.PrevButton = this;
        //PrevPageController.Button = NextButton;

        PrevPageController.Button = this;
    }

    public void HideText(int numberPage)
    {
        Paragraph text = promoOneInstantiate.GetParagraph(numberPage);
        var count = text.Paragraphs.Count;

        for (int i = 0; i < count; i++)
        {
            PromoOneStrController sub = text.Paragraphs[i];
            sub.gameObject.SetActive(false);
            //sub.SrtActiveFalse();
        }
    }

    public void ShowText(int[] numberPage)
    {
        Paragraphs paragraphs = new Paragraphs();
        foreach (var VARIABLE in numberPage)
        {
            Paragraph text = promoOneInstantiate.GetParagraph(VARIABLE - 1);
            var count = text.Paragraphs.Count - 1; //todo запихнуть в StartNextPageAnimation?

            for (int i = 0; i < count; i++)
            {
                PromoOneStrController sub = text.Paragraphs[i];
                sub.gameObject.SetActive(true);
                //sub.SrtActiveFalse();

                if (sub.SpecialSettings)
                {
                    // sub.SettingsForLoginScene.SetSettingsForOpen(sub);
                }

                sub.Dot.localScale = Vector3.zero;
                sub.PromoLinks.Text.text = "";
            }

            text.Paragraphs[count].transform.localScale = Vector3.zero;
            paragraphs.Pages.Add(text);
        }


        // DotsController.Instance.StartNextPageAnimation(paragraphs);
    }
}