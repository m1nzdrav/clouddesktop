
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class TimelinePlayer : MonoBehaviour,ISingleton
{
    public string NameComponent
    {
        get => name;
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(gameObject);
        else
        {
            // GetComponents<Component>().ForEach(x=>x.e) = true;
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
    }
    [SerializeField] private List<TimelineAsset> timelineAssets;
    [SerializeField] private PlayableDirector playableDirector;

    public void Play(string nameAssets)
    {
        playableDirector.playableAsset = timelineAssets.Find(x => x.name == nameAssets);
        playableDirector.Play();
    }

    public void CheckRegister()
    {
    }
}