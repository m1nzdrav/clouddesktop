using System;

[Serializable]
public class DropdownDialog
{
    /// <summary>
    /// Сейчас доступно только 3 поля ( они есть в CountryCode)
    /// </summary>
    public string nameDropdown;
    public string nameField;
    public string nameFirstElement;
    public bool withNumber;
    public string takeFirstElement;
}