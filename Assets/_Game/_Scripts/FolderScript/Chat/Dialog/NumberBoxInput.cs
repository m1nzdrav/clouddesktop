using System.Collections.Generic;
using System.Linq;
using _Game._Scripts;
using Michsky.UI.ModernUIPack;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class NumberBoxInput : MonoBehaviour, ICustomInput
{
    [SerializeField] private SecondChatModel _secondChatModel;
    [SerializeField] private DropdownInput _dropdownInput;
    [SerializeField] private InputField numberInputField;
    [SerializeField] private ErrorText errorText;

    public void Set(SecondChatModel secondChatModel, ErrorText errorText)
    {
        _secondChatModel = secondChatModel;
    }

    public void Set(SecondChatModel secondChatModel, List<string> variant, ErrorText errorText)
    {
        Set(secondChatModel, errorText);
        this.errorText = errorText;
        numberInputField.textComponent.font = secondChatModel.MainChat.Font;
        _dropdownInput.Set(secondChatModel, variant, errorText);
        numberInputField.Select();
    }

    public List<string> GetAnswer()
    {
        return GetAllAnswer();
    }

    private List<string> GetAllAnswer()
    {
        List<string> checkBoxes = new List<string>(1)
            { _dropdownInput.GetAnswer()[0].Split(' ')[1] + " " + numberInputField.text };
        return checkBoxes;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SelectText(IconCheckBox checkBox)
    {
        // lockIconCheckBoxes.Add(checkBox);
        _secondChatModel.SelectBtnAnswer();
    }

    public void DeSelect(IconCheckBox checkBox)
    {
        // lockIconCheckBoxes.Remove(checkBox);
        // if (lockIconCheckBoxes.Count == 0)
        // {
        //     _secondChatModel.DeSelectBtnAnswer();
        // }
    }
}