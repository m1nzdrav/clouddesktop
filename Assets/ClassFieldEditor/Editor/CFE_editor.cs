using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomEditor(typeof(ClassFieldEditorScript))]
public class CFE_editor : Editor
{
    ClassFieldEditorScript CFE;

    public SerializedProperty CustomClass;

    private void OnEnable()
    {
        CFE = target as ClassFieldEditorScript;

        CustomClass = serializedObject.FindProperty("CustomClass");

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (CFE.CustomClass == null && CFE.isAddClass == true)
        {
            CFE.isAddClass = false;
            CFE.CaptionsList.Clear();
        }

        if (CFE.CustomClass != null && CFE.isAddClass == false)
        {
            CFE.CaptionsList.Clear();
            Type t = CFE.CustomClass.GetType();
            List<FieldInfo> Fields = new List<FieldInfo>(t.GetFields());
            foreach (FieldInfo item in Fields)
            {
                CFE.CaptionsList.Add(CFE.NoCaptionString + " (" + item.Name + ")");
            }
            CFE.isAddClass = true;
        }
    }
}

