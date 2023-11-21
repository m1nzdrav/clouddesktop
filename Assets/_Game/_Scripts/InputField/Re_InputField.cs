using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Re_InputField : InputField
{
    public delegate void IfHighlighted(int startChar, int endChar);
    public event IfHighlighted OnHighlighted;

    public delegate void IfDeHighlighted();

    public event IfDeHighlighted OnDeHighlighted;
    
    private bool m_willActivate = false;
    private bool returnToSelectCondition = false;
    private bool _wasHighlighted = false;

    #region WordData

    private int _startChar = 0;
    private int _endChar = 0;

    #endregion

    protected override void Start()
    {
        base.Start();
        _endChar = this.text.Length;
    }

    protected override void LateUpdate()
    {
        /*if (_wasHighlighted)
        {
            if (caretPositionInternal == caretSelectPositionInternal)
            {
                _wasHighlighted = false;
                //OnDeHighlighted?.Invoke();
                Debug.Log("deHighlight");
            }
        }*/
        
        if (returnToSelectCondition && !(RegisterSingleton._instance.GetSingleton(typeof(TextInspector_Singleton)) as TextInspector_Singleton).DoubleText)
        {
            Debug.Log("set");
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
        
        base.LateUpdate();
        if (m_willActivate) 
        {
            MoveTextEnd(false);
            m_willActivate = false;
        }
    }

    public string GetHighlightedString(out int firstChar, out int lastChar)
    {
        //caretPos - start
        //selectPos - end
        string returnString = "";
        char[] chars = text.ToCharArray();
        firstChar = caretPositionInternal;
        lastChar = caretSelectPositionInternal;

        for (int i = firstChar; i < lastChar; i++)
        {
            returnString = returnString + Convert.ToString(chars[i]);
        }
        
        return returnString;
    }
    
    public void GenerateHighlightFromCharToChar(int startChar, int endChar) 
    {
        caretPositionInternal = startChar;
        caretSelectPositionInternal = endChar;
        _startChar = startChar;
        _endChar = endChar;
        Rebuild(CanvasUpdate.LatePreRender);
        _wasHighlighted = true;
        
        //string highlightedZone = 
        
        //OnHighlighted?.Invoke(startChar, endChar);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (!returnToSelectCondition)
        {
            m_willActivate = true;
        }
        else
        {
            returnToSelectCondition = false;
        }
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        if ((RegisterSingleton._instance.GetSingleton(typeof(TextInspector_Singleton)) as TextInspector_Singleton).ShouldDeselect) 
        {
            DeactivateInputField();
            base.OnDeselect(eventData);
            MoveTextStart(false);
        }
        else
        {
            returnToSelectCondition = true;
        }
    }

    public void Change_endChar()
    {
        _endChar = this.text.Length;
    }
}
