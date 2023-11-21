using System.Collections;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class CircleMenuButton : MonoBehaviour
{
    [Header("General")]

    [SerializeField] private Sprite _enterSprite;
    [SerializeField] private GameObject _glow;
    [SerializeField] private GameObject _pear;

    [SerializeField] private GlowSlider _circleMenu;

    [SerializeField] private bool _havePressed;

    private Sprite _exitSprite;

    [Header("Volume")]

    [SerializeField] private CircleMenuButton _VolDown;
    [SerializeField] private CircleMenuButton _VolUp;
    [SerializeField] private GameObject _slider;

    private float _saveVol;

    [Header("Blink")]

    private float _allSubs;

    [Header("PlayPause")]

    [SerializeField] private GameObject _dropdown;
    [SerializeField] private GameObject _glowPause;

    private bool _play;


    /// <summary>
    /// Общее
    /// </summary>

    public void MouseDown()
    {
        _glow.SetActive(true);
        _pear.SetActive(true);
    }

    public void MouseUp()
    {
        _glow.SetActive(false);
        _pear.SetActive(false);
    }

    public void Pressed()
    {
        if (_havePressed)
        {
            _glow.SetActive(true);
            _pear.SetActive(true);
        }
    }

    public void UnPressed()
    {
        if (_havePressed)
        {
            _glow.SetActive(false);
            _pear.SetActive(false);
        }
    }

    public void PointerOn()
    {
        _exitSprite = GetComponent<Image>().sprite;
        GetComponent<Image>().sprite = _enterSprite;
    }

    public void PointerOut()
    {
        GetComponent<Image>().sprite = _exitSprite;
    }

    /// <summary>
    /// Звук
    /// </summary>

    private void Start()
    {
        var allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.volume = 0.5f;
        }
    }

    public void UpDownVol(bool downVol)
    {
        _VolDown.UnPressed();
        _VolUp.UnPressed();

        var plus = 0.1f;
        var vol = 1;

        if (downVol)
        {
            plus = -0.1f;
            vol = 0;
        }

        var allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.volume += plus;
            _saveVol = audioS.volume;
        }

        //Как-то нужно достать изменение звука у track

        //var timeline = _circleMenu.GetComponent<PlayableDirector>();
        //var timelineAsset = (TimelineAsset)timeline.playableAsset;
        //foreach (var track in timelineAsset.GetOutputTracks())
        //{
        //    foreach (var clip in track.GetClips())
        //    {

        //    }
        //}

        if (_saveVol == vol) Pressed();

        StopAllCoroutines();
        StartCoroutine(FadeInSlider());

        _slider.GetComponent<Image>().fillAmount = _saveVol;
    }

    IEnumerator FadeInSlider()
    {
        var color = _slider.GetComponent<Image>().color;
        color.a = 0.5f;
        _slider.GetComponent<Image>().color = color;

        yield return new WaitForSeconds(1f);

        for (var i = color.a; i > 0f; i -= 0.05f)
        {
            color.a = i;
            _slider.GetComponent<Image>().color = color;

            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Пауза
    /// </summary>

    public void PlayPause()
    {
        if (_play)
        {
            UnPressed();

            _play = false;

            var playImage = transform.GetChild(0);
            var pauseImage = transform.GetChild(1);
            pauseImage.gameObject.SetActive(false);
            playImage.gameObject.SetActive(true);

            _glowPause.SetActive(true);
            _glowPause.GetComponent<Animation>().Play("GlowOpen");

            _circleMenu.StopDelay();
        }
        else
        {
            Pressed();

            _glowPause.SetActive(false);
            _play = true;

            var playImage = transform.GetChild(0);
            var pauseImage = transform.GetChild(1);
            pauseImage.gameObject.SetActive(true);
            playImage.gameObject.SetActive(false);

            _circleMenu.StartDelay();
        }
    }

    //public void ExpressStop()
    //{
    //    if (_play)
    //    {
    //        UnPressed();

    //        FindObjectOfType<GlowSlider>().PauseTimeline();
    //        _play = false;

    //        var playImage = transform.GetChild(0);
    //        var pauseImage = transform.GetChild(1);
    //        pauseImage.gameObject.SetActive(false);
    //        playImage.gameObject.SetActive(true);

    //        _glow.SetActive(true);
    //        _glow.GetComponent<Animation>().Play("GlowOpen");

    //        _circleMenu.StopDelay();
    //    }
    //}

    //public void PlayStopForDropdown()
    //{
    //    var isOn = _dropdown.GetComponent<CustomDropdown>().GetIsOn();

    //    if (!isOn)
    //    {
    //        ExpressStop();
    //        PlayPause();
    //    }
    //    else
    //    {
    //        ExpressStop();
    //    }
    //}
}
