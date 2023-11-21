using UnityEngine;
using UnityEditor;

/// <summary>
/// Add menu => "GameObject/UI/Class Field Editor"
/// </summary>
public class ClassFieldEditorCreator : Editor
{
    [MenuItem("GameObject/UI/Class Field Editor")]
    private static void CreateSatelliteInspector()
    {
        GameObject ClassFieldEditorObject = Instantiate((GameObject)Resources.Load("Prefabs/ClassFieldEditor"));
        Canvas cnv = GameObject.FindObjectOfType<Canvas>();
        ClassFieldEditorObject.transform.SetParent(cnv.transform);
        ClassFieldEditorObject.name = "ClassFieldEditor";
    }
}


