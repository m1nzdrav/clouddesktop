using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using _Game._Scripts;
using _Game._Scripts.JsonScript.Stock;
using _Game._Scripts.Panel;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class ModifyFromToJson : Modify
{ 
    public override void Set(IJsonCreator creator, LoadPaket loadPaket)
    {
        _jsonCreator = creator;

        _get = new CreateJsonFromObj();
        _set = new CreateObjFromJson();
        _stockJson = new StockJson();

        (RegisterSingleton._instance.GetSingleton(typeof(CoroutineServerLoader)) as CoroutineServerLoader)?.StartDownload(loadPaket);
    }

    public override void CopyToJson(GameObject obj)
    {
        string pathToJson;

        pathToJson = obj.GetComponent<PrefabName>().Prefab == Prefab.MYTDesktop
            ? Config.PATH_TO_MAIN_JSON
            : Config.PATH_TO_DIRECTORY_JSON;

        OnSetJsonParameters(obj, GetDesktopJson(obj.name, pathToJson) as JsonUnity);
    }

    private IJsonCreator _jsonCreator;

    private CreateObjFromJson _createObjFromJson;

    private CreateJsonFromObj _get;

    private CreateObjFromJson _set;

    private JsonUnity _jsonUnity;

    private string currentJson;

    /// <summary>
    /// Создание объекта по существующему json
    /// </summary>
    /// <param name="jsonName"></param>
    /// <param name="parent"></param>
    /// <param name="path"></param>
    public override async UniTask<GameObject> CreateMiniObj(string jsonName, Parent parent = null)
    {
        var query = CheckExistJsonAsync("JsonUnity" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonUnity json = JsonUtility.FromJson<JsonUnity>(query.Result);

        query = CheckExistJsonAsync("JsonParentChild" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonParentChild jsonParentChild = JsonUtility.FromJson<JsonParentChild>(query.Result);
        query = CheckExistJsonAsync("JsonData" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonData jsonData = JsonUtility.FromJson<JsonData>(query.Result);
        GameObject obj =Factory.CreateObj(Prefab.MYTIcon60_60); 
        ObjSetter.SetObj(new DataForObject(json, jsonParentChild, jsonData), parent, obj);
        obj.name = jsonName;
        _stockJson.Set(jsonName, obj);
        OnSetJsonParameters(obj, json);

        return obj;
    }

    public override void SetNew(Prefab prefabType, Parent parent, DataForObject dataForObject = null)
    {
        JsonUnity jsonUnity = new JsonUnity(prefabType, parent); //new json
        JsonData jsonData = new JsonData(true);
        JsonParentChild jsonParentChild = new JsonParentChild(parent);
        GameObject obj =Factory.CreateObj(Prefab.MYTIcon60_60); 
        ObjSetter.SetObj(new DataForObject(jsonUnity, jsonParentChild, jsonData), parent, obj);
        _stockJson.Set(obj.name, obj);
        //Поиск информации о префабе // определение формата json

        jsonUnity = FileToJsonObj(obj, jsonUnity);


        // SaveJsonCoroutine("JsonUnity" + obj.name, jsonUnity);
        // SaveJsonCoroutine("JsonData" + obj.name, jsonData);
        // SaveJsonCoroutine("JsonParentChild" + obj.name, jsonParentChild);


        SaveJson("JsonUnity" + obj.name, Config.PATH_TO_MAIN_JSON, jsonUnity);
        SaveJson("JsonData" + obj.name, Config.PATH_TO_MAIN_JSON, jsonData);
        SaveJson("JsonParentChild" + obj.name, Config.PATH_TO_MAIN_JSON, jsonParentChild);

        SetChild(parent, obj.name);
    }


    //todo надо определять что за префаб и отправлять сюду уже путь до json
    public override async UniTask SetMain(string jsonName, Parent parent)
    {
        var query = CheckExistJsonAsync("JsonUnity" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonUnity jsonUnity = JsonUtility.FromJson<JsonUnity>(query.Result);

        Prefab mainPrefab = Config.GetFormatJson(jsonUnity);

        query = CheckExistJsonAsync("JsonParentChild" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonParentChild jsonParentChild = JsonUtility.FromJson<JsonParentChild>(query.Result);

        query = CheckExistJsonAsync("JsonData" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonData jsonData = JsonUtility.FromJson<JsonData>(query.Result);
        GameObject obj =Factory.CreateObj(mainPrefab); 
        ObjSetter.SetObj(new DataForObject(jsonUnity, jsonParentChild, jsonData), parent, obj);
        obj.name = jsonName;
        _stockJson.Set(obj.name, obj);
        OnCreateJson(obj.name, obj, jsonUnity, parent);
        lastObject = obj;
    }

    public override async Task<GameObject> SetNormalJson(string jsonName, Parent parent)
    {
        var query = CheckExistJsonAsync("JsonUnity" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonUnity jsonUnity = JsonUtility.FromJson<JsonUnity>(query.Result);

        Prefab mainPrefab = Config.GetFormatJson(jsonUnity);


        query = CheckExistJsonAsync("JsonParentChild" + jsonName, Config.PATH_TO_MAIN_JSON);
        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonParentChild jsonParentChild = JsonUtility.FromJson<JsonParentChild>(query.Result);


        query = CheckExistJsonAsync("JsonData" + jsonName, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonData jsonData = JsonUtility.FromJson<JsonData>(query.Result);

        GameObject obj = Factory.CreateObj(mainPrefab);
        ObjSetter.SetObj(new DataForObject(jsonUnity, jsonParentChild, jsonData), parent, obj);
        obj.name = jsonName;

        _stockJson.Set(obj.name, obj);

            
        SaveJson("JsonUnity" + obj.name, Config.PATH_TO_MAIN_JSON, jsonUnity);
        // OnCreateJson(obj.name, obj, jsonUnity, parent);
        lastObject = obj;
        return obj;
    }

    //todo
    public JsonClass GetDesktopJson(string jsonName, string path)
    {
        return JsonUtility.FromJson<JsonClass>(CheckExistJson(jsonName, path));
    }

    public void SetNameJson(string jsonName, string path, string name)
    {
        JsonClass json = GetDesktopJson(jsonName, path);
        json.MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == "MYTDefaultName").MYTValue[0].MYTName = name;

        File.WriteAllText(_jsonCreator.SetJsonName(jsonName, path), JsonUtility.ToJson(json));
    }

    public string GetNameJson(string jsonName, string path)
    {
        JsonUnity json = GetDesktopJson(jsonName, path) as JsonUnity;
        return json.MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == "MYTDefaultName").MYTValue[0].MYTName;
    }

    private string CheckExistJson(string jsonName = null, string directory = null)
    {
        if (jsonName == null)
            return (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.TemplateJsonUnity;

        _jsonCreator.CreateFolderAndJsonString(jsonName, directory);

        return _jsonCreator.CurrentJson;
        // return jsonName == null ? templateJson : _jsonCreator.CreateFolderAndJsonString(jsonName, directory);
    }

    private async Task<string> CheckExistJsonAsync(string jsonName = null, string directory = null)
    {
        if (jsonName == null)
        {
            currentJson = (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.TemplateJsonUnity;
        }

        Task<string> test = _jsonCreator.CreateFolderAndJsonString(jsonName, directory);

        while (!test.IsCompleted)
        {
            await Task.Yield();
        }

        return test.Result;
    }

    // private static GameObject CreateNewObject(Prefab prefabType, JsonUnity jsonUnity, JsonParentChild jsonParentChild,
    //     JsonData jsonData,
    //     Parent parent)
    // {
    //     return Factory.CreateObj(prefabType);
    //     return null;
    // }

    /// <summary>
    /// Считывает параметры из объекта в json
    /// </summary>
    private void OnCreateJson(string nameJson, GameObject obj, JsonUnity json, Parent parent)
    {
        json = FileToJsonObj(obj, json);

        SaveJson("JsonUnity" + nameJson, Config.PATH_TO_MAIN_JSON, json);
    }

    private async void SetChild(Parent parent, string nameJson)
    {
        string path;

        if (parent.My.GetComponent<PrefabName>().Prefab == Prefab.Button)
        {
            return;
        }

        if (parent.My.GetComponent<PrefabName>().Prefab == Prefab.MYTDesktop)
            path = Config.PATH_TO_MAIN_JSON;
        else
            path = Config.PATH_TO_DIRECTORY_JSON;

        var query = CheckExistJsonAsync("JsonParentChild" + parent.My.name, Config.PATH_TO_MAIN_JSON);

        while (!query.IsCompleted)
        {
            await Task.Yield();
        }

        JsonParentChild parentJson = JsonUtility.FromJson<JsonParentChild>(query.Result);


        if (parentJson == null)
            return;

        parentJson.MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == "MYTParentChild").MYTValue.Find(x => x.MYTName == "MYTChild").MYTValue.Add(
                new IdNameValue(nameJson));

        SaveJson("JsonParentChild" + parent.My.name, Config.PATH_TO_MAIN_JSON, parentJson);

        // return json;
    }

    private JsonUnity FileToJsonObj(GameObject obj, JsonUnity json)
    {
        //отключение канваса у обновляемого объекта

        CanvasGroup objWithCanvas = obj.GetComponent<CanvasGroup>();

        if (objWithCanvas != null)
        {
            objWithCanvas.interactable = false;
        }

        json = _get.CreateUnityJson(obj, json); //0 - unity json

        objWithCanvas.interactable = true;
        return json;
    }

    /// <summary>
    /// Выставляет параметры из json в объект
    /// </summary>
    private void OnSetJsonParameters(GameObject obj, JsonUnity json)
    {
        CanvasGroup objWithCanvas = obj.GetComponent<CanvasGroup>();
        objWithCanvas.interactable = false;

        _set.UpdateAllObjFromJson(obj, json);

        objWithCanvas.interactable = true;
    }

    public Prefab GetPrefabType(string jsonId, string directory = null)
    {
        _jsonCreator.CreateFolderAndJsonString(jsonId, directory);
        _jsonUnity = JsonUtility.FromJson<JsonUnity>(_jsonCreator.CurrentJson);


        return (Prefab)Enum.Parse(typeof(Prefab),
            _jsonUnity.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
                .Find(x => x.MYTName == "MYTPrefab").MYTValue[0].MYTName);
    }

    public void SaveChild(string nameChild, string nameParent, bool isMain)
    {
        string pathToJson;
        pathToJson = isMain
            ? Config.PATH_TO_MAIN_JSON
            : Config.PATH_TO_DIRECTORY_JSON;

        Debug.LogError(nameParent);
        Debug.LogError(nameChild);

        JsonClass jsonParent = GetDesktopJson("JsonParentChild" + nameParent, pathToJson);
        JsonClass jsonChild = GetDesktopJson("JsonParentChild" + nameChild, Config.PATH_TO_DIRECTORY_JSON);

        jsonParent.MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue
            .Find(x => x.MYTName == SwitcherValues.MYTChild.ToString()).MYTValue
            .Add(new IdNameValue(nameChild));

        jsonChild.MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue
            .Find(x => x.MYTName == SwitcherValues.MYTParent.ToString()).MYTValue[0]
            .MYTName = nameParent;

        SaveJson("JsonParentChild" + nameChild, Config.PATH_TO_DIRECTORY_JSON, jsonChild);
        SaveJson("JsonParentChild" + nameParent, pathToJson, jsonParent);
        // JsonUtility.FromJson<Desktop>(CheckExistJson(jsonName, Config.PATH_TO_MAIN_JSON));
    }

    public void RemoveChild(string nameChild, string nameParent, bool isMain)
    {
        string pathToJson;
        pathToJson = isMain
            ? Config.PATH_TO_MAIN_JSON
            : Config.PATH_TO_DIRECTORY_JSON;

        JsonClass jsonParent = GetDesktopJson(nameParent, pathToJson);
        jsonParent.MYTJson
            .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
            .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue
            .Find(x => x.MYTName == SwitcherValues.MYTChild.ToString()).MYTValue
            .RemoveAll(x => x.MYTName == nameChild);

        SaveJson(nameParent, pathToJson, jsonParent);
    }

    public override void SaveChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        Thread testThread = null;

        void ThreadAction()
        {
            //start oldRemove
            string pathToJson;
            pathToJson = isMain
                ? Config.PATH_TO_MAIN_JSON
                : Config.PATH_TO_DIRECTORY_JSON;


            //start GetDesktop
            string json = null;

            (RegisterSingleton._instance.GetSingleton(typeof(CustomThread)) as CustomThread)?.Execute(() =>
            {
                json = CheckExistJson(nameParent, pathToJson);

                testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
            });
            JsonUnity jsonParent = JsonUtility.FromJson<JsonUnity>(json);

            json = null;

            (RegisterSingleton._instance.GetSingleton(typeof(CustomThread)) as CustomThread)?.Execute(() =>
            {
                json = CheckExistJson(nameChild, Config.PATH_TO_DIRECTORY_JSON);

                testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
            });
            JsonUnity jsonChild = JsonUtility.FromJson<JsonUnity>(json);

            //end GetDesktop

            jsonParent.MYTJson
                .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
                .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue
                .Find(x => x.MYTName == SwitcherValues.MYTChild.ToString()).MYTValue
                .Add(new IdNameValue(nameChild));
            jsonChild.MYTJson
                .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
                .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue
                .Find(x => x.MYTName == SwitcherValues.MYTParent.ToString()).MYTValue[0]
                .MYTName = nameParent;


            //start Save

            string pathChild = null;


            (RegisterSingleton._instance.GetSingleton(typeof(CustomThread)) as CustomThread)?.Execute(() =>
            {
                pathChild = _jsonCreator.SetJsonName(nameChild, Config.PATH_TO_DIRECTORY_JSON);

                testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
            });


            _jsonCreator.SaveFile(pathChild, JsonUtility.ToJson(jsonChild));
            Debug.LogError("endsave");

            string pathParent = null;
            (RegisterSingleton._instance.GetSingleton(typeof(CustomThread)) as CustomThread)?.Execute(() =>
            {
                pathParent = _jsonCreator.SetJsonName(nameParent, pathToJson);

                testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
            });


            _jsonCreator.SaveFile(pathParent, JsonUtility.ToJson(jsonParent));
        }

        testThread = new Thread(new ThreadStart((Action)ThreadAction));
        testThread.Start();
    }

    public override void RemoveChildWithThread(string nameChild, string nameParent, bool isMain)
    {
        Thread testThread = null;

        void ThreadAction()
        {
            //start oldRemove
            string pathToJson;
            pathToJson = isMain
                ? Config.PATH_TO_MAIN_JSON
                : Config.PATH_TO_DIRECTORY_JSON;


            //start GetDesktop
            string json = null;

            (RegisterSingleton._instance.GetSingleton(typeof(CustomThread)) as CustomThread)?.Execute(() =>
            {
                json = CheckExistJson(nameParent, pathToJson);

                testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
            });
            JsonUnity jsonParent = JsonUtility.FromJson<JsonUnity>(json);

            //end GetDesktop

            jsonParent.MYTJson
                .Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
                .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue
                .Find(x => x.MYTName == SwitcherValues.MYTChild.ToString()).MYTValue
                .RemoveAll(x => x.MYTName == nameChild);


            //start Save
            string path = null;
            (RegisterSingleton._instance.GetSingleton(typeof(CustomThread)) as CustomThread)?.Execute(() =>
            {
                path = _jsonCreator.SetJsonName(nameParent, pathToJson);

                testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
            });


            _jsonCreator.SaveFile(path, JsonUtility.ToJson(jsonParent));
        }

        testThread = new Thread(new ThreadStart((Action)ThreadAction));
        testThread.Start();
    }

    private async Task SaveJson(string nameJson, string pathToJson, JsonClass json)
    {

        Thread testThread = null;

        // async void ThreadAction()
        // {
        string path = null;
        // RegisterSingleton._instance.CustomThread.Execute(() =>
        // {
        path = _jsonCreator.SetJsonName(nameJson, pathToJson);

        //     testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
        // });


        Task task = _jsonCreator.SaveFile(path, JsonUtility.ToJson(json));
        // await Task.WhenAll(task);
        while (!task.IsCompleted)
        {
            await Task.Yield();
        }

        // }

        // testThread = new Thread(new ThreadStart((Action) ThreadAction));
        // testThread.Start();

        // File.WriteAllText(_jsonCreator.SetJsonName(nameJson, pathToJson), JsonUtility.ToJson(json));
    }

    private void SaveJsonCoroutine(string nameJson, JsonClass json)
    {
        UnityEvent unityEvent = new UnityEvent();
        // nameJson = "JsonUnity" + nameJson;

        WWWForm Data = new WWWForm();
        Data.AddField("nameFile", nameJson);
        Data.AddField("json", JsonUtility.ToJson(json));


        LoadPaket loadPaket = new LoadPaket(Config.URL_SAVE_FILE, unityEvent, Data);
        (RegisterSingleton._instance.GetSingleton(typeof(CoroutineServerLoader)) as CoroutineServerLoader)?.StartDownload(loadPaket);
    }

    #region UpdateChapter

    public override void UpdateFromNewChapter(IdNameValue newJson, bool needUpdateFolder, GameObject jsonObject)
    {
//        Debug.LogError("Modify"+Thread.CurrentThread.ManagedThreadId);

        _jsonCreator.CreateFolderAndJsonString(jsonObject.name);
        var jsonString = _jsonCreator.CurrentJson;
        var pathJson = _jsonCreator.Path;


        Debug.Log(newJson.MYTName);


        Thread testThread = null;

//        Debug.LogError( Thread.CurrentThread.ManagedThreadId);

        Action threadAction = () =>
        {
//            Debug.LogError( Thread.CurrentThread.ManagedThreadId);

            jsonString = JsonUtility.ToJson(new CreateJsonFromObj().Update(_jsonUnity, newJson));

            (RegisterSingleton._instance.GetSingleton(typeof(CustomThread)) as CustomThread)?.Execute(() =>
            {
                _jsonCreator.SaveFile(pathJson, jsonString);
                Debug.Log("записал в json " + newJson.MYTName);
//                Debug.LogError( Thread.CurrentThread.ManagedThreadId);
                if (needUpdateFolder)
                {
                    UpdateObjFromNewChapter(newJson, jsonObject);
                }

                testThread.Interrupt(); //данное действие очень важно в конце всей последовательности
            });
            //действия, которые могут быть исполнены в потоке просто так, в cube кстати будет лежать наш кубик
        };

        testThread = new Thread(new ThreadStart(threadAction));
        testThread.Start();
    }

    public void UpdateObjFromNewChapter(IdNameValue newJson, GameObject jsonObject)
    {
        if (_createObjFromJson == null)
        {
            _createObjFromJson = new CreateObjFromJson();
        }

        _createObjFromJson.UpdateChapterObj(jsonObject, newJson);
    }

    #endregion
}