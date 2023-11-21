using System.IO;
using System.Threading.Tasks;
using _Game._Scripts;
using Sirenix.Utilities;
using UnityEngine;

public class JsonResourceCreator : IJsonCreator
{
    private string path;
    private string currentJson;
    public string CurrentJson => currentJson;

    public string Path => path;

    public void CreateDirectory()
    {
        Debug.LogError("try Create directory in Resource");
        // CreateDirectory(Config.PATH_TO_DIRECTORY_JSON);
    }

    public void CreateDirectory(string str)
    {
        Debug.LogError("try Create directory in Resource");
    }

    public string[] GetFile(string pathStr = null)
    {
        var files = Resources.LoadAll<TextAsset>(pathStr.IsNullOrWhitespace() ? Config.PATH_TO_MAIN_RESOURCE_JSON : pathStr);
        string[] test = new string[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            test[i] = files[i].text;
        }

        return test;
    }

    public async Task<string> CreateFolderAndJsonString(string name, string directory = null)
    {
        if (string.IsNullOrEmpty(directory))
        {
            directory = Config.PATH_TO_DIRECTORY_JSON;
        }

        TextAsset textAsset = Resources.Load<TextAsset>(directory + name);

        currentJson = textAsset.text;
        return currentJson;
    }

    public string SetJsonName(string name, string directory)
    {
       
        if (!name.Contains("Json"))
        {
            name = "Json" + name;
        }

        CreateDirectory(directory);


        return  directory + name + ".json";
    }

    public async Task SaveFile(string path, string text)
    {
        Debug.LogError("cannot save resource");
            await Task.Yield();
    }
}