using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideText : MonoBehaviour
{
    [SerializeField] private Paragraphs subs;

    public Paragraphs Subs
    {
        get => subs;
        set => subs = value;
    }

    // public Paragraphs Subs { get; set; }

    public void ShowAll()
    {
        var subsCount = Subs.Pages.Count;

        for (int i = 0; i < subsCount; i++)
        {
            PromoOneStrController firstStr = Subs.Pages[i].Paragraphs[0].FirstStr;
            
            if (firstStr == null ||!firstStr.gameObject.activeSelf ) continue;

            // if (!firstStr.gameObject.activeSelf) continue;
            if (firstStr.SpecialSettings)
            {
                firstStr.SettingsForLoginScene
                    .OnPointerClick(
                        null); //todo проумать более абстрактный вариант для настроек определенных страниц
            }
            else
            {
                firstStr.OpenCloseButton(true);
            }
        }
    }

    public void HideAll()
    {
        var subsCount = Subs.Pages.Count;

        for (int i = 0; i < subsCount; i++)
        {
            var firstStr = Subs.Pages[i].Paragraphs[0].FirstStr;

            if (firstStr.gameObject.activeSelf)
            {
                if (firstStr.SpecialSettings)
                {
                    firstStr.SettingsForLoginScene
                        .OnPointerClick(
                            null); //todo проумать более абстрактный вариант для настроек определенных страниц
                }
                else
                {
                    firstStr.OpenCloseButton(false);
                }
            }
        }
    }

    //public void ShowAll()
    //{
    //    var subsCount = Subs.Count;

    //    for (int i = 0; i < subsCount; i++)
    //    {
    //        var firstStr = Subs[i][0].FirstStr;

    //        if (firstStr.gameObject.activeSelf)
    //        {
    //            firstStr.OpenCloseButton(true);
    //        }
    //    }
    //}

    //public void HideAll()
    //{
    //    var subsCount = Subs.Count;

    //    for (int i = 0; i < subsCount; i++)
    //    {
    //        var firstStr = Subs[i][0].FirstStr;

    //        if (firstStr.gameObject.activeSelf)
    //        {
    //            firstStr.OpenCloseButton(false);
    //        }
    //    }
    //}
}