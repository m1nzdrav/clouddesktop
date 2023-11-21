using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class DataFieldScript : MonoBehaviour
{
    /// <summary>
    /// Caption of the tables field
    /// </summary>
    public TextMeshProUGUI Caption;

    /// <summary>
    /// Place zone Prefab instantiate
    /// </summary>
    public GameObject ValueZone;

    /// <summary>
    /// Created Prefab object
    /// </summary>
    public GameObject InOutObject;

    /// <summary>
    /// Name field of table
    /// </summary>
    public string TableFieldName;

    /// <summary>
    /// Field
    /// </summary>
    public object ObjVar;

    // Prefab gameobject loaded from resources
    private GameObject InField_x1 => (GameObject) Resources.Load("Prefabs/InputField_x1");
    private GameObject InField_v3 => (GameObject) Resources.Load("Prefabs/InputField_v3");
    private GameObject InField_v2 => (GameObject) Resources.Load("Prefabs/InputField_v2");
    private GameObject Toogle_bool => (GameObject) Resources.Load("Prefabs/Toggle_bool");

    /// <summary>
    /// Build table datafield from class
    /// </summary>
    public void Build()
    {
        Caption.text = TableFieldName;

        #region Float

        if (ObjVar.GetType() == typeof(float))
        {
            InOutObject = Instantiate(InField_x1, ValueZone.transform);
            InOutObject.name = typeof(float).FullName;
            InOutObject.GetComponent<TMP_InputField>().text = ObjVar.ToString();
            InOutObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.DecimalNumber;
        }

        #endregion

        #region String

        if (ObjVar.GetType() == typeof(string))
        {
            InOutObject = Instantiate(InField_x1, ValueZone.transform);
            InOutObject.name = typeof(string).FullName;
            InOutObject.GetComponent<TMP_InputField>().text = ObjVar.ToString();
            InOutObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Standard;
        }

        #endregion

        #region Vector3

        if (ObjVar.GetType() == typeof(Vector3))
        {
            InOutObject = Instantiate(InField_v3, ValueZone.transform);
            InOutObject.name = typeof(Vector3).FullName;
            TMP_InputField[] IFs = InOutObject.GetComponentsInChildren<TMP_InputField>();
            Vector3 tempV3 = (Vector3) ObjVar;
            IFs[0].text = tempV3.x.ToString();
            IFs[1].text = tempV3.y.ToString();
            IFs[2].text = tempV3.z.ToString();
        }

        #endregion

        #region Vector2

        if (ObjVar.GetType() == typeof(Vector2))
        {
            InOutObject = Instantiate(InField_v2, ValueZone.transform);
            InOutObject.name = typeof(Vector2).FullName;
            TMP_InputField[] IFs = InOutObject.GetComponentsInChildren<TMP_InputField>();
            Vector2 tempV3 = (Vector2) ObjVar;
            IFs[0].text = tempV3.x.ToString();
            IFs[1].text = tempV3.y.ToString();
        }

        #endregion

        #region Bool

        if (ObjVar.GetType() == typeof(bool))
        {
            InOutObject = Instantiate(Toogle_bool, ValueZone.transform);
            InOutObject.name = typeof(bool).FullName;
            InOutObject.GetComponentInChildren<Toggle>().isOn = (Boolean) ObjVar;
        }

        #endregion

        #region List<float>

        if (ObjVar.GetType() == typeof(List<float>))
        {
            InOutObject = Instantiate(InField_x1, ValueZone.transform);
            InOutObject.name = typeof(List<float>).ToString();
            InOutObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Standard;
            List<float> newlist = (List<float>) ObjVar;
            foreach (float item in newlist)
            {
                InOutObject.GetComponent<TMP_InputField>().text += item.ToString() + " ";
            }
        }

        #endregion

        #region List<string>

        if (ObjVar.GetType() == typeof(List<string>))
        {
            InOutObject = Instantiate(InField_x1, ValueZone.transform);
            InOutObject.name = typeof(List<string>).ToString();
            InOutObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.Standard;
            List<string> newlist = (List<string>) ObjVar;
            foreach (string item in newlist)
            {
                InOutObject.GetComponent<TMP_InputField>().text += item + " ";
            }
        }

        #endregion

        #region Int

        if (ObjVar.GetType() == typeof(int))
        {
            InOutObject = Instantiate(InField_x1, ValueZone.transform);
            InOutObject.name = typeof(int).FullName;
            InOutObject.GetComponent<TMP_InputField>().text = ObjVar.ToString();
            InOutObject.GetComponent<TMP_InputField>().contentType = TMP_InputField.ContentType.IntegerNumber;
        }

        #endregion
    }
}