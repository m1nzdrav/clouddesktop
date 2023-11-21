using UnityEngine;

public class TextToName : MonoBehaviour
{
    IconModel currentIconModel;
    [SerializeField] Re_InputField Name_inpField;

    public IconModel Icon 
    {
        set 
        {
            currentIconModel = value;
            Name_inpField.SetTextWithoutNotify(currentIconModel.CurrentName);
            currentIconModel.EventName.onNameChanged += OnNameChanged_TextToName;
        }
    }

    public void Converter()
    {
        currentIconModel.EventName.SetName(Name_inpField.text);

        //folderModel.ChangeName(_text.text,true);
//        FindObjectOfType<ManagerJson>().UpdateJson(folderModel.gameObject);
    }

    public void OnNameChanged_TextToName(string newName) 
    {
        Name_inpField.text = newName;
    }
}
