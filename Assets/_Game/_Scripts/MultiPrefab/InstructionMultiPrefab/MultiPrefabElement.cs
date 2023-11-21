using UnityEngine;

public class MultiPrefabElement : MonoBehaviour
{
    [SerializeField] private MultiPrefab _multiPrefab;

    public MultiPrefab MultiPrefab
    {
        get => _multiPrefab;
        set => _multiPrefab = value;
    }
}