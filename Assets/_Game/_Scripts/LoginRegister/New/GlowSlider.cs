using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
    
public class GlowSlider : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _glow;
    [SerializeField] private GameObject _glowLine;

    [Header("Slider")]
    [SerializeField] private Transform _slider;
    [SerializeField] private Text _currentTime;

    private int _saveSub;
    private float _timelineDuration;
    private float _timeline;

    private List<float> _clipsStart = new List<float>();
    private List<GameObject> _subs = new List<GameObject>();

    private bool _pause;
    private bool _start;

    private string _language;


    [Header("Glow")] 
    [SerializeField] private Transform _saveGlow;

    private GameObject _glowSlider;
    private GameObject _glowLineSlider;
    private bool _glowOpen;

    [Header("")]
    [SerializeField] private Transform _playPauseButton;
    [SerializeField] private GeneratorSubsOnLoad _subOnLoad;

    private PlayableDirector _playableDirector;

    void Start()
    {
        Application.targetFrameRate = 60;

        _glowSlider = Instantiate(_glow, _saveGlow);
        _glowLineSlider = Instantiate(_glowLine, _saveGlow);
        _glowSlider.transform.SetSiblingIndex(0);
        _glowLineSlider.transform.SetSiblingIndex(1);

        _playableDirector = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (_start)
        {
            _slider.GetComponent<Image>().fillAmount = (float)(_playableDirector.time / _timelineDuration);
        }

        _currentTime.text = SetTime();
    }

    public PlayableDirector SetPlayableDirector()
    {
        return GetComponent<PlayableDirector>();
    }

    public void SpawnGlow()
    {
        //_subs = FindObjectOfType<GeneratorSubsOnLoad>()._subs;
        _saveSub = 0;

        _glowSlider = _saveGlow.GetChild(0).gameObject;
        _glowLineSlider = _saveGlow.GetChild(1).gameObject;

        SetGlowPosition();

        _start = true;

        SetTimelineTime();
    }

    public void StartTimeline()
    {
        _playableDirector.Play();

        if (_pause)
        {
            GetValue();
            _pause = false;
        }
    }

    public void PauseTimeline()
    {
        _playableDirector.Pause();
        _pause = true;
    }

    public void StopTimeline()
    {
        _playableDirector.Stop();
        _pause = false;

        UnityEngine.UI.Button test;
    }

    public void TimelineToStart()
    {
        _playableDirector.time = 0;
    }

    public void SetInterval(string name)
    {
        var timelineAsset = (TimelineAsset)_playableDirector.playableAsset;
        foreach (var track in timelineAsset.GetOutputTracks())
        {
            track.muted = true;

            if (track.name == name)
            {
                GetDurationClips(track);
            }
        }
    }

    void GetDurationClips(TrackAsset track)
    {
        _clipsStart.Clear();
        _clipsStart.Add(0);
        track.muted = false;
        _timelineDuration = (float)track.duration;

        foreach (var clip in track.GetClips())
        {
            var end = (float)(clip.duration);
            _clipsStart.Add(_clipsStart[_clipsStart.Count - 1] + end);
        }
    }

    public void GetValue()
    {
        if (_clipsStart.Count == 0) return;

        var currentValue = _slider.GetComponent<Image>().fillAmount;

        foreach (var clipStart in _clipsStart)
        {
            if ((currentValue * _timelineDuration) < clipStart)
            {
                _saveSub = _clipsStart.IndexOf(clipStart) - 1;

                if (_playableDirector.state == PlayState.Playing)
                {
                    StopDelay();
                    SetTimelineTime();
                    StartDelay();
                }
                else
                {
                    _playableDirector.Stop();
                    SetTimelineTime();
                }

                break;
            }
        }
    }

    public void SaveGlow()
    {
        _glowSlider.transform.parent = _saveGlow;
        _glowLineSlider.transform.parent = _saveGlow;
    }

    public void GlowOpen()
    {
        _glowSlider.GetComponent<Animation>().Play("GlowOpen");
        _glowLineSlider.GetComponent<Animation>().Play("GlowLineOpen");
    }

    public void GlowClose()
    {
        _glowSlider.GetComponent<Animation>().Play("GlowClose");
        _glowLineSlider.GetComponent<Animation>().Play("GlowLineClose");
    }

    public void SetSaveSub(int sub)
    {
        //Subs[_saveSub].GetComponent<TextAndGlowControler>().IsVoice = false;
        _saveSub = sub;
        //Subs[_saveSub].GetComponent<TextAndGlowControler>().IsVoice = true;

        SetGlowParent(_saveSub);
    }

    public void SetGlowParent(int sub)
    {
        _glowSlider.transform.parent = _subs[sub].transform;
        _glowLineSlider.transform.parent = _subs[sub].transform;
    }

    public void SetGlowPosition()
    {
        var posGlow = _glowSlider.GetComponent<RectTransform>().anchoredPosition;
        posGlow.y = 0;
        _glowSlider.GetComponent<RectTransform>().anchoredPosition = posGlow;

        var posGlowLine = _glowLineSlider.GetComponent<RectTransform>().anchoredPosition;
        posGlowLine.y = 0;
        _glowLineSlider.GetComponent<RectTransform>().anchoredPosition = posGlowLine;
    }

    public void More()
    {
        //_playPauseButton.GetComponent<CircleMenuButton>().ExpressStop();
        SetInterval(_language + "More");

        if (_playableDirector.state == PlayState.Playing)
        {
            StopDelay();
            SetSaveSub(4);
            StartDelay();
        }
        else
        {
            SetSaveSub(4);
            _playPauseButton.GetComponent<CircleMenuButton>().PlayPause();
        }
    }

    public void ClickOnSub(int sub)
    {
        if (_playableDirector.state == PlayState.Playing)
        {
            StopDelay();
            SetSaveSub(sub);
            StartDelay();
        }
        else
        {
            GetComponent<Animation>().Play("FastMenuShow"); //переделать, чтобы запускать всего один раз при загрузке языка

            SetSaveSub(sub);
            _playableDirector.Stop();
            _playPauseButton.GetComponent<CircleMenuButton>().PlayPause();
        }
    }

    public void GetLanguage(string language)
    {
        _language = language;
    }


    public string SetTime()
    {
        var min = 0;
        var sec = 0;

        var durationMin = 0;
        var durationSec = 0;

        var secStr = "";

        durationSec = (int)(_timelineDuration % 60);
        durationMin = (int)(_timelineDuration / 60);

        sec = (int)(_playableDirector.time % 60);
        min = (int)(_playableDirector.time / 60);

        sec = durationSec - sec;
        min = durationMin - min;

        if (sec < 10)
        {
            secStr = "0" + sec;
        }
        else
        {
            secStr = sec.ToString();
        }

        return min + ":" + secStr;
    }

    public void SetTimelineTime()
    {
        if(_clipsStart.Count <= _saveSub) return;

        _timeline = _clipsStart[_saveSub];
        _playableDirector.time = _timeline;
    }

    public void StartDelay()
    {
        StartCoroutine(Delay());
    }  
    
    public void StopDelay()
    {
        _playableDirector.Pause();
        StopAllCoroutines();
    }

    IEnumerator Delay()
    {
        for (int i = _saveSub; i < _clipsStart.Count - 1; i++)
        {
            //Subs[_saveSub].GetComponent<TextAndGlowControler>().IsVoice = false;
            _saveSub = i;
            //Subs[_saveSub].GetComponent<TextAndGlowControler>().IsVoice = true;

            SetTimelineTime();
            _playableDirector.Play();

            SetGlowParent(_saveSub);
            SetGlowPosition();

            if (_glowOpen)
            {
                _glowOpen = false;
            }
            else
            {
                GlowOpen();

                yield return new WaitForSeconds(0.5f);
            }

            _glowSlider.GetComponent<Animation>().Play("GlowBlink");

            var delay = _clipsStart[i + 1] - _clipsStart[i] - 1;
            yield return new WaitForSeconds(delay);

            GlowClose();

            yield return new WaitForSeconds(0.45f);

            _playableDirector.Stop();
        }

        if (_playableDirector.state != PlayState.Playing)
        {
            _saveSub = 0;
            SetGlowParent(_saveSub);
            _playPauseButton.GetComponent<CircleMenuButton>().PlayPause();
        }
    }

    public void NextSub()
    {
        if (_saveSub + 1 > _subs.Count) return;

        StopAllCoroutines();
        SetSaveSub(_saveSub + 1);
        //_playPauseButton.GetComponent<CircleMenuButton>().ExpressStop();
        _playPauseButton.GetComponent<CircleMenuButton>().PlayPause();
    }
    public void PrewSub()
    {
        if (_saveSub - 1 < 0) return;

        StopAllCoroutines();
        SetSaveSub(_saveSub - 1);
        //_playPauseButton.GetComponent<CircleMenuButton>().ExpressStop();
        _playPauseButton.GetComponent<CircleMenuButton>().PlayPause();
    }
}
