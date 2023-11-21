using System;
using System.Collections;
using System.Collections.Generic;
using OneLine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MoveCursor : MonoBehaviour
{
    [field: SerializeField,
            Required]
    private CircleMenuController circleController;

    [field: SerializeField,
            Required]
    private RotateCircleIndicator _rotateCircleIndicator;


//    [SerializeField]
//    private Sprite cursor;
    [SerializeField, OneLine, OneLine.HideLabel]
    private WardsCursor[] wards;

    [SerializeField] private float speedUP;
    [SerializeField] private float Speed;
    [SerializeField] private float timeToChange;
    [SerializeField] private float timeToSpeed;
    private float startSpeed;
    private int i = 0;

    public void StartVideo()
    {
        i = 0;
    }


    bool Move = false;
    bool firstStart = true;

    public bool FirstStart => firstStart;

    private void Start()
    {
        startSpeed = Speed;
    }

    private void FixedUpdate()
    {
        if (i != wards.Length)
            ActivationCheck();
        if (Move)
        {
            if (i < wards.Length)
            {
                DoMoveCursor();

                if (i >= wards.Length)
                {
                    Move = false;
                }
            }
        }
    }

    private void DoMoveCursor()
    {
        Vector3 endPos = new Vector3(wards[i].Ward.position.x, wards[i].Ward.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, endPos, Speed * Time.fixedDeltaTime);
        Speed += speedUP;

        if (transform.position == endPos)
        {
            i++;
            if (wards[i - 1].StopMove)
            {
                
//                startSpeed = (transform.position - wards[i].Ward.position).magnitude / timeToSpeed;
                Speed = (transform.position - wards[i].Ward.position).magnitude / timeToSpeed;
                Debug.LogError(Speed);
                Move = false;
                return;
            }

            if (i >= wards.Length)
            {
                Move = false;
                return;
            }


            Speed = wards[i].Speed;
            Move = false;
            StartCoroutine(PauseCursor());
        }
    }

    public IEnumerator StartOffset()
    {
        yield return new WaitForSeconds(2f);
        Move = true;
    }

    IEnumerator PauseCursor()
    {

        if (wards[i - 1].Sound)
        {
            GetComponent<AudioSource>().Play();
        }

        if (wards[i - 1].GameEvent!=null)
        {
            wards[i - 1].GameEvent.Raise();
        }

        Move = false;

        yield return new WaitForSeconds(wards[i - 1].PuaseCursor);

        Move = true;
    }

    [Button]
    public void startMove()
    {

        if (firstStart)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(StartOffset());
            firstStart = false;
        }
        else
            Move = true;
    }

    private void ActivationCheck()
    {
        if (Mathf.Abs(circleController.RadialSlider.SliderValueRaw + timeToChange -
                      _rotateCircleIndicator.ActivationPosition) <=
            _rotateCircleIndicator.AcceptableDeviation)
        {

            startMove();
        }

        if (!firstStart && Mathf.Abs(circleController.RadialSlider.SliderValueRaw - wards[i].ActivationPosition) <=
            _rotateCircleIndicator.AcceptableDeviation)
        {
            startMove();
        }
    }
}

[Serializable]
public class WardsCursor
{
    [SerializeField] private RectTransform ward;
    [SerializeField] private float speed;
    [SerializeField] private bool sound;
    [SerializeField] private float puaseCursor;
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private float activationPosition;
    [SerializeField] private bool stopMove;

    public bool StopMove
    {
        get => stopMove;
        set => stopMove = value;
    }

    public float ActivationPosition
    {
        get => activationPosition;
        set => activationPosition = value;
    }

    public GameEvent GameEvent => _gameEvent;

    public float PuaseCursor
    {
        get => puaseCursor;
        set => puaseCursor = value;
    }

    public bool Sound
    {
        get => sound;
        set => sound = value;
    }

    public RectTransform Ward
    {
        get => ward;
        set => ward = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }
}