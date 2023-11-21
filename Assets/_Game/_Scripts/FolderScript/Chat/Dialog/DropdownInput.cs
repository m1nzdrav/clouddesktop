using System.Collections.Generic;
using System.Linq;
using _Game._Scripts;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;


public class DropdownInput : MonoBehaviour, ICustomInput
{
    [SerializeField] private CustomDropdown customDropdown;
    [SerializeField] private Sprite pinkSprite;
    [SerializeField] private bool isOneAnswer = true;
    [SerializeField] private SecondChatModel secondChatModel;
    [SerializeField] private Country country;

    public void Set(SecondChatModel secondChatModel, ErrorText errorText)
    {
        this.secondChatModel = secondChatModel;
    }

    public void Set(SecondChatModel secondChatModel, List<string> variant, ErrorText errorText)
    {
        Set(secondChatModel, errorText);
        country = Config.LoadJson<Country>(variant[0]);

        if (variant[1] == "name")
        {
            foreach (CountryCode variable in country.countryCodes)
            {
                customDropdown.CreateNewItem(variable.name, pinkSprite);
            }
        }

        if (variant[1] == "code")
        {
            foreach (CountryCode variable in country.countryCodes)
            {
                customDropdown.CreateNewItem(variable.code, pinkSprite);
            }
        }

        if (variant[1] == "dialCode")
        {
            foreach (CountryCode variable in country.countryCodes)
            {
                customDropdown.CreateNewItem(variable.name + " " + variable.dial_code, pinkSprite);
            }
        }

        customDropdown.selectedText.text = variant[2];
        customDropdown.itemObject.GetComponentInChildren<Text>().font = secondChatModel.MainChat.Font;
        customDropdown.selectedText.font = secondChatModel.MainChat.Font;
        customDropdown.SetupDropdown();
    }

    public List<string> GetAnswer()
    {
        return GetAllAnswer();
    }

    private List<string> GetAllAnswer()
    {
        return new List<string>() { customDropdown.selectedText.text };
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void Select()
    {
        secondChatModel.SelectBtnAnswer();
    }
}