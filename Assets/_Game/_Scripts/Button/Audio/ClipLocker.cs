using UnityEngine;

public class ClipLocker : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool isLock;

    public bool IsLock
    {
        set => isLock = value;
    }

    public void Play(AudioClip audioClip)
    {
        if (isLock) return;

        
        audioSource.PlayOneShot(audioClip);
        isLock = true;
    }
}