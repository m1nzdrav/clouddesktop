using System.Collections.Generic;
using System.Linq;
using _Game._Scripts;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class CheckBoxInput : MonoBehaviour, ICustomInput
{
    [SerializeField] private IconCheckBox _checkBoxObject;
    [SerializeField] private Transform parentCheckBox;
    [SerializeField] private SecondChatModel _secondChatModel;

    public SecondChatModel SecondChatModel => _secondChatModel;

    [SerializeField] private IconCheckBox currentCheckBox;
    [SerializeField] private List<IconCheckBox> iconCheckBoxes = new List<IconCheckBox>(5);
    [SerializeField] private List<IconCheckBox> lockIconCheckBoxes;

    public void Set(SecondChatModel secondChatModel,ErrorText errorText)
    {
        _secondChatModel = secondChatModel;
    }

    public void Set(SecondChatModel secondChatModel, List<string> variant,ErrorText errorText)
    {
        lockIconCheckBoxes = new List<IconCheckBox>(variant.Capacity);
        Set(secondChatModel,errorText);

        foreach (var VARIABLE in variant)
        {
            currentCheckBox = Instantiate(_checkBoxObject, parentCheckBox).GetComponent<IconCheckBox>();
            currentCheckBox.Set(VARIABLE, this);
            iconCheckBoxes.Add(currentCheckBox);
        }
    }

    public List<string> GetAnswer()
    {
        return GetAllAnswer();
    }

    private List<string> GetAllAnswer()
    {
        List<string> checkBoxes = new List<string>(iconCheckBoxes.Count);
        checkBoxes.AddRange(from VARIABLE in iconCheckBoxes
            where !VARIABLE.Get().IsNullOrWhitespace()
            select VARIABLE.Get());

        return checkBoxes;

        // return iconCheckBoxes.Aggregate("", (current, VARIABLE) => current + VARIABLE.Get());
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SelectText(IconCheckBox checkBox)
    {
        lockIconCheckBoxes.Add(checkBox);
        _secondChatModel.SelectBtnAnswer();
    }

    public void DeSelect(IconCheckBox checkBox)
    {
        lockIconCheckBoxes.Remove(checkBox);
        if (lockIconCheckBoxes.Count == 0)
        {
            _secondChatModel.DeSelectBtnAnswer();
        }
    }
}