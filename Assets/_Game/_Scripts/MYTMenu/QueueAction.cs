using System.Collections.Generic;
using UnityEngine;

public class QueueAction : MonoBehaviour
{
    [SerializeField] private List<StatsButton> queue;
    [SerializeField] private int currentQueue = 0;

    private void Start()
    {
        ((PromoActivity)(RegisterSingleton._instance.GetSingleton(typeof(ActivityFinder)) as ActivityFinder)?.CurrentPromoActivity).MenuAction = this;
    }

    public void NextButton()
    {
        if (currentQueue >= queue.Count - 1)
            return;


        if (currentQueue >= 0)
        {
            queue[currentQueue]?.DeActivate();
        }

        queue[++currentQueue].Activate();
    }

    public void FastNextButton()
    {
        if (currentQueue >= queue.Count - 1)
            return;


        if (currentQueue >= 0)
        {
            queue[currentQueue]?.DeActivate();
        }

        queue[++currentQueue].FastActivate();
    }
}