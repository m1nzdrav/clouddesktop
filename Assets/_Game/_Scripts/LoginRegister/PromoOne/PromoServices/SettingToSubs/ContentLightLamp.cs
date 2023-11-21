using System;

[Serializable]
public class ContentLightLamp
{
    public void SetLightLampSetting(PromoOneStrController item, Content content)
    {
        item.LightLampSystem.NeedLightLamp = content.earthLamp;
    }
}