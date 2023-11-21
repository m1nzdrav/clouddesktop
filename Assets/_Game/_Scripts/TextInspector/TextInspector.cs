using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextInspector : MonoBehaviour
{
    //delegate void Resized(); 

    #region Objects to set in inspector

    [SerializeField] Dropdown textSize_Dropdown;
    [SerializeField] DoubleInputField SizeText;
    [SerializeField] ColorPaletteController Palette;
    [SerializeField] Toggle BoldStyle_toggle;
    [SerializeField] Toggle ItalicStyle_toggle;

    #endregion

    #region Static objects for static functions

    static InputField SizeText_static;
    static Toggle BoldStyle_toggle_static;
    static Toggle ItalicStyle_toggle_static;
    static ColorPaletteController Palette_static;

    #endregion

    public static Text SelectedText;
    public static Re_InputField SelectedField;

    static bool needToChangeFontStyle = true;

    private static string _selectedString = "";
    private static int _selectedString_startChar;
    private static int _selectedString_endChar;
    private static bool _stringSelected = false;

    #region TextInfo

    private static FontStyle _fontStyle = FontStyle.Normal;
    private static int _textSize = -5;

    #endregion


    void Start()
    {
        SizeText_static = SizeText;
        BoldStyle_toggle_static = BoldStyle_toggle;
        ItalicStyle_toggle_static = ItalicStyle_toggle;
    }

    string GetCharsFromInpField()
    {
        return null;
    }

    public void ChangeTextColor()
    {
        //TODO: аналогично, палитра подлежит изменению
        if (SelectedText != null)
            SelectedText.color = Palette.SelectedColor;
    }

    /*public void ChangeTextSize()
    {
        if (SelectedText != null)
        {
            SelectedText.fontSize = 14 + textSize_Dropdown.value;
        }
    }*/

    public void ChangeTextSize_InputType()
    {
        if (SelectedText != null)
        {
            SelectedText.fontSize = Convert.ToInt32(SizeText.text);
        }

        CheckIfItsNameField();
    }

    public void BoldText()
    {
        if (needToChangeFontStyle)
        {
            switch (SelectedText.fontStyle)
            {
                case FontStyle.Bold:
                    SelectedText.fontStyle = FontStyle.Normal;
                    break;
                case FontStyle.BoldAndItalic:
                    SelectedText.fontStyle = FontStyle.Italic;
                    break;
                case FontStyle.Normal:
                    SelectedText.fontStyle = FontStyle.Bold;
                    break;
                case FontStyle.Italic:
                    SelectedText.fontStyle = FontStyle.BoldAndItalic;
                    break;
            }

            CheckIfItsNameField();
        }
    }

    public void ItalicText()
    {
        if (needToChangeFontStyle)
        {
            switch (SelectedText.fontStyle)
            {
                case FontStyle.Italic:
                    SelectedText.fontStyle = FontStyle.Normal;
                    break;
                case FontStyle.BoldAndItalic:
                    SelectedText.fontStyle = FontStyle.Bold;
                    break;
                case FontStyle.Normal:
                    SelectedText.fontStyle = FontStyle.Italic;
                    break;
                case FontStyle.Bold:
                    SelectedText.fontStyle = FontStyle.BoldAndItalic;
                    break;
            }

            CheckIfItsNameField();
        }
    }

    public void LeftAllingment()
    {
        SelectedText.alignment = TextAnchor.MiddleLeft;
        CheckIfItsNameField();
    }

    public void MiddleAligment()
    {
        SelectedText.alignment = TextAnchor.MiddleCenter;
        CheckIfItsNameField();
    }

    public void RightAligment()
    {
        SelectedText.alignment = TextAnchor.MiddleRight;
        CheckIfItsNameField();
    }

    /*void CheckTextStyles(FontStyle style)
    {
        if (SelectedText.fontStyle == style)
            SelectedText.fontStyle = FontStyle.Normal;
        else if (SelectedText.fontStyle == FontStyle.BoldAndItalic)
        {
            if (style == FontStyle.Bold)
                SelectedText.fontStyle = FontStyle.Italic;
            else
                SelectedText.fontStyle = FontStyle.Bold;
        }
        else if (SelectedText.fontStyle == FontStyle.Normal)
            SelectedText.fontStyle = style;
        else
            SelectedText.fontStyle = FontStyle.BoldAndItalic;
    }*/

    public static void SelectText(Text text)
    {
        SelectedText = text;
        SizeText_static.text = Convert.ToString(SelectedText.fontSize);
        _textSize = SelectedText.fontSize;
        needToChangeFontStyle = false;
        BoldStyle_toggle_static.isOn = SelectedText.fontStyle == FontStyle.Bold ||
                                       SelectedText.fontStyle == FontStyle.BoldAndItalic;
        ItalicStyle_toggle_static.isOn = SelectedText.fontStyle == FontStyle.Italic ||
                                         SelectedText.fontStyle == FontStyle.BoldAndItalic;
        _fontStyle = SelectedText.fontStyle;
        needToChangeFontStyle = true;
        //TODO: сделать изменение цвета в инспекторе
        //*inspectorPalette.SelectedColor = SelectedText.color;
    }

    public static void SelectField(Re_InputField field)
    {
        if (field != null)
        {
            field.OnHighlighted -= SetStringFromHighlighted;
            field.OnDeHighlighted -= DeselectStringFromHighlighted;
        }

        SelectText(field.textComponent);
        SelectedField = field;
        field.OnHighlighted += SetStringFromHighlighted;
        field.OnDeHighlighted += DeselectStringFromHighlighted;
    }

    private static void SetStringFromHighlighted(int startChar, int endChar)
    {
        Debug.Log("setFromHighlighted");

        /*char[] chars = SelectedField.text.ToCharArray();
        string tagString = "";
        bool searchTag = false;
        int firstTagChar = 0;
        int lastTagChar = 0;

        if (chars[startChar] == '<')
        {
            for (int i = startChar; i < endChar; i++)
            {
                
            }
        }
        
        for (int i = startChar; i <= endChar; i++)
        {
            if (!searchTag)
            {
                if (chars[i] == '<')
                {
                    
                }
            }
        }*/

        _selectedString_startChar = startChar;
        _selectedString_endChar = endChar;
        _stringSelected = true;
    }

    private static void DeselectStringFromHighlighted()
    {
        SelectText(SelectedField.textComponent);
        _stringSelected = false;
    }

    void CheckIfItsNameField()
    {
        if (SelectedText.gameObject.tag == "Namefield")
        {
            SelectedText.gameObject.transform.parent.GetComponent<BorderForName>().OnSettingsChanged();
        }
    }
}