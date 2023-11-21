using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginController : MonoBehaviour
{
    [SerializeField] private OpenIconContainer _iconContainer;

    public void SettingsForLoginScene()
    {
        _iconContainer?.SetSettings();
        _iconContainer.Close();
    }
}
