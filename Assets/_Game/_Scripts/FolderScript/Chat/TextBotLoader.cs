using System;
using UnityEngine;

[Serializable]
public class TextBotLoader
{
    private AllText _allText;

    public AllText LoadedText => _allText;

    public TextBotLoader()
    {
        _allText=new AllText();
    }

    public void Init(string chatText)
    {
        _allText = JsonUtility.FromJson<AllText>(
            Resources.Load<TextAsset>(chatText).text);
    }

    public void SetAnswer(int currentNumber, string answer)
    {
        _allText.Key[currentNumber].MYTValue.Add(new IdNameValue(answer));
    }
    
}