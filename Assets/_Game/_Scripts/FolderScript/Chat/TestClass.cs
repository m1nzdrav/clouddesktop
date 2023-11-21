

using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class TestClass: MonoBehaviour
{
    public string ar;
    [SerializeField] public bool _bool;
    [SerializeField] public int _int;
    [SerializeField] public String _string;
    [SerializeField] public float _float;
    [SerializeField] public Color _color;
    [SerializeField] public Color _newColor;
    [SerializeField] public Vector3 _vectorStart;
    [SerializeField] public Vector3 _vector3;
    
    public void Clone()
    {
        Debug.LogError("test");
    }
}

public class AudioSourceExtension 
{
    public  object Clone( TerrainData terrainData)
    {
       return MemberwiseClone();
    }
}