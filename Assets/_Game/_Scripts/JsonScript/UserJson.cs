using System;
using System.Collections.Generic;
using System.IO;
using _Game._Scripts;
using _Game._Scripts.Panel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UserJson : MonoBehaviour
{
    [SerializeField] private bool isCreateChild = false;
    [SerializeField] private ParentChild _parentChild;
    [SerializeField] private GameObject folder;
    [SerializeField] private List<GameObject> childs = new List<GameObject>();

    public GameObject Folder
    {
        get => folder;
        set => folder = value;
    }

    [SerializeField] private JsonUnity userJson;


    public JsonUnity Json
    {
        get => userJson;
        set
        {
            userJson = value;
            // CreateChild();
            // UpdateChildTrees();
        }
    }


    private void OnEnable()
    {
        UpdateChildTrees();
    }

    public void UpdateChildTrees()
    {
        CreateChild(Json.MYTJson, GetComponent<CreateModelPrefab>());
    }

    public void CreateChild(List<IdNameValue> value, CreateModelPrefab createModelPrefab)
    {
        foreach (var VARIABLE in value)
        {
            GameObject icon = null;
            if (VARIABLE.MYTId.Contains("Json"))
            {
                Debug.Log("Был создан ранее");
                Debug.Log(VARIABLE.MYTId);

                icon = createModelPrefab.Create(VARIABLE.MYTId, Prefab.MYTFolder, null, null, false);
                childs.Add(icon);
            }
            else
            {
                // Debug.Log(VARIABLE.MYTName);
                Config.StringBuilder.Length = 0;
                Config.StringBuilder.Append(VARIABLE.MYTName);
                Config.StringBuilder.Append(Folder.gameObject.name);

                icon =
                    createModelPrefab.Create(null, Prefab.MYTFolder, null, Config.StringBuilder.ToString(), false);
                childs.Add(icon);


                // icon.GetComponent<IconModel>().Folder.gameObject.name = Config.StringBuilder.ToString();


                Json.MYTJson.Find(x => x.MYTName == VARIABLE.MYTName).MYTId =
                    icon.GetComponent<IconModel>().Folder.gameObject.name;


                IdNameValue Six = new IdNameValue(StructJson.Data.ToString());
                IdNameValue parentChild = new IdNameValue(SwitcherValues.MYTParentChild.ToString());
                IdNameValue childForm = new IdNameValue("MYTUserForm");

                childForm.MYTValue.Add(Json.MYTJson.Find(x => x.MYTName == VARIABLE.MYTName));
                parentChild.MYTValue.Add(childForm);
                Six.MYTValue.Add(parentChild);

                UpdateJson(Six, false);
            }

            icon.GetComponent<IconModel>().Folder.GetComponent<MytPrefabColorTypes>().ParentBorder.color =
                GetComponent<MytPrefabColorTypes>().ParentBorder.color;
            icon.GetComponent<IconModel>().Folder.GetComponent<MytPrefabColorTypes>().SetColor();

            icon.GetComponent<Image>().color = GetComponent<MytPrefabColorTypes>().ParentBorder.color;

            SetUserJsonComponentToFolder(icon.GetComponent<IconModel>().Folder.gameObject, VARIABLE);
            // }
        }
    }

    private void OnDisable()
    {
        DeleteChild();
    }

    public void DeleteChild()
    {
        foreach (var VARIABLE in childs)
        {
            VARIABLE.GetComponent<IconModel>().Folder.GetComponent<ICreator>().Delete();
        }

        childs.Clear();
    }

    public void SetUserJsonComponentToFolder(GameObject folder, IdNameValue json)
    {
        UserJsonComponent userJsonComponent =
            folder.AddComponent<UserJsonComponent>();

        userJsonComponent.SetComponentJson(json);
    }

    [Button]
    public void UpdateJson(IdNameValue newChapter, bool needUpdateFolder)
    {
        // ManagerJson.instance.UpdateUserNewChapter(newChapter, folder, false);
    }

    public void AddNewComponentUser(String componentName)
    {
        userJson.AddChapter(componentName);
    }

    public void DeleteComponentUser(String componentName)
    {
        userJson.DeleteChapter(componentName);
    }

    public void UpdateComponentUser(String componentName, List<IdNameValue> newValue)
    {
        userJson.UpdateChapter(componentName, newValue);
    }
}