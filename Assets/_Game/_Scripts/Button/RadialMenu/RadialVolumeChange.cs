using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Video;


public class RadialVolumeChange : MonoBehaviour
{
    [SerializeField] private float volumeValue = .1f;
    [SerializeField] private RadialVolumeSlider _radialVolumeSlider;
    [SerializeField] private VideoPlayer[] videoPlayer;

    public void ChangeVolume()
    {
        AudioListener.volume = Mathf.Clamp(AudioListener.volume + volumeValue, 0, 1);

        // if (videoPlayer != null)
        //     videoPlayer.ForEach(x =>
        //         x.SetDirectAudioVolume(0, Mathf.Clamp(x.GetTargetAudioSource(0).volume + volumeValue, 0, 1)));
        
        // Debug.LogError(videoPlayer[0]?.GetTargetAudioSource(0).volume);
        _radialVolumeSlider?.UpdateVolumeSlider();
    }
}