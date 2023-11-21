using UnityEngine;

[RequireComponent(typeof(CircleSettings))]
public class IrRegularStartCircle : MonoBehaviour
{
    [SerializeField] private CircleSettings _circleSettings;

    private void Start()
    {
        _circleSettings.CircleFactory.AddCircle(_circleSettings);
        _circleSettings.CircleFactory.ShowCircle(_circleSettings);
    }
}