using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextActivityPageSubs : MonoBehaviour
{
    private void Start()
    {
        gameObject.AddComponent<EventTrigger>().AddEventTrigger(OnPointerUp, EventTriggerType.PointerUp);
    }

    private void OnPointerUp(BaseEventData eventData)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)?.CurrentPromoActivity.ChangeActivity();
    }
}