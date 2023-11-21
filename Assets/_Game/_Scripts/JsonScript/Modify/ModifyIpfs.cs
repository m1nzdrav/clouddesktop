
using System.Threading.Tasks;
using _Game._Scripts.Panel;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ModifyIpfs : Modify

{
    public override void SaveChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        throw new System.NotImplementedException();
    }

    public override void CopyToJson(GameObject obj)
    {
        throw new System.NotImplementedException();
    }

    public override UniTask<GameObject> CreateMiniObj(string jsonName, Parent parent = null)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateFromNewChapter(IdNameValue newJson, bool needUpdateFolder, GameObject jsonObject)
    {
        throw new System.NotImplementedException();
    }

    public override Task<GameObject> SetNormalJson(string jsonName, Parent parent)
    {
        throw new System.NotImplementedException();
    }

    public override UniTask SetMain(string jsonName, Parent parent)
    {
        throw new System.NotImplementedException();
    }

    public override void SetNew(Prefab prefabType, Parent parent, DataForObject dataForObject = null)
    {
        throw new System.NotImplementedException();
    }

    public override void Set(IJsonCreator creator, LoadPaket loadPaket)
    {
        throw new System.NotImplementedException();
    }
}