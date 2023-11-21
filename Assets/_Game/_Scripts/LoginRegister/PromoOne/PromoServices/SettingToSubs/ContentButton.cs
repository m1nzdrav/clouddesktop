using System;
using UnityEngine;

public class ContentButton
{
    public void GetSettingGetCurrentString(Content introductions, PromoOneStrController item,
        ContentSetting contentSetting)
    {
        bool parse = false;
        button result = button.Null;

        if (introductions.button != null)
        {
            Enum.TryParse(introductions.button, out result);
        }

        switch (result)
        {
            case button.NextPage:
            {
                SetNextPageButton(introductions, item, contentSetting);
                break;
            }
            case button.NextActivityPromo:
            {
                SetNextActivity(item);
                break;
            }
            case button.OpenContents:
            {
                SetOpenContents(item, contentSetting.LinkedContent, introductions);
                break;
            }
            case button.NextPageContents:
            {
                SetNextPageContentsButton(introductions, item, contentSetting.LinkedContent);
                break;
            }
        }
    }

    private void SetNextPageButton(Content introductions, PromoOneStrController item, ContentSetting contentSetting)
    {
        if (introductions.autoNextPage)
        {
            item.ContentSetting = contentSetting;
            item.Pages = introductions.buttonPages;
            item.StartPage = introductions.autoNextPage;
        }
        else
            item.gameObject.AddComponent<NextPageSubs>().SetPage(contentSetting, introductions.buttonPages);
    }

    private void SetNextActivity(PromoOneStrController item)
    {
        item.gameObject.AddComponent<NextActivityPageSubs>();
    }

    private void SetOpenContents(PromoOneStrController item, ContentSetting main, Content content)
    {
        item.NeedClickAfterTextPrinting = content.clickAfterTextPrinting;
        NextOpenContents nextOpenContents = item.gameObject.AddComponent<NextOpenContents>();
        nextOpenContents.Set(main, content.nextContents);
    }

    private void SetNextPageContentsButton(Content introductions, PromoOneStrController item,
        ContentSetting contentSetting)
    {
        if (introductions.autoNextPage)
        {
            item.ContentSetting = contentSetting;
            item.Pages = introductions.buttonPages;
            item.StartPage = introductions.autoNextPage;
            item.LinkedPage = true;
        }
        else
            item.gameObject.AddComponent<NextPageSubs>().SetPage(contentSetting, introductions.buttonPages);
    }
}