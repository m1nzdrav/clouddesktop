using DG.Tweening;
using System.Collections;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Lines : MonoBehaviour
{
    public Transform _leftLine;
    public Transform _leftLamp;
    [SerializeField] private Transform _topLine;
    [SerializeField] private Transform _topLamp;
    [SerializeField] private float _delayStartLeft;
    [SerializeField] private float _delayStartTop;

    public float AnimationDurationLeft;
    public float AnimationDurationTop;

    [SerializeField] private GameObject _glowMore;
    [SerializeField] private GameObject _moreButton;
    [SerializeField] private GameObject _glowOk;

    private Vector3 _currentPosLeft;
    private Vector3 _currentPosTop;

    private bool _isMoveLeft;
    private bool _isMoveTop;

    private Tweener _moveLeftLamp;
    private Tweener _moveLeftLine;
    private Tweener _moveTopLamp;
    private Tweener _moveTopLine;

    public void MoveLeftLine()
    {
        StopAllCoroutines();
        if (_isMoveLeft)
        {
            _moveLeftLamp.Pause();
            _moveLeftLine.Pause();
            _leftLine.position = _currentPosLeft;
            _leftLamp.position = _currentPosLeft;
            _isMoveLeft = false;
        }

        if (_isMoveTop)
        {
            _moveTopLamp.Pause();
            _moveTopLine.Pause();
            _topLine.position = _currentPosTop;
            _topLamp.position = _currentPosTop;
            _isMoveTop = false;
            _topLamp.gameObject.SetActive(false);
            _topLine.gameObject.SetActive(false);
        }

        StartCoroutine(MoveAndWaitLeft(_leftLamp, new Vector3(Screen.width / 2f, 0, 0)));
    }

    public void MoveLeftLineArabic()
    {
        StopAllCoroutines();
        if (_isMoveLeft)
        {
            _moveLeftLamp.Pause();
            _moveLeftLine.Pause();
            _leftLine.position = _currentPosLeft;
            _leftLamp.position = _currentPosLeft;
            _isMoveLeft = false;
        }

        if (_isMoveTop)
        {
            _moveTopLamp.Pause();
            _moveTopLine.Pause();
            _topLine.position = _currentPosTop;
            _topLamp.position = _currentPosTop;
            _isMoveTop = false;
            _topLamp.gameObject.SetActive(false);
            _topLine.gameObject.SetActive(false);
        }

        StartCoroutine(MoveAndWaitLeft(_leftLamp, new Vector3(-Screen.width / 2f, 0, 0)));
    }

    public void MoveTopLine()
    {
        //_glowOk.GetComponent<Image>().
        //StopAllCoroutines();

        StartCoroutine(MoveAndWaitRight(_topLamp, new Vector3(0, -Screen.height / 2 + 45.02841f, 0)));
    }

    IEnumerator MoveAndWaitLeft(Transform line, Vector3 direction)
    {
        _moreButton.SetActive(false);

        var colorGlowMore = _glowMore.GetComponent<Image>().color;
        _glowMore.GetComponent<Image>().color = new Color(colorGlowMore.r, colorGlowMore.g, colorGlowMore.b, 0f);

        var colorGlowOk = _glowOk.GetComponent<Image>().color;
        _glowOk.GetComponent<Image>().color = new Color(colorGlowOk.r, colorGlowOk.g, colorGlowOk.b, 0f);

        var saveLeftlinePos = _leftLine.position;

        _isMoveLeft = true;
        yield return new WaitForSeconds(_delayStartLeft);

        _currentPosLeft = line.GetComponent<RectTransform>().anchoredPosition;

        line.gameObject.SetActive(true);
        _leftLine.gameObject.SetActive(true);

        _moveLeftLamp = line.DOLocalMove(direction, AnimationDurationLeft);
        _moveLeftLine = _leftLine.DOLocalMove(direction, AnimationDurationLeft);

        StartCoroutine(ShowGlowMore(AnimationDurationLeft));

        yield return new WaitForSeconds(AnimationDurationLeft);

        line.gameObject.SetActive(false);

        line.position = saveLeftlinePos;
        _leftLine.position = saveLeftlinePos;
        //_leftLine.GetComponent<Animation>()._play("GlowOpen");
        _isMoveLeft = false;
    }

    IEnumerator MoveAndWaitRight(Transform line, Vector3 direction)
    {
        _isMoveTop = true;
        yield return new WaitForSeconds(_delayStartTop);

        _currentPosTop = line.GetComponent<RectTransform>().localPosition;

        line.gameObject.SetActive(true);
        _topLine.gameObject.SetActive(true);

        _moveTopLamp = line.DOLocalMove(direction, AnimationDurationTop);
        _moveTopLine = _topLine.DOLocalMove(direction, AnimationDurationTop);

        StartCoroutine(ShowGlowOk(AnimationDurationTop));

        yield return new WaitForSeconds(AnimationDurationTop);

        line.gameObject.SetActive(false);
        _topLine.gameObject.SetActive(false);

        line.localPosition = _currentPosTop;
        _topLine.localPosition = _currentPosTop;
        _isMoveTop = false;
    }

    IEnumerator ShowGlowOk(float timeStop)
    {
        yield return new WaitForSeconds(timeStop);

        _glowOk.GetComponent<Animation>().Play("ShowGlow");
    }

    IEnumerator ShowGlowMore(float timeStop)
    {
        
        yield return new WaitForSeconds(timeStop);

        _moreButton.SetActive(true);
        //_moreButton.GetComponent<Animation>()._play("GlowOpen");

        yield return new WaitForSeconds(1f);

        _glowMore.GetComponent<Animation>().Play("ShowGlow");
        _moreButton.GetComponent<EventTrigger>().enabled = true;
    }

}
