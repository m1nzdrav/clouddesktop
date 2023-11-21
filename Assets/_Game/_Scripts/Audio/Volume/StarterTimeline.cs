using UnityEngine;
using UnityEngine.Playables;


public class StarterTimeline : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    public void Play(string name)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(TimelinePlayer)) as TimelinePlayer)?.Play(name);
    }

    public void Play()
    {
        _playableDirector.Play();
    }
}