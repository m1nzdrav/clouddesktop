using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class TranslateBlock : MonoBehaviour
{
    [SerializeField] protected string key;

    public string Key => key;

    public abstract void ChangeText(string newText);
    public abstract void ChangeText(string newText, string font);
    public abstract string GetText();
}