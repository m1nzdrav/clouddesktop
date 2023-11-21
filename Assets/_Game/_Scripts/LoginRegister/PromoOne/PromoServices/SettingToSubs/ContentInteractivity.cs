using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

[Serializable]
public class ContentInteractivity
{
    public static List<PromoOneStrController> PromoOneStrControllers;
    public string locker ;
    public string key;
    
    public void SetInteractivitySetting(PromoOneStrController item, Content content,ContentSetting contentSetting)
    {
        if (content.contentInteractivity==null ||content.contentInteractivity.locker.IsNullOrWhitespace()&& content.contentInteractivity.key.IsNullOrWhitespace()) return;

        contentSetting.LockedStr.Add(item);
        
        item.ContentInteractivity = content.contentInteractivity;

        // item.ContentInteractivity.locker =content.contentInteractivity.locker;
        // item.ContentInteractivity.key =content.contentInteractivity.key;
    }
}