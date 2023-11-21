using System.Collections.Generic;
using _Game._Scripts;
using Cysharp.Threading.Tasks;
// using Ipfs;
using UnityEngine;

public class JsonMain : JsonEntity<IdNameValue>
{
    public JsonMain(string nameEntity) : base(nameEntity)
    {
        data.Add(new IdNameValue(Config.JSON_ENTITY_SETTINGS));
        data.Add(new IdNameValue(Config.JSON_ENTITY_PARENT_CHILD));
        data.Add(new IdNameValue(Config.JSON_ENTITY_WHAT));
        data.Add(new IdNameValue(Config.JSON_ENTITY_WHO));
        data.Add(new IdNameValue(Config.JSON_ENTITY_WHERE));
        data.Add(new IdNameValue(Config.JSON_ENTITY_WHEN));
        data.Add(new IdNameValue(Config.JSON_ENTITY_ATTRIBUTE));
        data.Add(new IdNameValue(Config.JSON_ENTITY_EVENTS));
        data.Add(new IdNameValue(Config.JSON_ENTITY_ACTIONS));
    }

    private void AddValue(string key, string value)
    {
        data.Find(x => x.MYTName == key).MYTValue.Add(new IdNameValue(value));
    }

    #region static

    public static async UniTask<JsonMain> Create(string key, IpfsLoader ipfsLoader)
    {
        JsonMain jsonMain = new JsonMain(key);
        Settings settings = new Settings(key + "_Settings");
        What what = new What(key + "_What");
        Who who = new Who(key + "_Who");
        Where where = new Where(key + "_Where");
        When when = new When(key + "_When");
        Attributes attributes = new Attributes(key + "_Attributes");
        Events events = new Events(key + "_Events");
        Actions actions = new Actions(key + "_Actions");

        UniTask settingsTask = JsonTask(key, Config.JSON_ENTITY_SETTINGS, settings, jsonMain, ipfsLoader);
        UniTask whatTask = JsonTask(key, Config.JSON_ENTITY_WHAT, what, jsonMain, ipfsLoader);
        UniTask whoTask = JsonTask(key, Config.JSON_ENTITY_WHO, who, jsonMain, ipfsLoader);
        UniTask whereTask = JsonTask(key, Config.JSON_ENTITY_WHERE, where, jsonMain, ipfsLoader);
        UniTask whenTask = JsonTask(key, Config.JSON_ENTITY_WHEN, when, jsonMain, ipfsLoader);
        UniTask attributesTask = JsonTask(key, Config.JSON_ENTITY_ATTRIBUTE, attributes, jsonMain, ipfsLoader);
        UniTask eventsTask = JsonTask(key, Config.JSON_ENTITY_EVENTS, events, jsonMain, ipfsLoader);
        UniTask actionsTask = JsonTask(key, Config.JSON_ENTITY_ACTIONS, actions, jsonMain, ipfsLoader);

        await UniTask.WhenAll(settingsTask, whatTask, whoTask, whereTask, whenTask, attributesTask, eventsTask,
            actionsTask);

        await ipfsLoader.PublishText(key, JsonUtility.ToJson(jsonMain), (cid, keyName) => PlayerPrefs.SetString(keyName, cid)
        );

        return jsonMain;
    }

    private static UniTask JsonTask<T>(string key, string keyValue, T settings, JsonMain jsonMain,
        IpfsLoader ipfsLoader)
    {
        return ipfsLoader.UploadText(key + "_" + keyValue, JsonUtility.ToJson(settings),
            (cid, keyCid) => { jsonMain.AddValue(keyValue, cid); });
    }
    
        
/// <summary>
/// 
/// </summary>
/// <param name="jsonMain"></param>
/// <returns>
///     0 settingsTask,
///     1 whatTask,
///     2 whoTask,
///     3 whereTask,
///     4 whenTask,
///     5 attributesTask,
///     6 eventsTask,
///     7 actionsTask
///</returns>
    public static async UniTask<List<string>> LoadFromMainJson(JsonMain jsonMain,IpfsLoader ipfsLoader)
    {
        UniTask<string> settingsTask = ipfsLoader.DownloadIpfs(jsonMain.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_SETTINGS)
            .MYTValue[0].MYTName);
        UniTask<string> whatTask = ipfsLoader.DownloadIpfs(jsonMain.data.Find(x => x.MYTName == Config.JSON_ENTITY_WHAT)
            .MYTValue[0].MYTName);
        UniTask<string> whoTask = ipfsLoader.DownloadIpfs(jsonMain.data.Find(x => x.MYTName == Config.JSON_ENTITY_WHO)
            .MYTValue[0].MYTName);
        UniTask<string> whereTask = ipfsLoader.DownloadIpfs(jsonMain.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_WHERE)
            .MYTValue[0].MYTName);
        UniTask<string> whenTask = ipfsLoader.DownloadIpfs(jsonMain.data.Find(x => x.MYTName == Config.JSON_ENTITY_WHEN)
            .MYTValue[0].MYTName);
        UniTask<string> attributesTask = ipfsLoader.DownloadIpfs(jsonMain.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_ATTRIBUTE)
            .MYTValue[0].MYTName);
        UniTask<string> eventsTask = ipfsLoader.DownloadIpfs(jsonMain.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_EVENTS)
            .MYTValue[0].MYTName);
        UniTask<string> actionsTask = ipfsLoader.DownloadIpfs(jsonMain.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_ACTIONS)
            .MYTValue[0].MYTName);

        List<string> jsons = new List<string>(8)
        {
            await settingsTask,
            await whatTask,
            await whoTask,
            await whereTask,
            await whenTask,
            await attributesTask,
            await eventsTask,
            await actionsTask
        };
        
        return jsons;
    }

    #endregion
}