using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class TranslateBlockText : TranslateBlock
{
    public Text Text;

    public override void ChangeText(string newText)
    {
        Text.text = newText;
    }

    public override void ChangeText(string newText, string font)
    {
        Text.text = newText;
        
        if (font.IsNullOrWhitespace())
            return;
        
        Text.font = (RegisterSingleton._instance.GetSingleton(typeof(FontSingleton)) as FontSingleton)?.GetFont(font);
    }

    public override string GetText()
    {
        return Text.text;
    }
}