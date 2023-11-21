using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ShowOn : MonoBehaviour
{
    [SerializeField] public Blocker blocker;
    [SerializeField] private EventTrigger[] events;


    public bool IsBlocked
    {
        get { return blocker != null && blocker.ChekValue(); }
    }

    public void ChangeValueBlocker(string type, bool value)
    {
        if (value)
        {
            Enter(null);
        }

        blocker.ChangeValue(type, value);
        if (!blocker.ChekValue())
        {
            Exit(null);
        }
    }

    private void Start()
    {
        foreach (var VARIABLE in events)
        {
            VARIABLE.AddEventTrigger(arg0 => Enter(VARIABLE), EventTriggerType.PointerEnter);
            VARIABLE.AddEventTrigger(arg0 => Exit(VARIABLE), EventTriggerType.PointerExit);
        }
    }

    public abstract void Enter(EventTrigger who);


    public abstract void Exit(EventTrigger who);
}