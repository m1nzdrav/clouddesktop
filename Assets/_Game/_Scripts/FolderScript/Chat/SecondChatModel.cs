using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class SecondChatModel : MonoBehaviour
{
    [SerializeField] private MainChat _mainChat;

    public MainChat MainChat => _mainChat;

    [SerializeField] private GameObject _answer;
    [SerializeField] private Transform parentAnswer;
    [SerializeField] private List<IAnswer> _answers = new List<IAnswer>(3);
    [SerializeField] private GameObject glowLoginBox;
    [SerializeField] private BtnSend btnSend;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private GameObject _customInputField;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private ICustomInput currentInput;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private CustomInputCell inputCellEight;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private CustomInputCell inputCellFour;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private CheckBoxInput checkBoxInput;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private NumberBoxInput numberBoxInput;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private DropdownInput dropdownInput;

    [SerializeField, FoldoutGroup("CustomInput"), BoxGroup]
    private ActivateCanvas canvasLoginBox;

    public SingleText CurrentTexts => _mainChat.CurrentTexts;

    private void SpawnCheckBox(List<string> variantAnswer, ErrorText errorText)
    {
        currentInput = Instantiate(checkBoxInput, parentAnswer).GetComponent<ICustomInput>();
        currentInput.Set(this, variantAnswer, errorText);
        canvasLoginBox.Activate();
        btnSend.DeSelect();
        _answers.Add(currentInput);
    }

    public void SpawnAnswer(List<string> variantAnswer, bool variant, ErrorText errorText)
    {
        if (variant)
            SpawnCheckBox(variantAnswer, errorText);
        else
            foreach (string VARIABLE in variantAnswer)
                SpawnAnswer(VARIABLE);
    }

    public void SpawnAnswer(string variantAnswer)
    {
        Answer answer = Instantiate(_answer, parentAnswer).GetComponent<Answer>();
        answer.SetAnswer(variantAnswer, this, _mainChat);
        // canvasLoginBox.Activate();
        _answers.Add(answer);
    }

    public void SpawnAnswer(ErrorText errorText)
    {
        currentInput = Instantiate(_customInputField, parentAnswer).GetComponent<CustomInput>();
        currentInput.Set(this, errorText);

        canvasLoginBox.Activate();
        btnSend.Select();
        _answers.Add(currentInput);
    }

    public void SpawnAnswer(int number, ErrorText errorText)
    {
        currentInput = number == 8
            ? Instantiate(inputCellEight, parentAnswer).GetComponent<CustomInputCell>()
            : Instantiate(inputCellFour, parentAnswer).GetComponent<CustomInputCell>();

        currentInput.Set(this, errorText);
        canvasLoginBox.Activate();
        btnSend.DeSelect();
        _answers.Add(currentInput);
    }

    public void SpawnAnswer(DropdownDialog dropdownDialog, bool withNumber, ErrorText errorText)
    {
        currentInput = withNumber
            ? Instantiate(numberBoxInput, parentAnswer).GetComponent<ICustomInput>()
            : Instantiate(dropdownInput, parentAnswer).GetComponent<ICustomInput>();

        currentInput.Set(this,
            new List<string>(3)
            {
                dropdownDialog.nameDropdown, dropdownDialog.nameField,
                dropdownDialog.takeFirstElement.IsNullOrWhitespace()
                    ? dropdownDialog.nameFirstElement
                    : _mainChat.GetSaveAnswer(dropdownDialog.takeFirstElement)
            }, errorText);
        _answers.Add(currentInput);

        canvasLoginBox.Activate();

        if (dropdownDialog.takeFirstElement.IsNullOrWhitespace())
            btnSend.DeSelect();
        else
            btnSend.Select();
    }

    // public void SpawnAnswer(DropdownDialog dropdownDialog,bool )
    // {
    //     currentInput = Instantiate(dropdownInput, parentAnswer).GetComponent<ICustomInput>();
    //
    //     currentInput.Set(this,
    //         new List<string>(3)
    //             { dropdownDialog.nameDropdown, dropdownDialog.nameField, dropdownDialog.nameFirstElement });
    //     
    //     canvasLoginBox.Activate();
    //     btnSend.DeSelect();
    //     _answers.Add(currentInput);
    // }

    public void BtnSendAnswer()
    {
        SendAnswer(currentInput.GetAnswer());
    }

    public void SelectBtnAnswer()
    {
        btnSend.Select();
        canvasLoginBox.OnInteractable();
        canvasLoginBox.OnRaycast();
        glowLoginBox.SetActive(true);
    }

    public void DeSelectBtnAnswer()
    {
        btnSend.DeSelect();
        canvasLoginBox.OffInteractable();
        canvasLoginBox.OffRaycast();
        glowLoginBox.SetActive(false);
    }

    public void SendAnswer(List<string> answer)
    {
        StartCoroutine(DeleteAnswer(answer));
    }

    public void ErrorText()
    {
        _mainChat.ErrorText();
    }

    private IEnumerator DeleteAnswer(List<string> answer)
    {
        yield return null;

        foreach (IAnswer VARIABLE in _answers)
        {
            VARIABLE.DestroyObject();
        }

        canvasLoginBox.DeActivate();
        btnSend.DeSelect();
        _answers.Clear();

        _mainChat.WriteAnswer(answer);
    }
}