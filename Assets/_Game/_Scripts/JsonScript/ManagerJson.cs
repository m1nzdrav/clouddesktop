using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Game._Scripts;
using _Game._Scripts.JsonScript;
using _Game._Scripts.JsonScript.JsonCreator;
using _Game._Scripts.Panel;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ManagerJson : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    [SerializeField] private bool needCreate;
    private Modify modifyFromToJson;
    [SerializeField] private LoginSceneArea _loginSceneArea;
    [SerializeField] private CreateModelPrefab main;
    public GameObject lastObject;
    [SerializeField] private string templateJsonUnity;
    public string TemplateJsonUnity => templateJsonUnity;

    [SerializeField] private string templateJsonData;

    public string TemplateJsonData => templateJsonData;
    [SerializeField] private string templateJsonParentChild;

    public string TemplateJsonParentChild => templateJsonParentChild;

    public List<string> listStartJson;

    [SerializeField] private List<DataForObject> jsonChilds;
    // private IpfsLoader ipfsLoader;

    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
    }
    public void Start()
    {
        // modifyFromToJson = gameObject.AddComponent<ModifyFromToJson>();
        modifyFromToJson = gameObject.AddComponent<ModifyLocal>();
        // LoadPaket loadPaket = new LoadPaket(Config.URL_TEMPLATE_JSON, new UnityEvent());
        // loadPaket.finitLoad.AddListener((() => AfterDownload(loadPaket)));

        modifyFromToJson.Set(new JsonCreator());


        IJsonCreator jsonCreator = new JsonCreator();
        jsonCreator.CreateDirectory();
        // _loginSceneArea?.New();
        // SetMainJson();
    }

    public void AfterDownload(LoadPaket answer)
    {
        // templateJson = answer.answer;
        // if (!needCreate)
        //     SetMainJson();
        // else
        //     _loginSceneArea.New();
    }

    /// <summary>
    /// Проверка наличия и первое создание рабочих столов 
    /// </summary>
    public async void SetMainJson()
    {
        
    }

  


    /// <summary>
    /// Оnправка запроса на создание объекта в modify
    /// </summary>
    /// <param name="jsonName"> название Json</param>
    /// <param name="prefabType">Тип префаба</param>
    /// <param name="path">путь до json</param>
    public void SetNew(Prefab prefabType, Parent parent, DataForObject dataForObject = null)
    {
        modifyFromToJson.SetNew(prefabType, parent, dataForObject);
    }

    public async Task SetMain(string jsonName, Parent parent)
    {
        UniTask query = modifyFromToJson.SetMain(jsonName, parent);
        UniTask.WhenAny(query);

        lastObject = modifyFromToJson.lastObject;
        // parent.SetChild(lastObject);
        // return modifyFromToJson.lastObject; //todo 
    }

    public GameObject GetNormal(string jsonName, Parent parent)
    {
        return SetNormal(jsonName, parent).Result;
    }

    public async Task<GameObject> SetNormal(string jsonName, Parent parent)
    {
        var query = modifyFromToJson.SetNormalJson(jsonName, parent);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        GameObject newObj = query.Result;
        // lastObject = modifyFromToJson.lastObject;
        return newObj;
        // return modifyFromToJson.SetNormalJson(jsonName, parent);
    }

    public void UpdateNewChapter(IdNameValue json, GameObject objName, bool needUpdateFolder = true)
    {
        modifyFromToJson.UpdateFromNewChapter(json, needUpdateFolder, objName);
    }

    public async void CreateChild(string jsonName, Parent parent)
    {
        await modifyFromToJson.CreateMiniObj(jsonName,
            parent);
    }

    ///Применение параметров из json в объект
    public void UpdateJson(GameObject obj)
    {
        modifyFromToJson.CopyToJson(obj);
    }

    public void SetChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        modifyFromToJson.SaveChildWithThread(nameChild, nameParent, isMain);
    }

    public void RemoveChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        modifyFromToJson.RemoveChildWithThread(nameChild, nameParent, isMain);
    }

    public void Test(string jsonName, GameObject jsonObject)
    {
        modifyFromToJson._stockJson.Set(jsonName, jsonObject);
    }
}