
using UnityEngine;

public class JsonData : JsonClass
{
    public JsonData()
    {
        
    }
    public JsonData(bool isFirst)
    {
        MYTJson = JsonUtility.FromJson<JsonData>((RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.TemplateJsonData)
            .MYTJson;
        SetName();
    }
}