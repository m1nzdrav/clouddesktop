using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Re_TMPInputField : TMP_InputField
{
    private bool m_willActivate = false;
    private bool m_willDeaktivate = false;

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MoveTextStart(false);
        }
    }*/

    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (m_willActivate)
        {
            MoveTextEnd(false);
            m_willActivate = false;
        }

        if (m_willDeaktivate)
        {
            MoveTextStart(false);
            m_willDeaktivate = false;
            Debug.Log("move to start");
        }
    }
    
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        m_willActivate = true;
    }
    
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        m_willDeaktivate = true;
    }
    
    public void HiglightFromCharToChar(int startPos, int endPos)
    {
        caretPositionInternal = startPos;
        caretSelectPositionInternal = endPos;
    }
}
