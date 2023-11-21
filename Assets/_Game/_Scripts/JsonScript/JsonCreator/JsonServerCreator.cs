using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace _Game._Scripts.JsonScript.JsonCreator
{
    public class JsonServerCreator : IJsonCreator
    {
        private string path;

        public string Path => path;

        private string currentJson;
        public string CurrentJson => currentJson;

        public void CreateDirectory()
        {
            CreateDirectory(Config.PATH_TO_MAIN_JSON);
        }

        public void CreateDirectory(string str = null)
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
            Debug.LogError(name);
            WWWForm Data = new WWWForm();
            Data.AddField("nameFile", name);


            UnityWebRequest Query = UnityWebRequest.Post(Config.URL_READ_FILE, Data);
            // UnityWebRequest Query = UnityWebRequest.Get(Config.URL_TEMPLATE_JSON);

            UnityWebRequestAsyncOperation test = Query.SendWebRequest();

            while (!test.isDone)
            {
                await Task.Yield();
            }

            if (Query.isHttpError)
            {
                Debug.LogError("Server does not respond : " + Query.error);
            }

            Debug.LogError("Already");
            currentJson = Query.downloadHandler.text;

            Query.Dispose();

            // TextAsset textAsset = Resources.Load<TextAsset>(directory + name);

            return currentJson;
        }

        public string SetJsonName(string name, string directory)
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
            Debug.LogError("enter");
            Debug.LogError(path);
            WWWForm Data = new WWWForm();
            Data.AddField("nameFile", path);
            Data.AddField("json", text);

            UnityWebRequest Query = UnityWebRequest.Post(Config.URL_SAVE_FILE, Data);

            UnityWebRequestAsyncOperation test = Query.SendWebRequest();

            while (!test.isDone)
            {
                await Task.Yield();
            }


            if (Query.isHttpError)
            {
                Debug.LogError("Server does not respond : " + Query.error);
            }
            else
            {
                if (Query.downloadHandler.text == "good") // что нам должен ответить сервер на наши данные
                {
                    Debug.LogError("Server responded correctly");
                }
                else
                {
                    Debug.LogError("Server responded : " + Query.downloadHandler.text);
                }
            }

            // File.WriteAllText(path, text);
        }
    }
}