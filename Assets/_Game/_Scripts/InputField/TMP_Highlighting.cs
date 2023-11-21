using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMP_Highlighting : MonoBehaviour, IPointerClickHandler
{
    private Re_TMPInputField _inpField;
    
    float timer = -5;
    int clicks = 0;

    private void Start()
    {
        _inpField = GetComponent<Re_TMPInputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        timer = 0;
        clicks++;
    }
    
    void Update()
    {
        if(timer >= 0) 
        {
            timer += Time.deltaTime;
            if(timer >= 0.2f) 
            { 
                if (clicks == 3)
                    TripleClick();
                else if (clicks == 4)
                    QuadClick();

                timer = -5;
                clicks = 0;
            }
        }
    }

    void TripleClick()
    {
        FindParagraph();
    }

    void FindParagraph()
    {
        int caretPos = _inpField.caretPosition;//идет после символа
        int startPos = 0;
        int endPos = caretPos;

        char[] inputField_charArray = _inpField.text.ToCharArray();

        for (int i = caretPos; i > startPos; i--) 
        {
            if(inputField_charArray[i] == ' ' || inputField_charArray[i] == '\n') 
            {
                startPos = i + 1;
                break;
            }
        }

        if(inputField_charArray[endPos] != ' ' || inputField_charArray[endPos] == '\n') 
        {
            endPos = _inpField.text.Length;
            for (int i = caretPos; i < endPos; i++) 
            {
                if(inputField_charArray[i] == ' ' || inputField_charArray[i] == '\n') 
                {
                    endPos = i;
                    break;
                }
            }
        }
        
        _inpField.HiglightFromCharToChar(startPos, endPos);
    }

    void QuadClick()
    {
        _inpField.HiglightFromCharToChar(0, _inpField.text.Length);
    }
}
