using System;
using System.Collections.Generic;
using _Game._Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Documents 
{
    public int[] StartPages;

    public List<Page> Pages;

    [Button]
    private void Get(string name)
    {
        Documents temp = Config.LoadJson<Documents>(name);
        StartPages = temp.StartPages;
        Pages = temp.Pages;
    }
}