using System;
using System.Collections.Generic;
using System.IO;
using _Game._Scripts;
using UnityEngine;

[Serializable]
public class DialogModel
{
    public List<SingleText> singleTexts;
    public string font;

    public DialogModel()
    {
        singleTexts = new List<SingleText>();
    }

    public static DialogModel Init(string chatText)
    {
        return Config.LoadJson<DialogModel>(chatText+ChangeLanguageLoginScene.currentLanguage);
    }
}