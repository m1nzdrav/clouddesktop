public class ContentActivity
{
    public void SetActivitySetting(PromoOneStrController item, Content content)
    {
        item.NextActivitySystem.NextMenuActivity = content.nextMenuActivity;
        item.NextActivitySystem.NeedInStartMenuActivity = content.needInStartMenuActivity;
        item.NextActivitySystem.NextPromoActivity = content.nextPromoActivity;
        item.NextActivitySystem.NeedInStartPromoActivity = content.needInStartPromoActivity;
        // SetMenuActivity(item, content);
        //
        // SetPromoActivity(item, content);
    }

    private void SetPromoActivity(PromoOneStrController item, Content content)
    {
        // if (content.nextPromoActivity)
        // item.;
    }

    private void SetMenuActivity(PromoOneStrController item, Content content)
    {
        if (content.nextMenuActivity)
            item.gameObject.AddComponent<AnimationOnSubs>().SetStates(AnimationSetter.AddAnimationOpen(item, content),
                AnimationSetter.AddAnimationText(item, content));
    }
}