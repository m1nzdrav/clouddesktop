using UnityEngine;
using UnityEngine.UI;

public class UserLineInfo : MonoBehaviour
{
    [SerializeField] private GameObject icon;
    [SerializeField] private InputField text;
    [SerializeField] private CoreValue coreValue;

    public UserLineInfo()
    {
    }

    public UserLineInfo(string text, CoreValue coreValue)
    {
        this.text.text = text;
        this.coreValue = coreValue;
    }


}