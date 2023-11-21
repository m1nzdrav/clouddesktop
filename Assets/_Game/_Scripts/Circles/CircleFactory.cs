using System.Collections;
using System.Collections.Generic;
using _Game._Scripts;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CircleFactory : MonoBehaviour
{
    [SerializeField] private Image _circlePrefab;
    [SerializeField] private Transform _parent;
    public List<Circle> SomeCircles;

    private Dictionary<CircleSettings, List<Circle>> _dictionaryCircle;
    public List<Circle> Circles = new List<Circle>();

    private List<SettingsForCircle> _saveCircleSettings = new List<SettingsForCircle>();

    private const int X_SIDE = 0;
    private const int Y_SIDE = 2;
    private const int FIRST_SIDE = 0;
    private const int SECOND_SIDE = 2;

    private int _saveSide = -1;

    private const float DECREASE_ALPHA = 1.12f;
    [SerializeField] private float _startAlpha = 1;

    public float StartAlpha
    {
        get => _startAlpha;
        set
        {
            if (value >= 0 && value <= 1)
            {
                // Debug.LogError(value);
                _startAlpha = value;
            }
        }
    }

    private float _saveAlpha = 0.21f; //todo сделать _saveAlpha = _startAlpha вначале

    private void Awake()
    {
        _dictionaryCircle = new Dictionary<CircleSettings, List<Circle>>();
    }

    // public void AddCircle(List<SettingsForCircle> circleSettings, Transform parent = null)
    // {
    //     if (parent == null)
    //     {
    //         parent = _parent;
    //     }
    //
    //     foreach (SettingsForCircle settings in circleSettings)
    //     {
    //         _saveCircleSettings.Add(settings);
    //
    //         SetCircle(settings, parent);
    //     }
    // }

    public void AddCircle(CircleSettings circleSettings, Transform parent = null)
    {
        if (parent == null)
        {
            parent = _parent;
        }

        List<Circle> circles = new List<Circle>();

        foreach (SettingsForCircle settings in circleSettings.CustomCircles.stock)
        {
            _saveCircleSettings.Add(settings);

            circles.Add(SetCircle(settings, parent));
        }

        _dictionaryCircle.Add(circleSettings, circles);
    }

    private Circle SetCircle(SettingsForCircle settingsForCircleSettings, Transform parent)
    {
        // Image circle = RegisterSingleton._instance.ObjectPoolManager.CirclePool.GetObjectWithQueue()
        //     .GetComponent<Image>();
        Circle circle = Instantiate(_circlePrefab, parent).GetComponent<Circle>();

        RectTransform circleRt = circle.GetComponent<RectTransform>();
        Transform circleT = circle.transform;
        Vector2 size = circleRt.sizeDelta;

        size.x += 1;
        circleRt.sizeDelta = size;

        circleT.localPosition = settingsForCircleSettings.cordForCircle.needCord
            ? settingsForCircleSettings.cordForCircle.cord
            //GetFixCoordinate(settingsForCircleSettings.coordForCircle.cord)
            : GetCoordinate(); // проверка на фикс кордды

        circleT.localScale = Vector3.zero;
        // settingsForCircleSettings.color.a = _startAlpha;

        circle.image.color = settingsForCircleSettings.color * _startAlpha;
        StartAlpha -= Config.PERCENT_TO_FADE;

        circle.gameObject.SetActive(false);

        // Circle circleToAdd = circle.gameObject.AddComponent<Circle>();
//         circleToAdd.Set(circle.image, false);
// circle.Set();
        Circles.Add(circle);
        return circle;
    }

    public void ShowCircle(CircleSettings circleSettings)
    {
        List<Circle> circles = GetCircles(circleSettings);

        for (int i = 0; i < circles.Count; i++)
        {
            if (circles[i].active)
            {
                // _saveAlpha = circles[i].image.color.a;

                continue;
            }

            StartCoroutine(CircleAnimation(circles[i], true, 0, _saveCircleSettings[i].radius));
            circles[i].active = true;
        }
    }


    public void HideCircle(CircleSettings circleSettings)
    {
        var unActiveCirclesCount = 0;

        List<Circle> circles = GetCircles(circleSettings);

        int activeCirclesCount = 0;

        for (int i = circles.Count - 1; i >= 0; i--)
        {
            Image circleImage = circles[i].image;

            Transform circleT = circleImage.transform;
            float scaleX = circleT.localScale.x;
            StartAlpha += Config.PERCENT_TO_FADE;

            StartCoroutine(CircleAnimation(circles[i], false, scaleX, 0f, 0.8f));
            circles[i].active = false;
        }
    }

    private Vector3 GetFixCoordinate(Vector3 coord)
    {
        return new Vector3(coord.x * Screen.width / 1920, coord.y * Screen.height / 1920, coord.y);
    }

    private Vector3 GetCoordinate()
    {
        //if (_isIcon)
        //{
        //    width = Screen.width / 4;
        //    height = Screen.height / 4;
        //}
        //else
        //{
        //    width = Screen.width / 2;
        //    height = Screen.height / 2;
        //}

        var width = Screen.width / 2;
        var height = Screen.height / 2;

        var rand = Random.Range(X_SIDE, Y_SIDE);

        Vector3 point;

        int constCoord;

        if (rand == 1)
        {
            rand = Random.Range(FIRST_SIDE, SECOND_SIDE);
            _saveSide = rand;

            if (rand == 0)
            {
                constCoord = height;
            }
            else
            {
                constCoord = -1 * height;
            }


            var randomX = Random.Range(-1 * width, width);
            point = new Vector3(randomX, constCoord, 0f);
        }
        else
        {
            rand = Random.Range(FIRST_SIDE, SECOND_SIDE);
            _saveSide = rand;

            if (rand == 0)
            {
                constCoord = width;
            }
            else
            {
                constCoord = -1 * width;
            }


            var randomY = Random.Range(-1 * height, height);
            point = new Vector3(constCoord, randomY, 0f);
        }

        return point;
    }

    IEnumerator CircleAnimation(Circle circle, bool active, float from, float to, float period = 5f)
    {
        if (active)
        {
            var color = circle.image.color;
            // color.a = _saveAlpha;
            _saveAlpha /= DECREASE_ALPHA;
            circle.image.color = color;

            circle.image.gameObject.SetActive(true);
        }


        yield return null;
        circle.transform.DOScale(new Vector3(to, to, to), Config.TIME_CIRCLE - .5f);

        if (!active)
        {
            circle.gameObject.SetActive(false);

            var color = circle.image.color;
            // color.a = _startAlpha;
            _saveAlpha = _startAlpha;
            circle.image.color = color;
        }
    }


    public List<Circle> GetCircles(CircleSettings circleSettings)
    {
        return _dictionaryCircle.ContainsKey(circleSettings) ? _dictionaryCircle[circleSettings] : null;
    }
}