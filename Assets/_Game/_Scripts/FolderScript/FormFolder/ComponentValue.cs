using System;
using UnityEngine;
using UnityEngine.UI;

public class ComponentValue : MonoBehaviour
{
    
    [SerializeField] private InputField value;
    [SerializeField] private bool isReadOnly;
    [SerializeField] private Text countChild;
    [SerializeField] private Transform parentDropDown;
    [SerializeField] private Button btn;
    

    public InputField Value
    {
        get => value;
        set => this.value = value;
    }

    public bool IsReadOnly
    {
        get => isReadOnly;
        set => this.value.readOnly = value;
    }

    public Text CountChild
    {
        get => countChild;
        set => countChild = value;
    }

    public Button Btn => btn;


    public void OpenChildLine()
    {
//        GetComponent<LineInList>().ShowHideLine();
        transform.parent. GetComponent<LineInList>().ShowHideLine();
    }

    public GameObject AddItemDropdown(String itemName,GameObject Dropdown)
    {
        
        ComponentValue obj = Instantiate(Dropdown, parentDropDown).GetComponent<ComponentValue>();
        obj.value.text = itemName;
//        Debug.LogError(itemName);
       transform.parent. GetComponent<LineInList>().ListToChildLine(obj.gameObject);
        return obj.gameObject;
    }

    public void OnValueChanged()
    {
        ChangeBool();
        transform.parent.GetComponent<ComponentView>().UpdateInJson();
    }

   void ChangeBool()
    {
//        Debug.LogError(Value.text);
        if (Value.text=="False")
        {
//            Debug.LogError(Value.text);
            Value.text = "True";
            Btn.GetComponent<Image>()
                .color = Color.blue;
//            Debug.LogError(Value.text);
        }else

        if (Value.text=="True")
        {
             Value.text = "False";
             Btn.GetComponent<Image>()
                 .color = new Color(0,0,0,0);
        }
    }
}