using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Blocker
{
    private Dictionary<string, bool> block;

    public Dictionary<string, bool> Block
    {
        get => block;
        set => block = value;
    }

    public Blocker()
    {
        block = new Dictionary<string, bool>();
    }

    public bool ChangeValue(string type, bool value)
    {
        if (!block.ContainsKey(type))
        {
      
            block.Add(type, value);
        }
        else
        {
            block[type] = value;
        }

        return ChekValue();
    }

    public bool ChekValue()
    {
        return block.ContainsValue(true);
    }
}