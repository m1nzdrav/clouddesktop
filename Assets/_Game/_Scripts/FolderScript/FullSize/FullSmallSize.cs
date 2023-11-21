using System;
using System.Collections;
using System.Collections.Generic;
using _Game._Scripts.FolderScript.FullSize;
using DG.Tweening;
using Doozy.Engine.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class FullSmallSize : MonoBehaviour
{
    private Vector2 _currentSize;
    [SerializeField] private Vector3 _currentPosition;
    [SerializeField] private Transform currentParent;
    [SerializeField] private bool isFullSize = false;
    private Color _currentCollor;
    private float _currentFade;
    [SerializeField] private float _animDuration = .5f;

    [SerializeField] private Transform _number;

    private Vector3 _hideVector = Vector3.zero;
    private Vector3 _showVector = Vector3.one;


    [SerializeField] private ActiveFolderAnimation _folderAnimation;
    [SerializeField] private Image _areaToFade;
    [Header("ActionFull"), SerializeField] private GameEvent fullSize, smalSize;
    [Header("FadingArea"), SerializeField] private List<GameObject> fadingArea;
    public event Action OnFull;

    public Vector2 CurrentSize
    {
        get => _currentSize;
        set => _currentSize = value;
    }

    public Vector3 CurrentPosition
    {
        get => _currentPosition;
        set => _currentPosition = value;
    }

    public bool IsFullSize
    {
        get => isFullSize;
        set => isFullSize = value;
    }

    public Color CurrentCollor
    {
        get => _currentCollor;
        set => _currentCollor = value;
    }

    public float AnimDuration
    {
        get => _animDuration;
        set => _animDuration = value;
    }

    private RectTransform _rectTransform;

    private Image _image;


    private Vector2 testTransform;

    private void Awake()
    {
        SetAllLinks();
    }

    [Button]
    public void ChangeFullSmallSize()
    {
        GetComponent<ChangeLocationTopPanel>().ChangeLocationOut();
        if (isFullSize)
        {
            SmallSize();
            StartCoroutine(NumberAnimation(_showVector, 0f, _number));
            _folderAnimation.StartAnimation(_animDuration + 0.1f);
        }
        else
        {
            GetComponent<ChangeLocationTopPanel>().ChangeLocationIn();
            FullSize();
            StartCoroutine(NumberAnimation(_hideVector, 0f, _number));
            _folderAnimation.StopAnimation();
        }

        if (OnFull != null) OnFull();
    }

    IEnumerator NumberAnimation(Vector3 endScale, float delay,
        Transform obj) //todo сделать наследование жтого метода и для этого класса, и для класса ShowElements
    {
        yield return new WaitForSeconds(delay);

        var time = 0f;
        const float period = 0.5f;
        var startScale = obj.localScale;

        while (time < period)
        {
            time += Time.deltaTime;

            var lTime = time / period;
            var lVector = Vector3.Lerp(startScale, endScale, lTime);
            obj.localScale = lVector;

            yield return null;
        }
    }

    public void FullSize()
    {
//        Debug.LogError(_rectTransform.localScale + " " +
//                       "" + _rectTransform.sizeDelta);
        fullSize?.Raise();
        if (_image == null)
        {
            SetAllLinks();
        }

        fadingArea.ForEach(x => x.SetActive(false));
        testTransform = _rectTransform.sizeDelta;

        isFullSize = true;
        _currentCollor = _image.color;
        _image.color = new Color(_currentCollor.r, _currentCollor.g, _currentCollor.b, .95f);
        _currentSize = _rectTransform.sizeDelta;
        _currentPosition = transform.localPosition;
        currentParent = transform.parent;


        transform.SetParent((RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.RectTransformCurrentArea);
        Debug.LogError(transform.parent.parent.name);
        _currentFade = _areaToFade.color.a;
        _areaToFade.DOFade(1, _animDuration);
        _rectTransform.Center(true);
        _rectTransform.DOSizeDelta(new Vector2(Screen.width, Screen.height), _animDuration)
            .OnComplete(() =>
            {
                _rectTransform.FullScreen(true);
                fadingArea.ForEach(x => x.SetActive(true));
                // transform.localPosition =new Vector3(0, 0, 100);
            });


//        GetComponent<RectTransform>().Center(true);
//        GetComponent<RectTransform>().FullScreen(true);


        transform.localPosition = Vector3.zero;
    }

    public void SmallSize()
    {
//        Debug.LogError(_rectTransform.localScale + " " +
//                       "" + _rectTransform.sizeDelta);
        smalSize?.Raise();

        if (_image == null)
        {
            SetAllLinks();
        }

        fadingArea.ForEach(x => x.SetActive(false));
        _image.color = _currentCollor;
        isFullSize = false;

        transform.SetParent(currentParent);

//        _rectTransform.Copy(testTransform);
        _rectTransform.Center(true);
        _rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        _areaToFade.DOFade(_currentFade, _animDuration);
        _rectTransform.DOSizeDelta(_currentSize, _animDuration)
            .OnComplete(() => fadingArea.ForEach(x => x.SetActive(true)));

        transform.localPosition = _currentPosition;
    }

    [Button]
    public void TestMetodCenter()
    {
        _rectTransform.AnchorMaxToCenter();
        _rectTransform.AnchorMinToCenter();
    }


    [Button]
    public void TestMetodFull()
    {
//        GetComponent<RectTransform>().Center(true);
        _rectTransform.FullScreen(true);
    }

    private void SetAllLinks()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();

        FolderModel _folderModel = GetComponent<FolderModel>();
        if (_folderModel != null && _folderModel.RotatedArea != null)
        {
            _areaToFade = _folderModel.RotatedArea.GetComponent<Image>();
        }
        else if (_areaToFade == null)
        {
            _areaToFade = _image;
        }
    }
}