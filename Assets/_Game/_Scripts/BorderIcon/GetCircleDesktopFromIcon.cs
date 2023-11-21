using System.Collections.Generic;
using UnityEngine;

public class GetCircleDesktopFromIcon : MonoBehaviour
{
    [SerializeField] private MainArea _mainArea;
    private CircleSettings _circleSettings;

    private IconModel _icon;
    private Transform _iconTransform;
    private Transform _rotatedArea;

    private const float SMALL_CONST = 1f;

    private List<GameObject> _circles;

    private List<Vector3> _savePos = new List<Vector3>();

    private const int BIGGER_SCALE = 1;
    private const int SMALLER_SCALE = 2;

    //private void Start()
    //{

    //    _icon = _mainArea.Icon;
    //    _iconTransform = _icon.transform;
    //    _circleManager = _icon.GetComponent<CircleSettings>();

    //    _circles = _circleManager.Circles;
    //    _rotatedArea = _mainArea.RotatedArea;
    //}

    //public void SetGlowParentForClose()
    //{
    //    //todo перенесни в метод и вызвать 1 раз
    //    if (_circles == null)
    //    {
    //        _icon = _mainArea.Icon;
    //        _iconTransform = _icon.transform;
    //        _circleManager = _icon.GetComponent<CircleSettings>();
    //        //_circles = _circleManager.Circles;
    //        _rotatedArea = _mainArea.RotatedArea;
    //    }

    //    var count = _circles.Count;

    //    for (int i = 0; i < count; i++)
    //    {
    //        if (_circles[i] != null)
    //        {
    //            _circles[i].SetActive(false);

    //            var circleT = _circles[i].GetComponent<RectTransform>();

    //            circleT.SetParent(_iconTransform);

    //            circleT.localPosition = _savePos[i];

    //            var scale = circleT.localScale;
    //            scale.x /= SMALLER_SCALE;
    //            scale.y /=SMALLER_SCALE;
    //            circleT.localScale = scale;
    //        }
    //    }

    //    _savePos.Clear();//todo убрать очищение?
    //}

    //public void SetGlowParentForOpen()
    //{
    //    if (_circles == null)
    //    {
    //        _icon = _mainArea.Icon;
    //        _iconTransform = _icon.transform;
    //        _circleManager = _icon.GetComponent<CircleSettings>();
    //        //_circles = _circleManager.Circles;
    //        _rotatedArea = _mainArea.RotatedArea;
    //    }

    //    //var count = _circles.Count;

    //    //for (int i = 0; i < count; i++)
    //    //{
    //    //    if (_circles[i] != null)
    //    //    {
    //    //        var circleT = _circles[i].GetComponent<RectTransform>();
    //    //        circleT.SetParent(_rotatedArea);

    //    //        var pos = circleT.localPosition;

    //    //        _savePos.Add(pos);

    //    //        pos.x *= 2f;
    //    //        pos.y *= 2f;
    //    //        circleT.localPosition = pos;

    //    //        var scale = circleT.localScale;
    //    //        scale.x *= BIGGER_SCALE;
    //    //        scale.y *= BIGGER_SCALE;
    //    //        circleT.localScale = scale;
    //    //    }
    //    //}

    //    //_circleManager.ShowCircles();
    //}
}
