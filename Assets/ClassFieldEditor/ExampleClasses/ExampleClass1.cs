using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Example class loaded to Table
/// </summary>
public class ExampleClass1 : MonoBehaviour
{
    public string StringField;
    public float FloatField;
    public Vector2 Vector2Field;
    public Vector3 Vector3Field;
    public bool BoolField;
    public List<float> ListField = new List<float>();
    public List<string> StringList = new List<string>();
    public int IntField;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}