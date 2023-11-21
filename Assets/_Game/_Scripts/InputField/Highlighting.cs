using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Highlighting : MonoBehaviour, IPointerClickHandler
{
    Re_InputField inputField;
    //[SerializeField] Text inputField_text;

    float timer = -5;
    int clicks = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        timer = 0;
        clicks++;
    }


    // Start is called before the first frame update
    void Start()
    {
        inputField = gameObject.GetComponent<Re_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0) 
        {
            timer += Time.deltaTime;
            if(timer >= 0.2f) 
            {
                if (clicks == 2)
                    DoubleClick();
                else if (clicks == 3)
                    TripleClick();
                else if (clicks == 4)
                    QuadClick();

                timer = -5;
                clicks = 0;
            }
        }
    }

    void DoubleClick() 
    {
        FindWord();
    }

    void TripleClick() 
    {
        FindParagraph();
    }

    void QuadClick() 
    {
        inputField.GenerateHighlightFromCharToChar(0, inputField.textComponent.text.Length);
    }

    //TODO: нужно сделать все одним циклом (и в findParagraph тоже)
    void FindWord() 
    {
        int caretPos = inputField.caretPosition;//идет после символа
        int startPos = 0;
        int endPos = caretPos;

        char[] inputField_charArray = inputField.text.ToCharArray();

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
            endPos = inputField.text.Length;
            for (int i = caretPos; i < endPos; i++) 
            {
                if(inputField_charArray[i] == ' ' || inputField_charArray[i] == '\n') 
                {
                    endPos = i;
                    break;
                }
            }
        }

        inputField.GenerateHighlightFromCharToChar(startPos, endPos);
    }

    void FindParagraph() 
    {
        int caretPos = inputField.caretPosition;//идет после символа
        int startPos = 0;
        int endPos = caretPos;

        char[] inputField_charArray = inputField.text.ToCharArray();

        for (int i = caretPos; i > startPos; i--)
        {
            if (inputField_charArray[i] == '\n')
            {
                startPos = i + 1;
                break;
            }
        }

        if (inputField_charArray[endPos] != '\n')
        {
            endPos = inputField.text.Length;
            for (int i = caretPos; i < endPos; i++)
            {
                if (inputField_charArray[i] == '\n')
                {
                    endPos = i;
                    break;
                }
            }
        }

        inputField.GenerateHighlightFromCharToChar(startPos, endPos);
    }
}
