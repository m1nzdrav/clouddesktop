using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MissingScriptsCleaner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjects;
#if UNITY_EDITOR
    

    [Button]
    public void Clean()
    {
        foreach (var gameObject in gameObjects) 
        {
            gameObject.transform.FindAmongChilds<Transform>(t => GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject));
        }
    }
#endif
}
