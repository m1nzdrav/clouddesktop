using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField, Required] private AudioSource _audioSource;
    // [SerializeField] private PlayableDirector _playableDirector;

    [Button]
    public void PlayMusic(string _nameMusic)
    {
        _audioSource.clip = (RegisterSingleton._instance.GetSingleton(typeof(MusicStock)) as MusicStock)?.GetAudioClip(_nameMusic);
        // _playableDirector.Play();

        _audioSource.Play();
    }

    [Button]
    public void PlayMusicOnSelfSource(string _nameMusic, AudioSource audioSource)
    {
        audioSource.clip = (RegisterSingleton._instance.GetSingleton(typeof(MusicStock)) as MusicStock)?.GetAudioClip(_nameMusic);
        // _playableDirector.Play();

        audioSource.Play();
    }
}