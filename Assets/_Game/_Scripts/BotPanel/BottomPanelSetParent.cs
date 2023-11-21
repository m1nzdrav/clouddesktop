using UnityEngine;

public class BottomPanelSetParent : MonoBehaviour
{
    [SerializeField] private OpenManager _openManager;
    [SerializeField] private Transform _mainDesktop;

    private Transform _prefParent;

    public void SetParent()
    {
        var parent = _openManager.CurrentArea;

        if (parent == null) return;

        _prefParent = transform.parent;
        transform.SetParent(parent.transform);
    }

    public void SetPrew()
    {
        if (_prefParent == null) _prefParent = _mainDesktop;

        transform.SetParent(_prefParent);
    }
}
