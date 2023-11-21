using UnityEngine;


public interface IConfiguration
{
     JsonUnity Get(GameObject obj, JsonUnity newValue);
     void Set(GameObject obj, IdNameValue value);
}