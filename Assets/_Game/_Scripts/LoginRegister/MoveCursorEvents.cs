using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class MoveCursorEvents : MonoBehaviour
{
    [SerializeField] private AnimationClip _delay;
    [SerializeField] private CircleMenuButton _play;
    [SerializeField] private GlowSlider _circleMenu;


    [SerializeField] private Image _mainImage;
    [SerializeField] private Text _mainText;

    private float _delayInSec;

    public void StartGlowEnglish()
    {
        StartCoroutine(GlowEnglish());
    }

    IEnumerator GlowEnglish()
    {
        var eng = transform.GetChild(0);
        var glowSound = eng.GetChild(0).GetChild(0).GetComponent<Image>();
        var glow = eng.GetChild(1).GetChild(0).gameObject;

        yield return new WaitForSeconds(1f);

        glowSound.enabled = true;
        glow.SetActive(true);

        yield return new WaitForSeconds(2f);

        glowSound.enabled = false;
        glow.SetActive(false);
    }

    public void SoundEnglish()
    {
        var eng = transform.GetChild(0);

        eng.GetComponent<AudioSource>().Play();
    }

    public void StartSound()
    {
        StartCoroutine(Sound());
    }

    public void StopSound()
    {
        StopAllCoroutines();
    }

    IEnumerator Sound()
    {
        yield return new WaitForSeconds(_delay.length + 1f);

        var timeline = _circleMenu.SetPlayableDirector();

        if (timeline.state != PlayState.Playing)
        {
            _play.PlayPause();
        }
    }


    public void SetMainLanguage()
    {
        var currImage = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 2).GetComponent<Image>();
        var currText = transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1).GetComponent<Text>();

        _mainImage.sprite = currImage.sprite;
        _mainText.text = currText.text;
    }
}
