using System;
using System.Collections.Generic;
using _Game._Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ModelForm : MonoBehaviour
{
    [SerializeField] private GameObject prefabeComponentView;
    [SerializeField] private GameObject parentComponentView;
    [SerializeField] private ManagerJson _managerJson;
    [SerializeField] private JsonUnity jsonUnity;

    [SerializeField] private UserJson _userJson;

    public UserJson UserJson
    {
        get => _userJson;
        set => _userJson = value;
    }

    [SerializeField] private GameObject _folder;

    public GameObject Folder
    {
        get => _folder;
        set => _folder = value;
    }

    private void Awake()
    {
        _managerJson = FindObjectOfType<ManagerJson>();
//        UpdateForm();
    }

    private void OnDisable()
    {
//        Debug.LogError("delete "+ parentComponentView.transform.childCount);
        int count = parentComponentView.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Destroy(parentComponentView.transform.GetChild(i).gameObject);
        }

//        Debug.LogError("Final");
    }

    private void OnEnable()
    {
        UpdateForm();
    }

    public void UpdateForm()
    {
        OnDisable();

        JsonCreator jsonCreator = new JsonCreator();
        Debug.LogError(_folder.name);
//        Debug.LogError(jsonCreator.CreateFolderAndJsonString(_folder.name));
        // desktop = JsonUtility.FromJson<Desktop>(
        //     jsonCreator.CreateFolderAndJsonString(_folder.name,
        //         _folder.GetComponent<FolderModel>().PathToJson)); //todo 
        
        // _userJson.SetUserJson(desktop.MYTJson.Find(x => x.MYTName == StructJson.Six.ToString()).MYTValue
        //     .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue.Find(x=>x.MYTName=="MYTChildForm"),this);
        SetJson(jsonUnity);
    }

    public void SetJson(JsonUnity json, int count = 0)
    {
//        currentJson = json;
        List<IdNameValue> list = new List<IdNameValue>();
        List<String> _name = new List<string>();
        IdNameValue _json = new IdNameValue();


        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.Main.ToString()));
//        _json = new IdNameValue() {MYTName = "One",MYTValue = new List<IdNameValue>() {}};
        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.Who.ToString()));
//        _json = new IdNameValue() {MYTName = "Two",MYTValue = new List<IdNameValue>() {json.MYTJson.Find(x=>x.MYTName==StructJson.Two.ToString())}};
        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.What.ToString()));
//        _json = new IdNameValue() {MYTName = "Three",MYTValue = new List<IdNameValue>() {json.MYTJson.Find(x=>x.MYTName==StructJson.Three.ToString())}};
        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.Where.ToString()));
//        _json = new IdNameValue() {MYTName = "Four",MYTValue = new List<IdNameValue>() {json.MYTJson.Find(x=>x.MYTName==StructJson.Four.ToString())}};
        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.When.ToString()));
//        _json = new IdNameValue() {MYTName = "Five", MYTValue = new List<IdNameValue>() {json.MYTJson.Find(x=>x.MYTName==StructJson.Five.ToString())}};
        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.Events.ToString()));
