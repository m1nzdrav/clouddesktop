using System;
using UnityEngine;

public static class AnimationSetter
{
    public static AnimationState AddAnimationOpen(PromoOneStrController item, Content content)
    {
        AnimationState _animationStateOpen;
        switch (content.animationPointJson.AnimationOpenType)
        {
            case AnimationOpenType.Consistently:
            {
                _animationStateOpen = item.Dot.gameObject.AddComponent<AnimationConsistently>();
                break;
            }
            case AnimationOpenType.Parallel:
            {
                _animationStateOpen = item.Dot.gameObject.AddComponent<AnimationParallel>();
                break;
            }
            case AnimationOpenType.OnClick:
            {
                _animationStateOpen = item.Dot.gameObject.AddComponent<AnimationOnClick>();
                break;
            }
            default:
                Debug.LogError("this animation open type not exist");
                throw new NotSupportedException();
        }

        _animationStateOpen.SetSetting(content);
        return _animationStateOpen;
    }

    public static AnimationState AddAnimationText(PromoOneStrController item, Content content)
    {
        AnimationState _animationStateText;
        switch (content.animationTextJson.AnimationTextType)
        {
            case AnimationTextType.Fading:
            {
                _animationStateText = item.gameObject.AddComponent<AnimationFading>();
                break;
            }
            case AnimationTextType.Input:
            {
                _animationStateText = item.gameObject.AddComponent<AnimationInput>();
                break;
            }
            default:
                Debug.LogError("this animation open type not exist");
                throw new NotSupportedException();
        }

        _animationStateText.SetSetting(content);
        return _animationStateText;
    }
}