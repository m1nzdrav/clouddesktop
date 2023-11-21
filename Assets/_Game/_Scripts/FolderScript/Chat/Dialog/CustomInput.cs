using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomInput : MonoBehaviour, ICustomInput
{
    [SerializeField] private InputField answer;
    [SerializeField] private bool isOneAnswer = true;
    [SerializeField] private SecondChatModel _secondChatModel;
    [SerializeField] private List<InputField> _inputFields;
    [SerializeField] private ErrorText errorText;

    public void Set(SecondChatModel secondChatModel,ErrorText errorText)
    {
        _secondChatModel = secondChatModel;
        _inputFields[0].Select();
        this.errorText = errorText;
        
        if (this.errorText.accessTexts!=null && this.errorText.accessTexts.Length>0)
        {
            foreach (var VARIABLE in _inputFields)
            {
                VARIABLE.onValueChanged.AddListener(EnterText); 
            }
        }
    }

    public void Set(SecondChatModel secondChatModel, List<string> variant, ErrorText errorText)
    {
        Set(secondChatModel,errorText);
    }

    public string GetOneAnswer()
    {
        return GetAnswer().Aggregate("", (current, VARIABLE) => current + VARIABLE);
    }

    public List<string> GetAnswer()
    {
        return GetAllAnswer();
    }

    private List<string> GetAllAnswer()
    {
        return new List<string>() { _inputFields.Aggregate("", (current, VARIABLE) => current + VARIABLE.text) };
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void EnterText(string text)
    {
        if (!errorText.Validate(text[text.Length - 1]))
        {
            _secondChatModel.ErrorText();
            _inputFields[0].SetTextWithoutNotify(_inputFields[0].text.Remove(_inputFields[0].text.Length - 1));
        }
    }

    public void NextInput(int number)
    {
        if (number + 1 == _inputFields.Count)
        {
            _secondChatModel.SelectBtnAnswer();
            return;
        }

        _inputFields[number + 1].Select();
    }
    
    public void Select()
    {
        _secondChatModel.SelectBtnAnswer();
    }
}