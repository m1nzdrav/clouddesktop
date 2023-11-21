using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonInvoke : MonoBehaviour
{
    [SerializeField] private Button button;

    public void Invoke()
    {
        button.onClick.Invoke();
    }
}