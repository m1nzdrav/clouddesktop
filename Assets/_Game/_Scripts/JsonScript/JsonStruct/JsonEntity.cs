
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonEntity<T>
{
    protected string name;
    [SerializeField] public List<T> data;
    
    public JsonEntity(string nameEntity)
    {
        name = name;
        data = new List<T>();
    }
}