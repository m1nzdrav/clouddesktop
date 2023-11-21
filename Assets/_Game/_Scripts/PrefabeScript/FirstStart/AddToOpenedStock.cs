using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using UnityEngine;

[RequireComponent(typeof(PrefabName))]
public class AddToOpenedStock : MonoBehaviour
{
    [SerializeField] private PrefabName _prefabName;

    private void Start()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.SetOpenedPrefab(_prefabName);
    }
}