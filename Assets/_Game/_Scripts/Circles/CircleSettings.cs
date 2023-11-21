using System.Collections.Generic;
using UnityEngine;

public class CircleSettings : MonoBehaviour
{
    public List<SettingsForCircle> CirclesSettings = new List<SettingsForCircle>();
    [SerializeField] private CustomCircles _customCircles;

    public CustomCircles CustomCircles => _customCircles;

    public CircleFactory CircleFactory;

    //private static Transform _mainParent;

    //private void Start()
    //{
    //    if (_needCreateToStart)
    //    {
    //        StartSpawnCircles();
    //    }
    //}

    ////public void AddCircle(Image circlePrefab = null, Transform parent = null)
    ////{
    ////    if (_circleDesktopPrefab == null)
    ////    {
    ////        _circleDesktopPrefab = circlePrefab;
    ////    }

    ////    if (_startParent == null)
    ////    {
    ////        _startParent = parent;
    ////    }

    ////    var newCircleSettings = new SettingsForCircle
    ////    {
    ////        Color = new Color(0f, 0f, 1f, 0.4f),
    ////        Material = null,
    ////        Radius = 25f
    ////    };

    ////    CirclesSettings.Add(newCircleSettings);
    ////    SetSettingsForCircle(newCircleSettings);

    ////    newCircleSettings = new SettingsForCircle
    ////    {
    ////        Color = new Color(0.26f, 0.03f, 0.16f, 0.17f),
    ////        Material = null,
    ////        Radius = 20f
    ////    };

    ////    CirclesSettings.Add(newCircleSettings);
    ////    SetSettingsForCircle(newCircleSettings);

    ////    _haveCircles = true;
    ////}

    //public void ShowHideCircles(bool active)
    //{
    //    if (active)
    //    {
    //        //OnEnable();
    //    }
    //    else
    //    {
    //        HideCircles();
    //    }
    //}

    //public void StartSpawnCircles()
    //{
    //    if (_startParent == null)
    //    {
    //        //var getCircleParent = GetCircleParent.Instance;
    //        //_startParent = getCircleParent.GetParent();

    //        _startParent = GameObject.FindGameObjectWithTag("CircleParent").transform;
    //    }

    //    foreach (var circleSettings in CirclesSettings)
    //    {
    //        SetSettingsForCircle(circleSettings);
    //    }

    //    _haveCircles = true;
    //}

    //private void SetSettingsForCircle(SettingsForCircle circleSettings)
    //{
    //    var circle = SpawnCircle(_startParent, circleSettings.Color, circleSettings.Material);

    //    var circleRT = circle.GetComponent<RectTransform>();
    //    var size = circleRT.sizeDelta;
    //    size.x += 1;
    //    circleRT.sizeDelta = size;

    //    Circles.Add(circle);

    //    var circleT = circle.transform;
    //    var circleX = circleT.localScale.x;
    //    var radius = circleSettings.Radius;

    //    if (_needShowAlways)
    //    {
    //        StartCoroutine(CircleAnimation(circleT, circleX, radius));
    //    }
    //    else
    //    {
    //        circleT.localScale = new Vector2(radius, radius);

    //        circle.SetActive(false);
    //    }
    //}

    //public void ShowCircles()
    //{
    //    StopAllCoroutines();

    //    if (_haveCircles)
    //    {
    //        var count = Circles.Count;
    //        var countSettings = CirclesSettings.Count;

    //        for (var i = 0; i < count; i++)
    //        {
    //            var circle = Circles[i];
    //            circle.gameObject.SetActive(true);

    //            var circleT = circle.transform;

    //            if (countSettings <= i)
    //            {
    //                StartCoroutine(CircleAnimation(circleT, 0, CirclesSettings[0].Radius));
    //                continue;
    //            }

    //            StartCoroutine(CircleAnimation(circleT, 0, CirclesSettings[i].Radius));
    //        }
    //    }
    //}

    //public void HideCircles()
    //{
    //    StopAllCoroutines();

    //    if (_haveCircles)
    //    {
    //        var count = Circles.Count;
    //        var countSettings = CirclesSettings.Count;

    //        for (var i = 0; i < count; i++)
    //        {
    //            var circle = Circles[i];
    //            var circleT = circle.transform;
    //            var scaleX = circle.transform.localScale.x;

