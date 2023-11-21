using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace _Game._Scripts.Volume
{
    public class VolumeController : MonoBehaviour,ISingleton
    { 

        public string NameComponent
        {
            get => name;
        }
        private static int lockMusic = 0;
        [SerializeField] private PlayableDirector audioSource;
        [SerializeField] private bool isPlayed;
        [SerializeField] private StatePlay statePlay = StatePlay.Stop;
        [SerializeField] private AudioSource audioClip;
        [SerializeField] private float stepVolume = .7f;
        public bool IsPlayed => isPlayed;
        
        public void CheckRegister()
        {
            
        }
        private void Awake()
        {
            if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }

        public void ChangeStateMusic()
        {
            if (audioSource.state == PlayState.Playing)
                Pause();
            else
                Play();
        }

        [Button]
        public void SetVolume(float volume)
        {
            audioClip.volume = volume;
        }

        [Button]
        public void Play()
        {
            isPlayed = true;
            audioSource.Play();
        }

        [Button]
        public void Pause()
        {
            isPlayed = false;
            audioSource.Pause();
        }

        public void VolumeDown()
        {
            statePlay = StatePlay.Down;
            StartCoroutine(IEVolumeDown());
        }

        public void VolumeUp()
        {
            statePlay = StatePlay.Up;
            StartCoroutine(IEVolumeUp());
        }

        public void VolumeDownForTimer(float timer)
        {
            StartCoroutine(IEVolumeDownForTimer(timer));
        }

        IEnumerator IEVolumeDown()
        {
            SetVolume(audioClip.volume - stepVolume);

            yield return new WaitForFixedUpdate();

            if (statePlay == StatePlay.Down && audioClip.volume > 0) StartCoroutine(IEVolumeDown());
            else if (statePlay != StatePlay.Down) statePlay = StatePlay.Up;
        }

        IEnumerator IEVolumeUp()
        {
            SetVolume(audioClip.volume + stepVolume);

            yield return new WaitForFixedUpdate();

            if (statePlay == StatePlay.Up && audioClip.volume < 1) StartCoroutine(IEVolumeUp());
            else if (statePlay != StatePlay.Up) statePlay = StatePlay.Down;
        }

        IEnumerator IEVolumeDownForTimer(float timer)
        {
            VolumeDown();
            lockMusic++;

            yield return new WaitForSeconds(timer);

            lockMusic--;

            if (lockMusic != 0) yield break;

            VolumeUp();
        }

        
    }
}