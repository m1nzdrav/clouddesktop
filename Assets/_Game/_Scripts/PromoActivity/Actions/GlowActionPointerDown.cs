using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlowActionPointerDown : Activity
{
    [SerializeField] private EventTrigger triggerObject;
    [SerializeField] private UnityEvent startEvent, afterEnd;
    [SerializeField] private Image Glow;
    [SerializeField] private float endFadeGlow = .5f;
    [SerializeField] private float animationBreath = .7f;
    [SerializeField] private float breathIntensivity = .3f;
    private Tweener animation;
    [SerializeField] private float tempAlpha;
    [SerializeField] private bool isEnd = false;
    [SerializeField] private bool blockNextActivity = false;
    private EventTrigger.Entry entry;

    public override void StartActivity()
    {
        startEvent?.Invoke();

        AddTrigger();

        if (!Glow.gameObject.activeSelf)
        {
            Glow.gameObject.SetActive(true);
        }

        tempAlpha = Glow.color.a;
        animation = Glow.DOFade(1, animationBreath).OnComplete(() => StartCoroutine(GlowBreath()));
    }

    private void AddTrigger()
    {
        EventTrigger.TriggerEvent trigger = new EventTrigger.TriggerEvent();
        trigger.AddListener(PointerDown);
        entry = new EventTrigger.Entry { callback = trigger, eventID = EventTriggerType.PointerDown };
        triggerObject.triggers.Add(entry);
    }

    void PointerDown(BaseEventData data)
    {
        EndActivity();
    }

    [Button]
    public override void EndActivity()
    {
        triggerObject.triggers.Remove(entry);
        if (!blockNextActivity)
        {
            NextActivity(Ending);
        }
    }


    private void Ending()
    {
        EndBreath();
        afterEnd?.Invoke();
    }

    IEnumerator GlowBreath()
    {
        animation = Glow.DOFade(endFadeGlow, animationBreath);
        yield return new WaitForSeconds(animationBreath + breathIntensivity);
        animation = Glow.DOFade(1, animationBreath).OnComplete(() => StartCoroutine(GlowBreath()));
    }

    public void EndBreath()
    {
        animation.Kill();
        StopAllCoroutines();
        Glow.DOFade(tempAlpha, animationBreath);
    }
}