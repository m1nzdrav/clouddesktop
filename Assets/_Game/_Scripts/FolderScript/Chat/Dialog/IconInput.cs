using UnityEngine;
using UnityEngine.UI;

public class IconInput : MonoBehaviour
{
    [SerializeField] private CustomInputCell customInputCell;
    [SerializeField] private InputField inputField;
    [SerializeField] private int maxCharacter = 1;
    [SerializeField] private int number;
    [SerializeField] private bool isChange = false;


    public void Select()
    {
        inputField.Select();
        isChange = false;
    }

    public void EndInput()
    {
        if (inputField.text.Length != maxCharacter || !customInputCell.EnterText(inputField.text)) return;
        
        isChange = true;
        customInputCell.NextInput(number);
    }

    public void End()
    {
        if (isChange)
            return;

        customInputCell.ReloadWithoutNotify();
    }
}