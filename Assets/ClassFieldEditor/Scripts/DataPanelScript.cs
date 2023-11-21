using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataPanelScript : MonoBehaviour
{
    public ClassFieldEditorScript CF_Editor;

    [Tooltip("Exported class to table")] public UnityEngine.Object TC;
    public string ExtensionJsonFile => CF_Editor.SaveFileExtension;

    /// <summary>
    /// Up element with dropdown
    /// </summary>
    [HideInInspector] public LoadPanelScript LPS;

    /// <summary>
    /// Element with save file options
    /// </summary>
    [HideInInspector] public SavePanelScript SPS;

    /// <summary>
    /// List of elements of table
    /// </summary>
    private readonly List<DataFieldScript> DataFields = new List<DataFieldScript>();

    // Prefab gameobject loaded from resources
    private GameObject DataField_pref => (GameObject) Resources.Load("Prefabs/DataField");
    private GameObject LoadPanel_pref => (GameObject) Resources.Load("Prefabs/LoadPanel");
    private GameObject SavePanel_pref => (GameObject) Resources.Load("Prefabs/SavePanel");

    private void OnEnable()
    {
        TC = CF_Editor.CustomClass;
        CreateLoadPanel();
        CreateSavePanel();
        LoadClassToTable();
    }

    /// <summary>
    /// Instantiate "LoadPanel"
    /// </summary>
    public void CreateLoadPanel()
    {
        if (GetComponentInChildren<LoadPanelScript>() == null)
        {
            GameObject NewLoadPanel = Instantiate(LoadPanel_pref, transform);
            LPS = NewLoadPanel.GetComponentInChildren<LoadPanelScript>();
        }
    }

    /// <summary>
    /// Instantiate "SavePanel"
    /// </summary>
    public void CreateSavePanel()
    {
        SavePanelScript oldSPS = GetComponentInChildren<SavePanelScript>();
        if (oldSPS != null)
        {
            Destroy(oldSPS.transform.gameObject);
        }

        GameObject NewSavePanel = Instantiate(SavePanel_pref, transform);
        SPS = NewSavePanel.GetComponentInChildren<SavePanelScript>();
    }

    /// <summary>
    /// Load fields of class to panel
    /// </summary>
    public void LoadClassToTable()
    {
        DataFields.Clear();
        DataFieldScript[] DataFieldScripts = GetComponentsInChildren<DataFieldScript>();
        foreach (DataFieldScript item in DataFieldScripts)
        {
            Destroy(item.transform.gameObject);
        }

        Type t = TC.GetType();
        List<FieldInfo> Fields = new List<FieldInfo>(t.GetFields());
        foreach (FieldInfo item in Fields)
        {
            int counter = Fields.IndexOf(item);
            if (!item.IsStatic)
            {
                if (CF_Editor.CaptionsList.Count <= counter)
                {
                    CF_Editor.CaptionsList.Add(CF_Editor.NoCaptionString);
                }

                CreateDataField(item.Name, CF_Editor.CaptionsList[counter], item.GetValue(TC));
            }
        }
    }

    /// <summary>
    /// Create one table`s DataField
    /// </summary>
    /// <param name="fieldname">Class field name</param>
    /// <param name="caption">Caption of the field</param>
    /// <param name="objvar">Object</param>
    private void CreateDataField(string fieldname, string caption, object objvar)
    {
        GameObject NewDataField = Instantiate(DataField_pref, transform);

        DataFieldScript dfs = NewDataField.GetComponent<DataFieldScript>();
        dfs.name = fieldname;
        dfs.TableFieldName = caption;
        dfs.ObjVar = objvar;
        dfs.Build();

        DataFields.Add(dfs);
    }

    /// <summary>
    /// Save or rewrite table fields to class
    /// </summary>
    public void RewriteClass()
    {
        Type t = TC.GetType();
        FieldInfo[] info = t.GetFields();

        foreach (DataFieldScript item in DataFields)
        {
            int counter = DataFields.IndexOf(item);

            #region String

            if (item.InOutObject.name == typeof(string).FullName)
            {
                info[counter].SetValue(TC, item.GetComponentInChildren<TMP_InputField>().text);
            }

            #endregion

            #region Float

            if (item.InOutObject.name == typeof(float).FullName)
            {
                info[counter].SetValue(TC, Convert.ToSingle(item.GetComponentInChildren<TMP_InputField>().text));
            }

            #endregion

            #region Vector3

            if (item.InOutObject.name == typeof(Vector3).FullName)
            {
                TMP_InputField[] ifs = item.GetComponentsInChildren<TMP_InputField>();

                Vector3 vec3 = new Vector3();
                vec3[0] = Convert.ToSingle(ifs[0].text);
                vec3[1] = Convert.ToSingle(ifs[1].text);
                vec3[2] = Convert.ToSingle(ifs[2].text);

                info[counter].SetValue(TC, vec3);
            }

            #endregion

            #region Vector2

            if (item.InOutObject.name == typeof(Vector2).FullName)
            {
                TMP_InputField[] ifs = item.GetComponentsInChildren<TMP_InputField>();

                Vector2 vec2 = new Vector2();
                vec2[0] = Convert.ToSingle(ifs[0].text);
                vec2[1] = Convert.ToSingle(ifs[1].text);

                info[counter].SetValue(TC, vec2);
            }

            #endregion

            #region Bool

            if (item.InOutObject.name == typeof(bool).FullName)
            {
                Toggle ifs = item.GetComponentInChildren<Toggle>();
                info[counter].SetValue(TC, ifs.isOn);
            }

            #endregion

            #region List<float>

            if (item.InOutObject.name == typeof(List<float>).ToString())
            {
                string[] str = item.GetComponentInChildren<TMP_InputField>().text.Split(' ');
                List<float> newlist = new List<float>();
                foreach (string stritem in str)
                {
                    if (!string.IsNullOrEmpty(stritem))
                    {
                        newlist.Add(Convert.ToSingle(stritem));
                    }
                }

                info[counter].SetValue(TC, newlist);
            }

            #endregion

            #region List<string>

            if (item.InOutObject.name == typeof(List<string>).ToString())
            {
                string[] str = item.GetComponentInChildren<TMP_InputField>().text.Split(' ');
                List<string> newlist = new List<string>();
                foreach (string stritem in str)
                {
                    if (!string.IsNullOrEmpty(stritem))
                    {
                        newlist.Add(stritem);
                    }
                }

                info[counter].SetValue(TC, newlist);
            }

            #endregion

            #region Integer

            if (item.InOutObject.name == typeof(int).ToString())
            {
                info[counter].SetValue(TC, Convert.ToInt32(item.GetComponentInChildren<TMP_InputField>().text));
            }

            #endregion
        }
    }
}