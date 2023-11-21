using _Game._Scripts.Panel;
using UnityEngine;

public class BaseConfiguration : IConfiguration
{
    #region Get

    public JsonUnity Get(GameObject obj, JsonUnity newValue)
    {
        IdNameValue content = new IdNameValue(SwitcherValues.MYTBase.ToString());

        content.MYTValue.Add(GetNameJson(obj));
        content.MYTValue.Add(GetMainPrefab(obj));
        content.MYTValue.Add(GetFolderOpenFlag(obj));
        content.MYTValue.Add(GetIconNumberPosition(obj));

        newValue.MYTJson.Find(x => x.MYTName == "MYTPrefab").MYTValue
            .Find(x => x.MYTName == obj.GetComponent<PrefabName>().Prefab.ToString()).MYTValue.Add(content);
        return newValue;
    }


    private IdNameValue GetNameJson(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("NameJson");
        idNameValue.MYTValue.Add(new IdNameValue(obj.name));
        return idNameValue;
    }

    private IdNameValue GetMainPrefab(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("MainPrefab");
        idNameValue.MYTValue.Add(new IdNameValue(obj.GetComponent<PrefabName>().Prefab.ToString()));
        return idNameValue;
    }

    private IdNameValue GetFolderOpenFlag(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("FolderOpenFlag");
        Debug.LogError("нет folder open flag");
        // idNameValue.MYTValue.Add(new IdNameValue(obj.position.ToString()));
        return idNameValue;
    }

    private IdNameValue GetIconNumberPosition(GameObject obj)
    {
        IdNameValue idNameValue = new IdNameValue("IconNumberPosition");
        Debug.LogError("нет icon number position");
        // idNameValue.MYTValue.Add(new IdNameValue(obj.position.ToString()));
        return idNameValue;
    }

    #endregion

    public void Set(GameObject obj, IdNameValue value)
    {
    }
}