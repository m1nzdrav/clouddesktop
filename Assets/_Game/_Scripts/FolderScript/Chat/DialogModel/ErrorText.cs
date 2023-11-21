using System;
using System.Linq;

[Serializable]
public class ErrorText
{
    public string textError;
    public AccessText[] accessTexts;
    public string textKeyRef;

    public bool Validate(char ch)
    {
        if (accessTexts.Contains(AccessText.Latin))
        {
            if (ch >= 'A' && ch <= 'Z') return true;
            if (ch >= 'a' && ch <= 'z') return true;
        }

        if (accessTexts.Contains(AccessText.Number))
        {
            if (ch >= '0' && ch <= '9') return true;
        }

        if (accessTexts.Contains(AccessText.Dash))
        {
            if ((ch == '_' || ch == '-' || ch == '@')) return true;
        }

        if (accessTexts.Contains(AccessText.Dot))
        {
            if ((ch == '.' || ch == ',')) return true;
        }
       

        return false;
    }
}
