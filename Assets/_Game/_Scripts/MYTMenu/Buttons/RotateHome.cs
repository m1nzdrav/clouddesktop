using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonRotateGlow), typeof(Button))]
public class RotateHome : MonoBehaviour
{
    [SerializeField] private MYTMenu _mytMenu;
    

    public void GoHome()
    {
        _mytMenu.GoHome(GetComponent<ButtonRotateGlow>());
    }

   
}