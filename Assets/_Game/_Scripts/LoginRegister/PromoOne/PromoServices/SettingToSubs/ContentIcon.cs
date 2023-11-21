using System;
using UnityEngine;

[Serializable]
public class ContentIcon
{
    private Transform _social, _platform;

    public ContentIcon(Transform social, Transform platform)
    {
        _social = social;
        _platform = platform;
    }

    public void SetContentIconSetting(PromoOneStrController item, Content content)
    {
        
        Enum.TryParse(content.typeContentIcon, out TypeContentIcon _type);
        // item.ContentIconSystem.TypeContentIcon = _type;
        switch (_type)
        {
            case TypeContentIcon.Platform:
            {
                item.ContentIconSystem.Set(_platform);
                break;
            }
            case TypeContentIcon.Social:
            {
                item.ContentIconSystem.Set(_social);
                break;
            }
        }
    }
}