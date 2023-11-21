using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class CustomInputCell : MonoBehaviour, ICustomInput
{
    [SerializeField] private KeyCode deleteAndReturn = KeyCode.Backspace;
    [SerializeField] private InputField answer;
    [SerializeField] private bool isOneAnswer = true;
    [SerializeField] private SecondChatModel _secondChatModel;
    [SerializeField] private List<InputField> _inputFields;
    [SerializeField] private List<IconInput> _iconInputs;
    [SerializeField] private List<GameObject> _selectGlow;
    [SerializeField] private ErrorText errorText;

    [SerializeField] private bool error;

    public void Set(SecondChatModel secondChatModel, ErrorText errorText)
    {
        _secondChatModel = secondChatModel;
        _iconInputs[0].Select();
        _selectGlow[0].SetActive(true);
        this.errorText = errorText;
    }

    public void Set(SecondChatModel secondChatModel, List<string> variant, ErrorText errorText)
    {
        Set(secondChatModel, errorText);
        this.errorText = errorText;
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

    public void NextInput(int number)
    {
        if (error)
        {
            error = false;
            Reload();
            return;
        }

        if (number + 1 == _iconInputs.Count)
        {
            _secondChatModel.SelectBtnAnswer();
            return;
        }

        _selectGlow[number].SetActive(false);

        _selectGlow[number + 1].SetActive(true);
        _iconInputs[number + 1].Select();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(deleteAndReturn) && _inputFields.Exists(x => x.isFocused))
            Reload();
    }

    public string GetOneAnswer()
    {
        return GetAnswer().Aggregate("", (current, VARIABLE) => current + VARIABLE);
    }

    public bool EnterText(string text)
    {
        if (!errorText.Validate(text[text.Length - 1]))
        {
            // error = true;
            _secondChatModel.ErrorText();
            Reload();
            return false;
        }

        return true;
    }

    private void Reload()
    {
        ReloadWithoutNotify();
        _iconInputs[0].Select();
        _selectGlow[0].SetActive(true);
    }

    public void ReloadWithoutNotify()
    {
        for (int i = 0; i < _iconInputs.Count ; i++)
        {
            _inputFields[i].SetTextWithoutNotify("");
            _selectGlow[i].SetActive(false);
        }
        _secondChatModel.DeSelectBtnAnswer();
    }
}