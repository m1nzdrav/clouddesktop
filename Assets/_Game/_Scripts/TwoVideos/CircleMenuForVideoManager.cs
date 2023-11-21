using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CircleMenuForVideoManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _video;
    [SerializeField] private AudioSource _audio;

    [Header("Buttons")]
    [Header("PlayPause")]
    [SerializeField] private Button _play;
    [SerializeField] private GameObject _playImage;
    [SerializeField] private GameObject _pauseImage;

    [Header("Volume")]
    [SerializeField] private Button _volumeUp;
    [SerializeField] private Button _volumeDown;


    private const int _divisor = 100;
    [Range(0, _divisor)] [SerializeField] private int _startVolume;

    [Range(0, _divisor)] [SerializeField] private int _step;

    [Header("Rewind")]
    [SerializeField] private Button _rewindUp;
    [SerializeField] private Button _rewindDown;

    private void Awake()
    {
        _play.onClick.AddListener(SetPlayPause);

        _audio.volume = _startVolume / _divisor;
        _volumeUp.onClick.AddListener(() => SetVolume(true));
        _volumeDown.onClick.AddListener(() => SetVolume(false));
    }

    private void OnDestroy()
    {
        _play.onClick?.RemoveAllListeners();
        _volumeUp.onClick?.RemoveAllListeners();
        _volumeDown.onClick?.RemoveAllListeners();
    }

    public void SetPlayPause()
    {
        var isPlay = _video.isPlaying;

        if (isPlay)
        {
            _video.Pause();
            _pauseImage.SetActive(true);
            _playImage.SetActive(false);
        }
        else
        {
            _video.Play();
            _pauseImage.SetActive(false);
            _playImage.SetActive(true);
        }
    }

    public void SetVolume(bool isUp)
    {
        var step = _step / _divisor;

        if (isUp)
        {
            _audio.volume += step;
        }
        else
        {
            _audio.volume -= step;
        }
    }
}
