using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class field editor script
/// </summary>
public class ClassFieldEditorScript : MonoBehaviour
{
    [Tooltip("Custom class")] public Object CustomClass;

    [Tooltip("Json file extension")] public string SaveFileExtension;

    [SerializeField] [Tooltip("Field names")]
    public List<string> CaptionsList = new List<string>();

    /// <summary>
    /// Default field name, if not included in CaptionList
    /// </summary>
    public string NoCaptionString = "Caption";

    [HideInInspector] public Scrollbar SB;

    [HideInInspector] [SerializeField] public bool isAddClass = false;

    private void OnEnable()
    {
        if (CustomClass == null)
        {
            Debug.Log("Please add <Custom Class> in ClassFieldEditor");
        }

        //GetGridControllerRuntime scrollbar in position 1
        SB.value = 1.0f;
    }
}