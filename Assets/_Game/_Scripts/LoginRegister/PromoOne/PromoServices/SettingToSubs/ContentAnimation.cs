using Sirenix.Utilities;

public class ContentAnimation
{
    public void SetAnimationSetting(PromoOneStrController item, Content content)
    {
        if (content.point)
        {
            item.gameObject.AddComponent<AnimationOnSubs>().SetStates(AnimationSetter.AddAnimationOpen(item, content),
                AnimationSetter.AddAnimationText(item, content));
            return;
        }

        if (!content.text.IsNullOrWhitespace())
        {
            item.gameObject.AddComponent<AnimationOnSubs>().SetStates(
                AnimationSetter.AddAnimationText(item, content));
        }
    }
}