    //            //if (countSettings <= i)
    //            //{
    //            //    StartCoroutine(CircleAnimation(circleT, 0, CirclesSettings[0].Radius));
    //            //    continue;
    //            //}

    //            StartCoroutine(CircleAnimation(circleT, scaleX, 0f, 0.8f));
    //        }
    //    }
    //}

    ////private void OnEnable()
    ////{
    ////    StopAllCoroutines();

    ////    ShowCircles();
    ////}

    ////private void OnDisable()
    ////{
    ////    StopAllCoroutines();

    ////    HideCircles();
    ////}

    //public GameObject SpawnCircle(Transform desktopParent, Color color, Material material)
    //{
    //    //var thePosition = transform.TransformPoint(Random.Range(-1 * Screen.width / 2, Screen.width / 2), Random.Range(-1 * Screen.height / 2, Screen.height / 2), 0);
    //    //var parent = desktopParent.GetComponent<MainArea>().RotatedArea;

    //    var parent = desktopParent;

    //    var circle = Instantiate(_circleDesktopPrefab, parent);

    //    var circleTransform = circle.transform;
    //    circleTransform.localPosition = GetCoordinate();

    //    //_startScale = _startScale / _decrease;

    //    var changeColorA = circle.color;

    //    if (NeedDeacreese)
    //    {
    //        changeColorA.a = _startAlpha / _deacreeseAlpha;
    //        _startAlpha = changeColorA.a;
    //        color.a = changeColorA.a;
    //    }

    //    circle.color = color;

    //    if (material != null)
    //    {
    //        circle.material = material;
    //    }

    //    //circleTransform.SetAsFirstSibling();
    //    circleTransform.SetSiblingIndex(1);


    //    return circle.gameObject;
    //}

    //public Vector3 GetCoordinate()
    //{
    //    int width;
    //    int height;

    //    if (_isIcon)
    //    {
    //        width = Screen.width / 4;
    //        height = Screen.height / 4;
    //    }
    //    else
    //    {
    //        width = Screen.width / 2;
    //        height = Screen.height / 2;
    //    }

    //    var left = -1 * width;
    //    var right = width;
    //    var bot = -1 * height;
    //    var top = height;

    //    var randomX = Random.Range(left, right);
    //    var randomY = Random.Range(bot, top);

    //    var rand = Random.Range(_xSide, _ySide);

    //    Vector3 point;

    //    if (rand == 1)
    //    {
    //        int constCoord;

    //        if (_saveSide == _firstSide)
    //        {
    //            rand = _secondSide - 1;
    //        }
    //        else if (_saveSide == _secondSide)
    //        {
    //            rand = _firstSide;
    //        }
    //        else
    //        {
    //            rand = Random.Range(_firstSide, _secondSide);
    //            _saveSide = rand;
    //        }

    //        if (rand == 0)
    //        {
    //            constCoord = height;
    //        }
    //        else
    //        {
    //            constCoord = -1 * height;
    //        }

    //        point = new Vector3(randomX, constCoord, 0f);

    //    }
    //    else
    //    {
    //        int constCoord;

    //        if (_saveSide == _firstSide)
    //        {
    //            rand = _secondSide - 1;
    //        }
    //        else if (_saveSide == _secondSide)
    //        {
    //            rand = _firstSide;
    //        }
    //        else
    //        {
    //            rand = Random.Range(_firstSide, _secondSide);
    //            _saveSide = rand;
    //        }

    //        if (rand == 0)
    //        {
    //            constCoord = width;
    //        }
    //        else
    //        {
    //            constCoord = -1 * width;
    //        }

    //        point = new Vector3(constCoord, randomY, 0f);

    //    }

    //    return point;
    //}

    //IEnumerator CircleAnimation(Transform circle, float from, float to, float period = 5f)
    //{
    //    var time = 0f;

    //    while (time < period)
    //    {
    //        time += Time.deltaTime;
    //        var lTime = time / period;

    //        var lValue = Mathf.Lerp(from, to, lTime);
    //        from = lValue;
    //        circle.localScale = new Vector3(lValue, lValue);

    //        yield return null;
    //    }

    //    circle.localScale = new Vector3(to, to);
    //}
}