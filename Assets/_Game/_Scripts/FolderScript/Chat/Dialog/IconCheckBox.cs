using _Game._Scripts;
using UnityEngine;
using UnityEngine.UI;

public class IconCheckBox : MonoBehaviour
{
    [SerializeField] private Text textName;
    [SerializeField] private bool choose;
    [SerializeField] private CheckBoxInput currentCheckBoxInput;
    public bool Choose => choose;

    [SerializeField] private GameObject glowSelected;

    public void ChangeState()
    {
        if (choose)
            DeSelect();
        else
            SelectTest();
    }

    private void SelectTest()
    {
        glowSelected.SetActive(true);
        choose = true;
        currentCheckBoxInput.SelectText(this);
    }

    private void DeSelect()
    {
        glowSelected.SetActive(false);
        choose = false;
        currentCheckBoxInput.DeSelect(this);
    }

    public void Set(string nameCheckBox, CheckBoxInput currentCheckBoxInput)
    {
        textName.text = nameCheckBox;
        textName.font = currentCheckBoxInput.SecondChatModel.MainChat.Font;
        this.currentCheckBoxInput = currentCheckBoxInput;
    }

    public string Get()
    {
        return choose ? textName.text : "";
    }
}