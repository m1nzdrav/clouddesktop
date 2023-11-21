using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoveCursorToPos : MonoBehaviour
{
    private Vector3 _cursorPos;
    private float _saveSpeed;
    private float _delay;

    [SerializeField] private CursorAnimation _cursorAnimation;
    [SerializeField] private Lines _lines;
    [SerializeField] protected Image _cursor;

    [SerializeField] private float _speed;
    [SerializeField] private List<Points> _points;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _saveSpeed = _speed;

        _delay = _cursorAnimation.Delay * _cursorAnimation.CircleCount + 1f;

        StartCoroutine(GlobalDelay(_delay));
    }

    IEnumerator GlobalDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Move(0));
    }

    IEnumerator Move(int index)
    {
        yield return new WaitForSeconds(_points[index].Delay);

        while (true)
        {
            _cursorPos = transform.position;
            transform.position = Vector3.MoveTowards(_cursorPos, _points[index].Point.position, _speed * Time.deltaTime);

            _speed += 0.05f;

            if (_cursorPos == _points[index].Point.position) break;

            yield return 0;
        }

        _points[index].HitEvent.Invoke();

        if (index != _points.Count - 1)
        {
            _speed = _saveSpeed;
            StartCoroutine(Move(index + 1));
        }
        else
        {
            StartCoroutine(_cursorAnimation.SpawnCircleMouse());

            var lineDelay = _lines.AnimationDurationLeft;

            yield return new WaitForSeconds(lineDelay + 1.8f);//задержка появления мыши 

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void StartHideCursor()
    {
        StartCoroutine(HideCursor());
    }

    IEnumerator HideCursor()
    {
        yield return new WaitForSeconds(0.1f);

        _cursor.enabled = false;
    }
}

[Serializable]
public class Points
{
    public Transform Point;
    public float Delay;
    public UnityEvent HitEvent;
}
