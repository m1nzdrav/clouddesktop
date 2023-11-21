using System;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TranslateBlockInputField : TranslateBlock
{
    public InputField subInput;
    [SerializeField] private RectTransform parentToRebuild;

    public override void ChangeText(string newText)
    {
        subInput.text = newText;
        
        if (parentToRebuild!=null) LayoutRebuilder.ForceRebuildLayoutImmediate(parentToRebuild);
    }

    public override void ChangeText(string newText, string font)
    {
        subInput.text = newText;
        
        if (parentToRebuild!=null) LayoutRebuilder.ForceRebuildLayoutImmediate(parentToRebuild);
        
        if (font.IsNullOrWhitespace())
            return;
        
        subInput.textComponent.font = (RegisterSingleton._instance.GetSingleton(typeof(FontSingleton)) as FontSingleton)?.GetFont(font);
        
    }

    public override string GetText()
    {
        return subInput.text;
    }
}