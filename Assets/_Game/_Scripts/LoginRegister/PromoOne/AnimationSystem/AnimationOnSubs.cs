using UnityEngine;

public class AnimationOnSubs : MonoBehaviour
{
    [SerializeField] private AnimationState _animationStateOpen;
    [SerializeField] private AnimationState _animationStateText;

    public AnimationState AnimationStateOpen => _animationStateOpen;

    public AnimationState AnimationStateText => _animationStateText;

    public void SetStates(AnimationState open, AnimationState text)
    {
        _animationStateOpen = open;

        _animationStateText = text;
        _animationStateOpen.AddNewChildStates(text);
    }

    public void SetStates(AnimationState open)
    {
        _animationStateText = open;
    }
}