using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ScrollAnimation : MonoBehaviour
{
    [SerializeField] private ScrollChecker scrollChecker;
    [SerializeField] private EventTrigger eventTrigger;
    public EventTrigger EventTrigger => eventTrigger;
    public void Start()
    {
        scrollChecker.AddAreas(this);
        scrollChecker.SubscribeToTime(ActionScroll);
    }


    public bool LockAnimation;
    public abstract void ActionMin();
    public abstract void ActionMax();
    public abstract void ActionAfterMin();
    public abstract void ActionAfterMax();
    public abstract void ActionScroll(float time);
}