//        _json = new IdNameValue() {MYTName = "Six", MYTValue = new List<IdNameValue>() {json.MYTJson.Find(x=>x.MYTName==StructJson.Six.ToString())}};
        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString()));

        list.Add(json.MYTJson.Find(x => x.MYTName == StructJson.Actions.ToString()));


        _name = new List<string>();
        _name.Add("Main");
        _name.Add("Who");
        _name.Add("What");
        _name.Add("Where");
        _name.Add("When");
        _name.Add("Events");
        _name.Add("Data");
        _name.Add("Actions");

        CreateComponentView(list, count, _name).ForEach(x => x.gameObject.SetActive(true));
    }

    /// <summary>
    /// Создание верхнего уровня json
    /// </summary>
    /// <param name="json"> json на основе которого создаем компоненты</param>
    /// <param name="_numRows"> количество </param>
    private List<ComponentView> CreateComponentView(List<IdNameValue> json, int _numRows, List<String> _name,
        string _value = null)
    {
        List<ComponentView> list = new List<ComponentView>();

        for (int i = 0; i < json.Count; i++)
        {
            ComponentView component = Instantiate(prefabeComponentView, parentComponentView.transform)
                .GetComponent<ComponentView>();
            if (_name[i] == "Zero")
            {
                component.SetNameValuesOnPrefabe(this, json[i], _numRows, _name[i], _value);
                CreateChildZero(component);
                list.Add(component);
            }
            else
            {
                component.SetNameValuesOnPrefabe(this, json[i], _numRows, _name[i], _value);
                CreateChild(component, _numRows + 1, true);
                list.Add(component);
            }
        }
//        foreach (var VARIABLE in json)
//        {
//            ComponentView component = Instantiate(prefabeComponentView, parentComponentView.transform)
//                .GetComponent<ComponentView>();
//            component.SetNameValuesOnPrefabe(this, VARIABLE, _numRows, name, _value);
//            CreateChild(component, _numRows + 1, true);
//            list.Add(component);
//        }

        return list;
    }

    /// <summary>
    /// Проверка нужно ли значение у компонента
    /// </summary>
    /// <param name="json">json который хотим проверить</param>
    /// <returns></returns>
    public bool CheckJson(IdNameValue json)
    {
        try
        {
            if (json.MYTValue[0].MYTValue.Count == 0)
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <summary>
    /// Создание компонента
    /// </summary>
    /// <param name="sender">компонент родителя</param>
    /// <param name="count"> каким по счету создаем</param>
    /// <param name="firstTry">True в случае первого запуска</param>
    public void CreateChild(ComponentView sender, int count, bool firstTry = false)
    {
//        Debug.LogError(sender.CurrentJson.MYTName);
//        if (firstTry || (count == Config.QUEUE_FORM && sender.CurrentJson.MYTValue[0].MYTValue.Count != 0))

        if (firstTry || (count == Config.QUEUE_FORM &&
                         sender.CurrentJson.MYTValue.Find(x => x.MYTValue.Count != 0) != null))
        {
            // foreach (var VARIABLE in sender.CurrentJson.MYTValue)
            // {
            for (int i = sender.CurrentJson.MYTValue.Count - 1; i >= 0; i--)
            {
                IdNameValue currentForm = sender.CurrentJson.MYTValue[i];
                if (!string.IsNullOrEmpty(currentForm.MYTName))
                {
                    GameObject obj = Instantiate(prefabeComponentView, sender.transform.parent);
                    ComponentView child = obj.GetComponent<ComponentView>();

                    child.transform.SetSiblingIndex(sender.transform.GetSiblingIndex() + 1);
                    sender.ChildComponent.Add(child);
                    sender.Btn.GetComponent<Image>()
                        .color = Color.blue; //todo переделать на вызов цвета у родителя

                    if (CheckJson(currentForm))
                        child.SetNameValuesOnPrefabe(this, currentForm, sender.NumRows + 1, currentForm.MYTName,
                            currentForm.MYTValue[0].MYTName);

                    else
                        child.SetNameValuesOnPrefabe(this, currentForm, sender.NumRows + 1, currentForm.MYTName);
                }
            }

            // }


            sender.GetComponent<LineInList>().ListToChlidLine(sender.ChildComponent);
        }
        else
            foreach (var VARIABLE in sender.ChildComponent)
            {
                CreateChild(VARIABLE, count + 1);
            }
    }

    /// <summary>
    /// Выставление параметров для Zero
    /// </summary>
    /// <param name="sender"></param>
    public void CreateChildZero(ComponentView sender)
    {
        sender.Btn.GetComponent<Image>()
            .color = Color.blue; //todo переделать на вызов цвета у родителя
        foreach (var VARIABLE in sender.CurrentJson.MYTValue)
        {
            GameObject obj = Instantiate(prefabeComponentView, sender.transform.parent);
            ComponentView child = obj.GetComponent<ComponentView>();
            child.transform.SetSiblingIndex(sender.transform.GetSiblingIndex() + 1);
            child.SetNameValuesOnPrefabe(this, VARIABLE, sender.NumRows + 1, "MytID",
                VARIABLE.MYTId);
            sender.ChildComponent.Add(child);


            obj = Instantiate(prefabeComponentView, sender.transform.parent);
            child = obj.GetComponent<ComponentView>();
            child.transform.SetSiblingIndex(sender.transform.GetSiblingIndex() + 1);
            child.SetNameValuesOnPrefabe(this, VARIABLE, sender.NumRows + 1, "MytName",
                VARIABLE.MYTId);
            sender.ChildComponent.Add(child);
        }


        sender.GetComponent<LineInList>().ListToChlidLine(sender.ChildComponent);
    }

    public void UpdateJson(IdNameValue newJson,bool needUpdateFolder =true)
    {
        Debug.LogError("update+ "+ newJson.MYTValue[0].MYTName);
        _managerJson.UpdateNewChapter(newJson, _folder,needUpdateFolder);
//        currentJson = new UpdateChapterJson().Update(currentJson, newJson);
    }

//public void CreateChild(ComponentView sender, int count, bool firstTry = false)
//{
//    if (count == OpenManager.queue || firstTry)
//    {
//        foreach (var VARIABLE in sender.CurrentJson.MYTValue)
//        {
//            //set value or name
//            GameObject obj = Instantiate(prefabeComponentView, sender.transform.parent);
//            ComponentView child = obj.GetComponent<ComponentView>();
//
//            child.transform.SetSiblingIndex(sender.transform.GetSiblingIndex() + 1);
//            sender.ChildComponent.Add(child);
//
//            if (CheckJson(VARIABLE))
//                child.SetNameValuesOnPrefabe(this, VARIABLE, sender.NumRows + 1, VARIABLE.MYTName,
//                    VARIABLE.MYTValue[0].MYTName);
//
//            else
//                child.SetNameValuesOnPrefabe(this, VARIABLE, sender.NumRows + 1,"MytName", VARIABLE.MYTName);
//            
//            // set id
//            
//            GameObject Id = Instantiate(prefabeComponentView, sender.transform.parent);
//            ComponentView childId = Id.GetComponent<ComponentView>();
//
//            childId.transform.SetSiblingIndex(sender.transform.GetSiblingIndex() + 2);
//            sender.ChildComponent.Add(childId);
//            childId.SetNameValuesOnPrefabe(this, VARIABLE, sender.NumRows + 1, "MytId",
//                VARIABLE.MYTId);
//            
//        }
//
//
//        sender.GetComponent<LineInList>().ListToChlidLine(sender.ChildComponent);
//    }
//    else
//        foreach (var VARIABLE in sender.ChildComponent)
//        {
//            CreateChild(VARIABLE, count + 1);
//        }
//}
}