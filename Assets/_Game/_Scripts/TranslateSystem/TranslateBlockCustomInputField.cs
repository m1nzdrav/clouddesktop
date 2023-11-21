using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class TranslateBlockCustomInputField : TranslateBlock
{
    public Re_InputField subInput;
    
    [SerializeField] private RectTransform parentToRebuild;
    public override void ChangeText(string newText)
    {
        subInput.text = newText;
        
        if (parentToRebuild!=null) LayoutRebuilder.ForceRebuildLayoutImmediate(parentToRebuild);
    }

    public override void ChangeText(string newText, string font)
    {
        subInput.text = newText;
        
        if (font.IsNullOrWhitespace())
            return;
        
        subInput.textComponent.font = (RegisterSingleton._instance.GetSingleton(typeof(FontSingleton)) as FontSingleton)?.GetFont(font) ;
    }

    public override string GetText()
    {
        return subInput.text;
    }
}