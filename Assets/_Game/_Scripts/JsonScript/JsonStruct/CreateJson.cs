using System.Collections.Generic;
using System.Threading.Tasks;
using _Game._Scripts;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class CreateJson : MonoBehaviour
{
    private IpfsLoader ipfsLoader;

    private void Start()
    {
        ipfsLoader = new IpfsLoader();
        Debug.LogError("start");
        CheckExistIconJson();
    }

    [Button]
    private async void CheckExistIconJson()
    {
        JsonIcon jsonIcon = await GetJsonIcon();
        Debug.LogError("getedJsonIcon");
        CreateIconFolder(jsonIcon, await JsonIcon.LoadFromIconJson(jsonIcon, ipfsLoader));
    }

    // [Button]
    // private async void CheckExistMainJson()
    // {
    // JsonMain jsonMain = await GetJsonIcon();
    // CreateMainFolder(jsonMain, await JsonMain.LoadFromMainJson(jsonMain, ipfsLoader));
    // }

    private async Task<JsonIcon> GetJsonIcon()
    {
        if (PlayerPrefs.HasKey(Config.STARTER_REF + "_what"))
            return await LoadIconJson(PlayerPrefs.GetString(Config.STARTER_REF + "_what"));

        Debug.LogError("no prefs");
        return await JsonIcon.Create(Config.STARTER_REF + "_what", ipfsLoader);
    }

    private async UniTask<JsonIcon> LoadIconJson(string key)
    {
        return Config.DeserializeJson<JsonIcon>(await ipfsLoader.DownloadIpns(key));
    }

    private async UniTask<JsonMain> LoadMainJson(string key)
    {
        return Config.DeserializeJson<JsonMain>(await ipfsLoader.DownloadIpns(key));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jsonMain"></param>
    /// <param name="jsons">
    ///     0 settingsTask,
    ///     1 whatTask,
    ///     2 whoTask,
    ///     3 whereTask,
    ///     4 whenTask,
    ///     5 attributesTask,
    ///     6 eventsTask,
    ///     7 actionsTask
    /// </param>
    private void CreateMainFolder(JsonMain jsonMain, List<string> jsons)
    {
        Debug.LogError(jsons[0]);
    }

    private void CreateIconFolder(JsonIcon jsonIcon, List<string> jsons)
    {
        Debug.LogError(jsons[0]);
    }
}