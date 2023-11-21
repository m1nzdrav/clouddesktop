using System;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using UnityEngine;

public class PrefabStock : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
    }
    [SerializeField] private List<PrefabName> listPrefabs;

    public GameObject CheckPrefab(Prefab prefab)
    {
        try
        {
            return listPrefabs.Find(x => x.Prefab == prefab).gameObject;
        }
        catch (Exception e)
        {
            Debug.LogError(prefab.ToString());
            throw;
        }
    }
}