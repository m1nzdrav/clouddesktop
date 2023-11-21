using UnityEngine;

public class JsonParentChild : JsonClass
{


    public JsonParentChild(Parent parent)
    {
        MYTJson = JsonUtility.FromJson<JsonParentChild>((RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.TemplateJsonParentChild)
            .MYTJson;
        SetParent(parent);
    }
}