using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public enum TransformArea
{
    Left,
    Right,
    Bottom,
    Top
}

public class ShowHideObjects : MonoBehaviour
{
    [SerializeField] private bool _isShowed = true;
    private bool needShow;

    public bool NeedShow
    {
        get => needShow;
        set => needShow = value;
    }

    [SerializeField] private ObjectWithPosition _objectWithPositions;
    [SerializeField] private float hideAnimationDuration;


    public bool IsShowed
    {
        get => _isShowed;
        set => _isShowed = value;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowHideObject);
    }

    public void ShowHideObject()
    {
        if (_isShowed)
        {
            if (_objectWithPositions.obj.GetComponentsInChildren<ShowHideObjects>().Length!=0)
            {
                Debug.LogError("Скрыть детей");
                HideChildFolder();
                StartCoroutine(HideFolderAfterTime());
            }
            else

                Hide();
        }
        else
        {
            Show();
            if (_objectWithPositions.obj.GetComponent<ShowableFolder>() != null)
            {
                Invoke("ShowChildFolder", hideAnimationDuration);
            }
        }
    }

    public void Show()
    {
        _isShowed = true;
        StopAllCoroutines();

        _objectWithPositions.obj.SetActive(true);
        _objectWithPositions.obj.transform.DOMove(_objectWithPositions.currentPosition,
            hideAnimationDuration);
        _objectWithPositions.obj.transform.DOScale(Vector3.one, hideAnimationDuration);
    }

    public void Hide()
    {
        Debug.LogError("name " +  _objectWithPositions.obj.name);
        _objectWithPositions.currentPosition = _objectWithPositions.obj.transform.position;
        _isShowed = false;
        _objectWithPositions.obj.transform.DOMove(transform.position, hideAnimationDuration);
        _objectWithPositions.obj.transform.DOScale(Vector3.zero, hideAnimationDuration);

        StartCoroutine(HideAfterTime());
    }

    public void HideChildFolder()
    {
        try
        {
            _objectWithPositions.obj.GetComponent<ShowableFolder>().HideChildFolder();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void ShowChildFolder()
    {
        try
        {
            _objectWithPositions.obj.GetComponent<ShowableFolder>().ShowChildFolder();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }


    IEnumerator HideAfterTime()
    {
        yield return new WaitForSeconds(hideAnimationDuration);
        _objectWithPositions.obj.SetActive(false);
    }

    IEnumerator HideFolderAfterTime()
    {
        yield return new WaitForSeconds(hideAnimationDuration);
        Hide();
    }
}

[Serializable]
public class ObjectWithPosition
{
    public TransformArea TransformArea;
    public Vector3 currentPosition;
    public GameObject obj;
}