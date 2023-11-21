using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game._Scripts.Events.StartEnd;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class MultiPrefabInstruction : FirstSecondEvent
{
    [SerializeField] private float maxTime;

    public float MAXTime => maxTime;

    [SerializeField] private Instruction script;
    [SerializeField] private float currentTime = 0;
    [SerializeField] private bool isPlayed;
    [SerializeField] private UnityEvent _eventTimeChange;

    public UnityEvent EventTimeChange => _eventTimeChange;

    public float CurrentTime
    {
        get => currentTime;
        set
        {
            _eventTimeChange.Invoke();
            script.Instructions.FindAll(x => x.time <= currentTime && !x.isPlayed).ForEach(x => x.GetAction().Invoke());
            currentTime = value;
        }
    }

    public bool IsPlayed => isPlayed;

    private void Start()
    {
        InstantiateEvent();
    }

    private void OnDisable()
    {
        RestartTimer();
    }

    private void Play(PlayableDirector obj)
    {
        Parallel.For(0, script.Instructions.Count, (test) =>
        {
            script.Instructions[test].GetAction().Invoke();
        });
        // for (int index = 0; index < script.Instructions.Count; index++)
        // {
        //     script.Instructions[index].GetAction().Invoke();
        // }
    }

    [Button]
    public void ChangeStatePlayer()
    {
        if (isPlayed)
            StopTimer();
        else
            PlayTimer();
    }

    [Button]
    public void RestartTimer()
    {
        isPlayed = false;

        StopAllCoroutines();
        currentTime = 0;
        _eventTimeChange.Invoke();

        script.Instructions.FindAll(x => x.isPlayed).ForEach(x => x.Restart().Invoke());
        // List<SingleInstruction> temp = script.Instructions.FindAll(x => x.isPlayed);
        //
        // Parallel.For(0, temp.Count, (test) =>
        // {
        //     temp[test].Restart().Invoke();
        //     Debug.LogError("restart");
        // });
    }

    [Button]
    public void StopTimer()
    {
        FirstEvent.Raise();

        isPlayed = false;
        StopAllCoroutines();
        // List<SingleInstruction> temp = script.Instructions.FindAll(x => x.isPlayed);
        //
        // Parallel.For(0, temp.Count, (test) =>
        // {
        //     temp[test].GetPause().Invoke();
        //     Debug.LogError("pause");
        // });
        script.Instructions.FindAll(x => x.isPlayed).ForEach(x => x.GetPause().Invoke());
    }

    [Button]
    public void Prepare()
    {
        script.Instructions.ForEach(x =>
        {
            x.Prepare()?.Invoke();
            
        });
        SetMaxTime();
    }

    [Button]
    public void PlayTimer()
    {
        SecondEvent.Raise();

        isPlayed = true;
        // script.Instructions.FindAll(x => x.time <= currentTime && !x.isPlayed).ForEach(x => x.GetAction().Invoke());
        StartCoroutine(StartingTimer());
    }

    [Button]
    public void SetTimer(float valueTime)
    {
        bool tempPlayed = isPlayed;

        if (isPlayed) StopTimer();

        currentTime = valueTime;
  
        script.Instructions.FindAll(x => x.time <= valueTime && !x.isPlayed)
            .ForEach(x => x.SetTime(valueTime - x.time));

        if (tempPlayed) PlayTimer();
    }

    [Button]
    public void SetMaxTime()
    {
        float temp;

        foreach (SingleInstruction expr in script.Instructions)
        {
            temp = expr.time + expr.GetMaxTime();
            if (maxTime < temp) maxTime = temp;
        }
    }

    IEnumerator StartingTimer()
    {
        yield return Time.deltaTime;
        CurrentTime += Time.deltaTime;
        StartCoroutine(StartingTimer());
    }
}