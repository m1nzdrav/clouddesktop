public class ContentGlow
{
    public void SetGlowSetting(PromoOneStrController item, Content content)
    {
        if (!content.glowDot) return;

        item.GlowInPromo = item.gameObject.AddComponent<GlowInPromo>();
        item.GlowInPromo.SetGlow(item.Dot);
    }
}