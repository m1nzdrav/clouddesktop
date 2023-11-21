using System.Collections.Generic;
using _Game._Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class JsonIcon : JsonEntity<IdNameValue>
{
    public JsonIcon(string nameObject) : base("JsonIcon_" + nameObject)
    {
        data.Add(new IdNameValue(Config.JSON_ENTITY_SETTINGS));
        data.Add(new IdNameValue(Config.JSON_ENTITY_MAIN));
        data.Add(new IdNameValue(Config.JSON_ENTITY_COVER));
        data.Add(new IdNameValue(Config.JSON_ENTITY_NAME));
        data.Add(new IdNameValue(Config.JSON_ENTITY_SOUND));
        data.Add(new IdNameValue(Config.JSON_ENTITY_COMMENT));
    }

    private void AddValue(string key, string value)
    {
        data.Find(x => x.MYTName == key).MYTValue.Add(new IdNameValue(value));
    }


    public static async UniTask<JsonIcon> Create(string key, IpfsLoader ipfsLoader)
    {
        Debug.LogError("start create jsonIcon");
        JsonIcon jsonIcon = new JsonIcon(key);
        JsonMain jsonMain = await JsonMain.Create(key + "_Main", ipfsLoader);
        Cover cover = new Cover(key + "_Cover");
        Name name = new Name(key + "_Name");
        Sound sound = new Sound(key + "_Sound");
        Comment comment = new Comment(key + "Comment");

        jsonIcon.AddValue(Config.JSON_ENTITY_MAIN, PlayerPrefs.GetString(key + "_Main"));
        UniTask mainTask = JsonTask(key, Config.JSON_ENTITY_MAIN, jsonMain, jsonIcon, ipfsLoader);
        UniTask coverTask = JsonTask(key, Config.JSON_ENTITY_COVER, cover, jsonIcon, ipfsLoader);
        UniTask nameTask = JsonTask(key, Config.JSON_ENTITY_NAME, name, jsonIcon, ipfsLoader);
        UniTask soundTask = JsonTask(key, Config.JSON_ENTITY_SOUND, sound, jsonIcon, ipfsLoader);
        UniTask commentTask = JsonTask(key, Config.JSON_ENTITY_COMMENT, comment, jsonIcon, ipfsLoader);

        
        await UniTask.WhenAll(mainTask, coverTask, nameTask, soundTask, commentTask);

        Debug.LogError("UniTask whenAll");

        await ipfsLoader.PublishText(key, JsonUtility.ToJson(jsonIcon),
            (cid, keyName) => PlayerPrefs.SetString(keyName, cid)
        );
        Debug.LogError("publish successfully");
        return jsonIcon;
    }

    private static UniTask JsonTask<T>(string key, string keyValue, T settings, JsonIcon jsonIcon,
        IpfsLoader ipfsLoader)
    {
        return ipfsLoader.UploadText(key + "_" + keyValue, JsonUtility.ToJson(settings),
            (cid, keyCid) => { jsonIcon.AddValue(keyValue, cid); });
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="jsonIcon"></param>
    /// <returns>
    ///     0 mainTask,
    ///     1 coverTask,
    ///     2 nameTask,
    ///     3 soundTask,
    ///     4 commentTask
    ///</returns>
    public static async UniTask<List<string>> LoadFromIconJson(JsonIcon jsonIcon, IpfsLoader ipfsLoader)
    {
        Debug.LogError("start download ");
        UniTask<string> mainTask = ipfsLoader.DownloadIpns(jsonIcon.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_MAIN)
            .MYTValue[0].MYTName);
        UniTask<string> coverTask = ipfsLoader.DownloadIpfs(jsonIcon.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_COVER)
            .MYTValue[0].MYTName);
        UniTask<string> nameTask = ipfsLoader.DownloadIpfs(jsonIcon.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_NAME)
            .MYTValue[0].MYTName);
        UniTask<string> soundTask = ipfsLoader.DownloadIpfs(jsonIcon.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_SOUND)
            .MYTValue[0].MYTName);
        UniTask<string> commentTask = ipfsLoader.DownloadIpfs(jsonIcon.data
            .Find(x => x.MYTName == Config.JSON_ENTITY_COMMENT)
            .MYTValue[0].MYTName);


        List<string> jsons = new List<string>(8)
        {
            await mainTask,
            await coverTask,
            await nameTask,
            await soundTask,
            await commentTask
        };

        return jsons;
    }
}