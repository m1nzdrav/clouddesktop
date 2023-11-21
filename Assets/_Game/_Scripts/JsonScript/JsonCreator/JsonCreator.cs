using System;
using System.IO;
using System.Threading.Tasks;
using _Game._Scripts;
using UnityEngine;

[Serializable]
public class JsonCreator : IJsonCreator
{
    private string path;
    private string currentJson;
    public string CurrentJson => currentJson;

    public string Path => path;

    public void CreateDirectory()
    {
        CreateDirectory(Config.PATH_TO_DIRECTORY_JSON);
    }

    public void CreateDirectory(string str)
    {
    
        Directory.CreateDirectory(Application.dataPath + str);
    }

    public string[] GetFile(string pathStr = null)
    {
        if (pathStr == null)
        {
            pathStr = Config.PATH_TO_DIRECTORY_JSON;
        }

        return Directory.GetFiles(Application.dataPath + pathStr, "*.json", SearchOption.AllDirectories);
    }

    public async Task<string> CreateFolderAndJsonString(string name, string directory = null)
    {
        if (string.IsNullOrEmpty(directory))
        {
            directory = Config.PATH_TO_DIRECTORY_JSON;
        }


        path = SetJsonName(name, directory);


        if (!File.Exists(path))
            File.Create(path).Dispose();

        // TextAsset textAsset = Resources.Load<TextAsset>(directory + name);

        currentJson = File.ReadAllText(path);
        return File.ReadAllText(path);
    }

    public  string SetJsonName(string name, string directory)
    {
  
        if (!name.Contains("Json"))
        {
            name = "Json" + name;
        }

        CreateDirectory(directory);

        
        return Application.dataPath + directory + name + ".json";
    }

    public async Task SaveFile(string path, string text)
    {
        File.WriteAllText(path, text);
        await Task.Yield() ;
    }
}