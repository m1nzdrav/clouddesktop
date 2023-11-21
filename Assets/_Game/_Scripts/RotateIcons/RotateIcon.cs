using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class RotateIcon : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _name;

    [SerializeField] private float _period = 0.5f;
    [SerializeField] private int _turns = 1;

    private float _prevValue;

    public void Rotate()
    {
        var icon = transform;
        var angle = _slider.value;

        float delta = angle - _prevValue;
        transform.Rotate(Vector3.forward * delta * 360);

        _prevValue = angle;
    }

    [Button]
    public void Rotate2DAnimation()
    {
        StartCoroutine(Animation(Vector3.forward));
    }

    [Button]
    public void Rotate3DAnimation()
    {
        StartCoroutine(Animation(Vector3.up));
    }

    IEnumerator Animation(Vector3 rotate)
    {
        var parent = _name.parent;
        _name.SetParent(transform.parent);

        var time = 0f;

        while (time < _period)
        {
            time += Time.deltaTime;

            var lTime = time / _period;

            var angle = Mathf.Lerp(0f, _turns, lTime);
            float delta = angle - _prevValue;
            _prevValue = angle;
            transform.Rotate(rotate * delta * 360);

            yield return null;
        }

        _name.SetParent(parent);
    }
}
