using System.Collections.Generic;
using UnityEngine;

public class MainStart : MonoBehaviour
{
    [SerializeField] private MultiPrefabInstruction _multiPrefabInstruction;
    [SerializeField] private List<StartButton> _startButtons;

    public MultiPrefabInstruction MultiPrefabInstruction => _multiPrefabInstruction;
    public List<StartButton> StartButtons => _startButtons;

    public void SetState(bool trigger)
    {
        if (trigger)
            StartTimeline();
        else
            StopTimeline();
    }

    public void EnterTrigger()
    {
        _startButtons.ForEach(x => x.Activate());
    }

    public void ExitTrigger()
    {
        _startButtons.ForEach(x => x.DeActivate());
    }

    private void StartTimeline()
    {
        _multiPrefabInstruction.PlayTimer();
        _startButtons.ForEach(x => x.ChangeStart());
    }

    private void StopTimeline()
    {
        _multiPrefabInstruction.StopTimer();
        _startButtons.ForEach(x => x.ChangeStop());
    }
}