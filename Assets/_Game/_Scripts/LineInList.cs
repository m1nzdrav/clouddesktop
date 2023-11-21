using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class LineInList : MonoBehaviour
{
    [SerializeField] private CircleMenuButtonController button;
    [SerializeField] private GameObject[] _childLine;

    public GameObject[] ChildLine => _childLine;


    public void ListToChildLine(GameObject list)
    {
        GameObject[] tempArray = new GameObject[_childLine.Length + 1];

        for (int i = 0; i < _childLine.Length; i++)
        {
            tempArray[i] = _childLine[i];
        }

        tempArray[_childLine.Length] = list;
        _childLine = tempArray;
        CheckBorderForButton();
    }

    private void CheckBorderForButton()
    {
        if (_childLine.Length > 0)
        {
            button.BorderEnabled();
        }
        else
        {
            button.BorderDisabled();
        }
    }

    public void ListToChlidLine(List<ComponentView> list)
    {
//        Debug.LogError("Добавили в "+ gameObject.name);
        GameObject[] test = new GameObject[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            test[i] = list[i].gameObject;
        }

        _childLine = test;
        CheckBorderForButton();
    }


    [SerializeField] private float _scaleDuration = 0.5f;
    [SerializeField] private bool _isShowed, needShowing;

    private void Start()
    {
        if (_childLine?.Length > 0)
        {
            button.BorderEnabled();
        }
    }

    public bool NeedShowing
    {
        get => needShowing;
        set
        {
            needShowing = value;
            try
            {
                GetComponent<ComponentView>().CurrentJson.OpenStatus = value;
            }
            catch (Exception e)
            {
            }
        }
    }

    public void ShowHideLine()
    {
        if (_isShowed)
        {
            NeedShowing = false;
            HideChildLine();
        }
        else
        {
            ShowChildLine();
        }
    }

    public void ShowChildLine()
    {
        if (_childLine != null)
        {
            _isShowed = true;
            NeedShowing = true;
            foreach (var VARIABLE in _childLine)
            {
                VARIABLE.SetActive(true);
                if (VARIABLE.GetComponent<LineInList>().NeedShowing)
                {
                    ComponentView componentView = VARIABLE.GetComponent<ComponentView>();
                    if (componentView != null)
                    {
                        componentView.Btn.onClick.Invoke();
                    }
                }
            }
        }
    }

    public void HideChildLine()
    {
        if (_childLine != null)
        {
            _isShowed = false;

            StartCoroutine(HideChild());
        }
    }

    IEnumerator HideChild()
    {
        yield return TryHideChild();

        foreach (var VARIABLE in _childLine)
        {
            VARIABLE.SetActive(false);
        }
    }

    public Object TryHideChild()
    {
        foreach (var VARIABLE in _childLine)
        {
            try
            {
                VARIABLE.GetComponent<LineInList>().HideChildLine();
            }
            catch (Exception e)
            {
            }
        }

        return null;
    }
}