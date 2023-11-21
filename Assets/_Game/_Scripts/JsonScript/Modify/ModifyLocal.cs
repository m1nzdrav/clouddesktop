using System.Collections.Generic;
using System.Threading.Tasks;
using _Game._Scripts;
using _Game._Scripts.JsonScript.Stock;
using _Game._Scripts.Panel;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ModifyLocal : Modify
{
    [SerializeField] private List<DataForObject> _dataForObjects;

    public override void Set(IJsonCreator creator, LoadPaket loadPaket)
    {
        _stockJson = new StockJson();
        // RegisterSingleton._instance.CoroutineServerLoader.StartDownload(loadPaket);
    }

    public override void CopyToJson(GameObject obj)
    {
    }

    public override async UniTask<GameObject> CreateMiniObj(string jsonName, Parent parent = null)
    {
        DataForObject dataForObject = new DataForObject();

        dataForObject.AddNewFields(mainPrefab: Prefab.MYTIcon60_60,
            color: Color.blue,
            name: Config.DEFAULT_NAME_FOLDER);

        GameObject obj = Factory.CreateObj(Prefab.MYTIcon60_60);
        ObjSetter.SetObj(dataForObject, parent, obj);
        obj.name = jsonName;

        return obj;
    }

    public override void SetNew(Prefab prefabType, Parent parent, DataForObject dataForObject = null)
    {
        if (dataForObject == null)
        {
            dataForObject = new DataForObject();
        }

        dataForObject.AddNewFields(name: Config.DEFAULT_NAME_FOLDER,
            color: parent.My.GetComponent<MytPrefabColorTypes>().LastSelectedColor,
            mainPrefab: prefabType);

        GameObject obj = Factory.CreateObj(Prefab.MYTIcon60_60);
        ObjSetter.SetObj(dataForObject, parent, obj);
        _stockJson.Set(obj.name, obj);
    }

    public override async UniTask SetMain(string jsonName, Parent parent)
    {
        DataForObject dataForObject = new DataForObject();

        dataForObject.AddNewFields(
            color: parent.My.GetComponent<MytPrefabColorTypes>().LastSelectedColor,
            name: Config.DEFAULT_NAME_FOLDER,
            mainPrefab: _stockJson.GetObjects(jsonName)
                .FindLast(x => x.GetComponent<PrefabName>().Prefab == Prefab.MYTIcon60_60).GetComponent<IconModel>()
                .MainPrefab);

        GameObject obj = Factory.CreateObj(dataForObject.mainPrefab);
        ObjSetter.SetObj(dataForObject, parent, obj);
        obj.name = jsonName;

        _stockJson.Set(obj.name, obj);
        lastObject = obj;
    }

    public override async Task<GameObject> SetNormalJson(string jsonName, Parent parent)
    {
        DataForObject dataForObject = new DataForObject();

        IconModel icon = _stockJson.GetObjects(jsonName)
            .FindLast(x => x.GetComponent<PrefabName>().Prefab == Prefab.MYTIcon60_60).GetComponent<IconModel>();

        Debug.LogError(icon.ParentHierarchy.DataForObject.color);
        dataForObject.AddNewFields(mainPrefab: icon.MainPrefab,
            color: icon.GetComponent<Image>().color,
            name: Config.DEFAULT_NAME_FOLDER);

        GameObject obj = Factory.CreateObj(
            icon.ParentHierarchy.DataForObject.mainPrefab);
        ObjSetter.SetObj(icon.ParentHierarchy.DataForObject, parent, obj);
        obj.name = jsonName;

        _stockJson.Set(obj.name, obj);
        lastObject = obj;
        return obj;
    }

    public override void SaveChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateFromNewChapter(IdNameValue newJson, bool needUpdateFolder, GameObject jsonObject)
    {
        throw new System.NotImplementedException();
    }

    [Button]
    private void CheckStock()
    {
        _stockJson.Check();
    }
}