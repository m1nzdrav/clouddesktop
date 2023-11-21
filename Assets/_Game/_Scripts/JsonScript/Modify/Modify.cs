using System;
using System.Threading.Tasks;
using _Game._Scripts.JsonScript.Stock;
using _Game._Scripts.Panel;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public abstract class Modify : MonoBehaviour
{
    public GameObject lastObject;
    public StockJson _stockJson;
    public abstract void SaveChildWithThread(string nameChild, string nameParent, bool isMain);
    public abstract void RemoveChildWithThread(string nameChild, string nameParent, bool isMain);
    public abstract void CopyToJson(GameObject obj);
    public abstract UniTask<GameObject> CreateMiniObj(string jsonName, Parent parent = null);
    public abstract void UpdateFromNewChapter(IdNameValue newJson, bool needUpdateFolder, GameObject jsonObject);
    public abstract Task<GameObject> SetNormalJson(string jsonName, Parent parent);
    public abstract UniTask SetMain(string jsonName, Parent parent);
    public abstract void SetNew(Prefab prefabType, Parent parent, DataForObject dataForObject = null);
    public abstract void Set(IJsonCreator creator, LoadPaket loadPaket=null);
}