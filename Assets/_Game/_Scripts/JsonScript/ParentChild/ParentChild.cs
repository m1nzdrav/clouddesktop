using System;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using UnityEngine;

public class ParentChild : UpdateJson
{
    [SerializeField] private GameObject parent;
    [SerializeField] private List<GameObject> child;
    [SerializeField] private int folderNumberHierarchy;

    [SerializeField] private Border border;

    private GameEvent addChild, removeChild;

    public GameEvent AddChildEvent => addChild;

    public GameEvent RemoveChildEvent => removeChild;


    public int FolderNumberHierarchy
    {
        get => folderNumberHierarchy;
        set
        {
            border.NumberHierarchyText.text = value.ToString();
            folderNumberHierarchy = value;
        }
    }

    public GameObject Parent
    {
        get => parent;
        set => parent = value;
    }

    public List<GameObject> Child
    {
        get => child;
        set => child = value;
    }

    public void RegisterForAllEvent(EventListener listener)
    {
        RegisterAddChild(listener);
        RegisterRemoveChild(listener);
    }

    private void InstantiateEvent()
    {
        if (addChild != null) return;

        addChild = new GameEvent();
        removeChild = new GameEvent();
    }


    public void RegisterRemoveChild(EventListener listener)
    {
        InstantiateEvent();
        removeChild.Register(listener);
    }

    public void RegisterAddChild(EventListener listener)
    {
        InstantiateEvent();
        addChild.Register(listener);
    }

    public void AddChild(String childName, GameObject newChild = null)
    {
        InstantiateEvent();
        if (newChild == null)
        {
            // newChild = gameObject.GetComponent<CreateModelPrefab>()
            //     .Create(childName, RegisterSingleton._instance.ManagerJson.ReturnTypeOfPrefab(childName));
        }
        else
        {
            newChild.GetComponent<FolderModel>().Icon.transform.rotation = transform.rotation;
            Child.Add(newChild);
            SetJson(gameObject);
        }


        SetHierarchyChildNumber(newChild.GetComponent<ParentChild>());

        try
        {
            GetComponent<FolderModel>().TopPanelSetParametrs.FolderHierarchy.GetComponent<HierarchyModel>()
                .ReloadChild();
        }
        catch (Exception e)
        {
            Debug.Log("Нет Иерархии у объекта " + gameObject.name);
        }

        addChild.Raise();
    }

    public void RemoveChild(string newChild, bool needDelete, bool needUpdate = false)
    {
        RemoveChild(Child.Find(x => x.name == newChild), needDelete, needUpdate);
    }

    public void RemoveChild(GameObject newChild, bool needDelete, bool needUpdate = false)
    {
        if (needDelete)
        {
            newChild.GetComponent<ICreator>().Delete();
        }

        child.Remove(newChild);


        try
        {
            GetComponent<ShowableArea>().CantDropInMe.Remove(newChild.GetComponent<ShowableArea>());
            GetComponent<ShowableArea>().CantDropInMe
                .Remove(newChild.GetComponent<FolderModel>().Icon.GetComponent<ShowableArea>());
        }
        catch (Exception e)
        {
            Debug.LogError("Не удалось Убрать ребенка у " + GetComponent<ShowableArea>().NameArea);
        }

        if (needUpdate)
            SetJson(gameObject);
        try
        {
            GetComponent<FolderModel>().TopPanelSetParametrs.FolderHierarchy.GetComponent<HierarchyModel>()
                .ReloadChild();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        Debug.LogError("test Remove");
        // removeChild.Raise();
    }

    public void SetHierarchyChildNumber(ParentChild child)
    {
        if (child != null)
        {
            child.FolderNumberHierarchy = folderNumberHierarchy + 1;
        }
    }

    public void ChangeParent(ShowableArea newParent)
    {
        //удаление ребенка у старого родителя
        try
        {
            parent.GetComponent<ParentChild>().RemoveChild(gameObject, false, true);
        }
        catch (Exception e)
        {
            Debug.LogError("Не удалось удалить ребенка");
        }


        //смена текщего родителя
        parent = newParent.gameObject;
        GetComponent<FolderModel>().Icon.GetComponent<RectTransform>().sizeDelta =
            new Vector2(newParent.SizeChild, newParent.SizeChild);

        //добавление ребенка текущему родителю
        if (newParent.GetComponent<PrefabName>() != null)
        {
            switch (newParent.GetComponent<PrefabName>().Prefab)
            {
                case Prefab.MYTFolder:
                {
                    newParent.GetComponent<ParentChild>().AddChild(null, gameObject);
                    break;
                }

                case Prefab.MYTIcon60_60:
                {
                    newParent.GetComponent<IconModel>().Folder.GetComponent<ParentChild>().AddChild(null, gameObject);
                    break;
                }

                default:
                    newParent.GetComponent<ParentChild>().AddChild(null, gameObject);
                    break;
            }
        }

        //обновление json у текущего ребенка
        SetJson(gameObject);
    }
}