using System;
using Sirenix.Utilities;
using UnityEngine;

[Serializable]
public class UserData
{
    public string language;
    public string selfReferal;
    public string token;
    public string bread;
    
    public void SetData(string language = null, string selfReferal = null, string token = null, string bread = null)
    {
        this.language = this.language.IsNullOrWhitespace() ? this.language : language;
        this.selfReferal = this.selfReferal.IsNullOrWhitespace() ? this.selfReferal : selfReferal;
        this.token = this.token.IsNullOrWhitespace() ? this.token : token;
        this.bread = this.bread.IsNullOrWhitespace() ? this.bread : bread;
    }

    public string GetData()
    {
        return JsonUtility.ToJson(this);
    }

    
}