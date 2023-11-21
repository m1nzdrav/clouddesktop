using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ComponentView : MonoBehaviour
{
    [SerializeField] private GameObject _dropdown;
    [SerializeField] private IdNameValue currentJson;

    public IdNameValue CurrentJson
    {
        get => currentJson;
        set => currentJson = value;
    }

    [SerializeField] private List<ComponentView> childComponent = new List<ComponentView>();

    public List<ComponentView> ChildComponent
    {
        get => childComponent;
        set { childComponent = value; }
    }

    [SerializeField] private ComponentView parent;

    public ComponentView Parent
    {
        get => parent;
        set => parent = value;
    }

    [SerializeField] private GameObject rowsPrefabe;
    [SerializeField] private GameObject parentRows;

    [SerializeField] private ModelForm currentForm;


    [SerializeField] private ComponentValue name, value;

    public ComponentValue Name => name;

    public ComponentValue Value => value;

    [SerializeField] private String nameStr, valueStr;
    [SerializeField] private int numRows;
    [SerializeField] private Button btn;


    public Button Btn
    {
        get => btn;
        set => btn = value;
    }

    public int NumRows
    {
        get => numRows;
        set => numRows = value;
    }


    [SerializeField] private bool justPress = false;


    public void BtnClick(ComponentValue sender)
    {
        if (!justPress)
        {
            AddParentInChildComponent();
//            Debug.LogError(currentJson.MYTValue[0].MYTValue.Count);
//            try
//            {
            currentForm.CreateChild(this, 0);
//            }
//            catch (Exception e)
//            {
//                Debug.LogError(e);
//            }

            justPress = true;
        }
    }

    public void AddParentInChildComponent()
    {
        foreach (var VARIABLE in GetComponent<LineInList>().ChildLine)
        {
            VARIABLE.GetComponent<ComponentView>().parent = this;
        }
    }

    public void UpdateInJson(IdNameValue json = null)
    {
        IdNameValue newJson = new IdNameValue();
        if (json == null)
        {
//            Debug.LogError("test");
            nameStr = name.Value.text;
            currentJson.MYTName = nameStr;

            valueStr = value.Value.text;
//            Debug.LogError(valueStr);
            currentJson.MYTValue[0].MYTName = valueStr;
            newJson = currentJson;
        }
        else
        {
            newJson = new IdNameValue() {MYTName = currentJson.MYTName};
            newJson.MYTValue.Add(json);
        }

        if (parent != null)
        {
            parent.UpdateInJson(newJson);
        }
        else
        {
            currentForm.UpdateJson(newJson,true);
        }
    }

    public void SetNameValuesOnPrefabe(ModelForm form, IdNameValue json, int _numRows, String _name,
        string _value = null)
    {
        if (string.IsNullOrEmpty(_name))
        {
            Destroy(gameObject);
        }

        currentForm = form;
        CurrentJson = json;
        name.Value.text = _name;
        nameStr = _name;
        valueStr = _value;

        if (_value == null)
        {
            if (json.MYTValue.Count == 0)
            {
                value.Value.text = "- - -";
                justPress = true;
                btn.interactable = false;
                value.Btn.interactable = false;
            }
            else

                value.gameObject.SetActive(false);
        }
        else
        {
            justPress = true;
            value.Value.text = _value;
            Name.Btn.interactable = false;
            if (_value == "False" || _value == "True")
            {
                if (_value == "True")
                {
                    Value.Btn.GetComponent<Image>()
                        .color = Color.blue;
                }
                else
                {
                    Value.Btn.GetComponent<Image>()
                        .color = new Color(0, 0, 0, 0);
                }

                Destroy(Value.Btn.gameObject.GetComponent<CircleMenuButtonController>());

                value.Value.interactable = false;
                value.Btn.onClick.AddListener(value.OnValueChanged);
            }
            else if (json.MYTValue.Count > 1)
            {
                SetLongValue(json);
            }
            else
            {
                value.Btn.interactable = false;
            }
        }


        numRows = _numRows;
        for (int i = 0; i < _numRows; i++)
        {
            Instantiate(rowsPrefabe, parentRows.transform).transform.SetAsFirstSibling();
        }

        GetComponent<LineInList>().NeedShowing = json.OpenStatus;
        try
        {
            if (json.MYTValue.Count > 0 && _value == null && !string.IsNullOrEmpty(json.MYTValue[0].MYTName))
            {
                name.CountChild.gameObject.SetActive(true);
                name.CountChild.text = json.MYTValue.Count.ToString();
            }
        }
        catch (Exception e)
        {
        }
    }

    private void SetLongValue(IdNameValue json)
    {
        CreateExValue(valueStr);

        for (int i = 1; i < json.MYTValue.Count; i++)
        {
            value.Value.text += ", " + json.MYTValue[i].MYTName;
            CreateExValue(json.MYTValue[i].MYTName);
        }

        Value.Btn.GetComponent<Image>()
            .color = Color.blue; //todo переделать на вызов цвета у родителя
        Value.CountChild.gameObject.SetActive(true);
        Value.CountChild.text = json.MYTValue.Count.ToString();
    }

    private void CreateExValue(string _name)
    {
        ComponentView obj = Instantiate(_dropdown, transform.parent).GetComponent<ComponentView>();
        obj.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        childComponent.Add(obj);
        obj.SetOnlyValue(numRows, _name);
        GetComponent<LineInList>().ListToChildLine(obj.gameObject);
    }

    public void SetOnlyValue(int _numRows, String _name)
    {
        numRows = _numRows + 1;
        for (int i = 0; i < numRows; i++)
        {
            Instantiate(rowsPrefabe, parentRows.transform).transform.SetAsFirstSibling();
        }

        valueStr = _name;
        value.Value.text = _name;
    }

//    private void OnDestroy()
//    {
//        Debug.LogError("Destroy all child");
//        foreach (var VARIABLE in childComponent)
//        {
//            Destroy(VARIABLE.gameObject);
//        }
//
//        Debug.LogError("Complit");
//    }
}