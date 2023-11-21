using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using _Game._Scripts.Panel;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class CreateObjFromJson
{
    private JsonCreator _jsonCreator;

    private bool _isGrid;

    private List<String> blockProperty = new List<string>()
    {
        "UnityEngine.RectTransform"
    };

//todo Брать первый и последний элемент у компонента и по ним строить словарь

    #region GET_SET_DICTIONARY

    /// <summary>
    /// </summary>
    /// <param name="idNameValue">структуру json компонента</param>
    /// <param name="obj">Объект у которого надо изменить property</param>
    public void UpdateComponent(IdNameValue idNameValue, GameObject obj, bool needAllParameters = true)
    {
        // Debug.LogError("update + " + idNameValue.MYTName);

        foreach (var VARIABLE in idNameValue.MYTValue)
        {
            if (needAllParameters || blockProperty.Find(x => x == idNameValue.MYTName) == null)
            {
                CurrentName(idNameValue.MYTName, VARIABLE, obj);
            }
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="nameComp">имя компонента у которого меняем проперти</param>
    /// <param name="idNameValue">структура json property</param>
    /// <param name="obj">Объект у которого надо изменить property</param>
    public void CurrentName(String nameComp, IdNameValue idNameValue, GameObject obj)
    {
        List<String> list = new List<string>();
        foreach (var VARIABLE in idNameValue.MYTValue)
        {
            list.Add(CurrentValue(VARIABLE));
        }

        SetParamToObject(nameComp,
            idNameValue.MYTName,
            list,
            obj);
    }

    /// <summary>
    /// Пока не дойдет до последнего элемента будет вызывать сама себя - углубляясь
    /// </summary>
    /// <param name="idNameValue"></param>
    /// <returns>Возвращает массив значений.</returns>
    public String CurrentValue(IdNameValue idNameValue)
    {
        if (idNameValue.MYTValue.Count != 0)
        {
            return CurrentValue(idNameValue.MYTValue[0]);
        }

        return idNameValue.MYTName;
    }


    /// <summary>
    /// Выставляет объекту параметр 
    /// </summary>
    /// <param name="nameComponent">Имя изменяемого компонента</param>
    /// <param name="nameProp">Имя изменяемого параметра</param>
    /// <param name="value">значение изменяемого параметра</param>
    /// <param name="obj">изменяемый объект</param>
    public void SetParamToObject(String nameComponent, String nameProp, List<String> value, GameObject obj)
    {
        if (nameComponent == "Shapes2D.Shape+UserProps")
        {
            Shape.UserProps test = obj.GetComponent<Shape>().settings;
            PropertyInfo prop = test.GetType().GetProperty(nameProp);
            SwitchTypeconverter(ConvertFromListValue(value), prop, test);
        }
        else
        {
            Object component = obj.GetComponents<Component>().ToList()
                .Find(x => x.GetType().ToString() == nameComponent);
//            Debug.LogError(nameComponent);
            try
            {
                PropertyInfo prop = component.GetType().GetProperty(nameProp);

                SwitchTypeconverter(ConvertFromListValue(value), prop, component);
            }
            catch (Exception e)
            {
            }
        }
    }

    /// <summary>
    /// Преобразует массив строк в 1 строку
    /// </summary>
    /// <param name="value">массив строк значений</param>
    /// <returns></returns>
    public string ConvertFromListValue(List<String> value)
    {
        string newValue = "";
        value.ForEach(x => { newValue += x + ";"; });
        newValue = newValue.Remove(newValue.Length - 1);

        return newValue;
    }

    #endregion

    #region SetProperty

    public void UpdateAllObjFromJson(GameObject _gameObject, JsonUnity json)
    {
        foreach (var chapter in json.MYTJson)
        {
            UpdateChapterObj(_gameObject, chapter);
        }
    }

    //todo нужно поменять обработку структуры json, доделать 7 json, настроить их сохранение и обновление 
    //todo отображение(в каком виде отображаются папки будь то список, плитка или как в балансе. В Workflow есть структура) для баланса и отображение для профиля
    //todo запрос из json в базу(нужен тэг для этого), чтобы получать инфу о токенах
    
    
    public void UpdateChapterObj(GameObject _gameObject, IdNameValue json)
    {
        StructJson chapter = (StructJson)Enum.Parse(typeof(StructJson), json.MYTName);

        switch (chapter)
        {
            case StructJson.Data:
            {
                foreach (var chapterJson in json.MYTValue)
                {
                    SwitchMetodSix(_gameObject, chapterJson);
                }

                break;
            }

            // case StructJson.Where:
            // {
            //     foreach (IdNameValue chapterJson in json.MYTValue)
            //     {
            //         SwitchMetodThree(_gameObject, chapterJson);
            //     }
            //
            //     break;
            // }

            case StructJson.What:
            {
                foreach (IdNameValue chapterJson in json.MYTValue)
                {
                    SwitchMetodWhat(_gameObject, chapterJson);
                }

                break;
            }
        }
    }

    void SwitchMetodSix(GameObject _gameObject, IdNameValue json)
    {
        SwitcherValues switcherValues = (SwitcherValues)Enum.Parse(typeof(SwitcherValues), json.MYTName);

        //todo добавить сюда переделанные части json 
        switch (switcherValues)
        {
            case SwitcherValues.MYTDefaultName:
            {
                WorkWithName(_gameObject, json);
                break;
            }

            case SwitcherValues.MYTParentChild:
            {
                WorkWithChild(_gameObject, json.MYTValue
                    .Find(x => x.MYTName == SwitcherValues.MYTChild.ToString()));
                break;
            }

            case SwitcherValues.MYTPrefab:
            {
                WorkWithPrefab(_gameObject, json);
                break;
            }

            case SwitcherValues.MYTLinks:
            {
//                Debug.LogError("MYTLinks  " + json.MYTName);
                break;
            }

            default:
                Debug.Log("UsersType in data " + json.MYTName);
                break;
        }
    }

    void SwitchMetodThree(GameObject _gameObject, IdNameValue json)
    {
        SwitcherValues switcherValues = (SwitcherValues)Enum.Parse(typeof(SwitcherValues), json.MYTName);

        switch (switcherValues)
        {
            case SwitcherValues.MYTVideo:
            {
//                Debug.LogError("WorkWithVideo  " + json.MYTName);
                WorkWithVideo(
                    _gameObject,
                    json);
                break;
            }

            case SwitcherValues.MYTImage:
            {
                //Debug.LogError("WorkWithImage  " + json.MYTName);
                WorkWithImage(
                    _gameObject,
                    json);
                break;
            }

            default:
            {
                Debug.LogError("UsersType  in where " + json.MYTName);
                break;
            }
        }
    }

    void SwitchMetodWhat(GameObject _gameObject, IdNameValue json)
    {
        SwitcherValues switcherValues = (SwitcherValues)Enum.Parse(typeof(SwitcherValues), json.MYTName);

        switch (switcherValues)
        {
            case SwitcherValues.MYTVideo:
            {
//                Debug.LogError("WorkWithVideo  " + json.MYTName);
                WorkWithVideo(
                    _gameObject,
                    json);
                break;
            }

            case SwitcherValues.MYTImage:
            {
                //Debug.LogError("WorkWithImage  " + json.MYTName);
                WorkWithImage(
                    _gameObject,
                    json);
                break;
            }

            default:
            {
                Debug.LogError("UsersType in what" + json.MYTName);
                break;
            }
        }
    }

    // public void SetPropertyToObj(List<GameObject> _gameObject, Desktop json,
    //     SwitcherValues switcherValues = SwitcherValues.MYTPrefab)
    // {
    //     switch (switcherValues)
    //     {
    //         case SwitcherValues.MYTParentChild:
    //         {
    //             WorkWithChild(_gameObject[0], json.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
    //                 .Find(x => x.MYTName == SwitcherValues.MYTParentChild.ToString()).MYTValue
    //                 .Find(x => x.MYTName == SwitcherValues.MYTChild.ToString()));
    //             break;
    //         }
    //
    //         case SwitcherValues.MYTDefaultName:
    //         {
    //             WorkWithName(_gameObject[0], json.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
    //                 .Find(x => x.MYTName == SwitcherValues.MYTDefaultName.ToString()));
    //             break;
    //         }
    //
    //
    //         case SwitcherValues.MYTPrefab:
    //         {
    //             WorkWithPrefab(_gameObject,
    //                 json.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
    //                     .Find(x => x.MYTName == SwitcherValues.MYTPrefab.ToString()));
    //             break;
    //         }
    //     }
    // }

    public void WorkWithPrefab(GameObject _gameObject, IdNameValue json)
    {
        Prefab prefabType = _gameObject.GetComponent<PrefabName>().Prefab;
        if (prefabType == Prefab.MYTIcon60_60)
        {
            WorkWithFolderIcon(
                _gameObject,
                json.MYTValue.Find(x => x.MYTName == prefabType.ToString()));
        }

        else
        {
            WorkWithFolder(_gameObject, json.MYTValue.Find(x => x.MYTName == prefabType.ToString()));
            // WorkWithImportantAreas(_gameObject,
            //     json.MYTValue.Find(x => x.MYTName == SwitcherValues.ImportantAreas.ToString()));
        }


        // foreach (var VARIABLE in json.MYTValue)
        // {
        //     if (VARIABLE.MYTName == Prefab.MYTIcon60_60.ToString())
        //     {
        //         WorkWithFolderIcon(
        //             _gameObject,
        //             VARIABLE);
        //     }
        //     else if (VARIABLE.MYTName == SwitcherValues.ImportantAreas.ToString())
        //     {
        //         // Debug.LogError("присваиваю импортант ");
        //         WorkWithImportantAreas(_gameObject,
        //             VARIABLE.MYTValue.Find(x => x.MYTName == SwitcherValues.ImportantAreas.ToString()));
        //     }
        //     else
        //     {
        //         // Debug.LogError("try");
        //         try
        //         {
        //             WorkWithFolder(
        //                 _gameObject,
        //                 VARIABLE.MYTValue[0]);
        //             WorkWithImportantAreas(_gameObject,
        //                 VARIABLE.MYTValue.Find(x => x.MYTName == SwitcherValues.ImportantAreas.ToString()));
        //         }
        //         catch (Exception e)
        //         {
        //             Debug.Log(e);
        //         }
        //     }
        //
        //     ////todo Перенести в 3 раздел ссылок
        //     //if (VARIABLE.MYTName == Prefab.MYTVideo.ToString())
        //     //{
        //     //    WorkWithVideo(
        //     //        _gameObject.Find(x => x.GetComponent<PrefabName>().Prefab == Prefab.MYTVideo).gameObject,
        //     //        VARIABLE);
        //     //}
        //
        //     //if (VARIABLE.MYTName == Prefab.MYTImage.ToString())
        //     //{
        //     //    WorkWithImage(
        //     //        _gameObject.Find(x => x.GetComponent<PrefabName>().Prefab == Prefab.MYTImage).gameObject,
        //     //        VARIABLE);
        //     //}
        // }
    }

    public void WorkWithImportantAreas(GameObject folder, IdNameValue jsonImportantAreas)
    {
        foreach (var VARIABLE in folder.GetComponent<FolderModel>().MyImportantAreas)
        {
            if (VARIABLE.name == SwitcherValues.Border.ToString())
            {
                WorkWithBorder(VARIABLE, jsonImportantAreas.MYTValue.Find(x => x.MYTName == VARIABLE.name));
            }
            else

            {
                WorkWithFolder(VARIABLE, jsonImportantAreas.MYTValue.Find(x => x.MYTName == VARIABLE.name), false);
            }
        }
    }

    public void WorkWithBorder(GameObject obj, IdNameValue json)
    {
        WorkWithFolder(obj, json, false);
        GameObject parentBorder = obj.GetComponent<Border>().MainFolder;
        parentBorder.GetComponent<MytPrefabColorTypes>().SetColor();
    }

    public void WorkWithFolderIcon(GameObject obj, IdNameValue json)
    {
        ;
        foreach (var component in json.MYTValue)
        {
            UpdateComponent(component, obj);
        }

        obj.GetComponent<SetAccesIcon>().IsAcces = obj.GetComponent<SetAccesIcon>().IsAcces;

        obj.GetComponent<IconModel>().SetIconToCell();
    }

    public void WorkWithFolder(GameObject obj, IdNameValue json, bool needAllParameters = true)
    {
        foreach (var component in json.MYTValue)
        {
            if (component.MYTName == SwitcherValues.MYTPrefabChildTypes.ToString())
            {
                obj.GetComponent<ShowableArea>().CanDropInMe.Clear();
                foreach (var VARIABLE in component.MYTValue)
                {
                    obj.GetComponent<ShowableArea>().CanDropInMe
                        .Add((Prefab)Enum.Parse(typeof(Prefab), VARIABLE.MYTName));
                }
            }


            UpdateComponent(component, obj, needAllParameters);
        }

        //obj.transform.parent.GetComponent<GridCell>().MyInventory.UpdateListGrid();

        var gridController = obj.GetComponent<GridControllerInFolderDesktop>();

        if (gridController != null)
        {
            //Debug.Log("gridController.IsGrid " + gridController.IsGrid + " " + obj);

            gridController.UpdateListGrid();
        }
    }

    public void WorkWithVideo(GameObject obj, IdNameValue json)
    {
        var path = json.MYTValue[0].MYTName;

        obj.GetComponent<GetVideoPlayer>().VideoManager.url = path;
    }

    public void WorkWithImage(GameObject obj, IdNameValue json)
    {
        string path = json.MYTValue[0].MYTName;
        // ManagerJson managerJson = RegisterSingleton._instance.ManagerJson;
        Sprite sprite = LoadSprite(path);
        // Transform imageGO = obj.transform.GetChild(0);

        // Image image = imageGO.GetComponent<Image>();
        // image.sprite = sprite;
        var imageIcon = obj.GetComponent<Image>();
        imageIcon.sprite = sprite;
        imageIcon.color = Color.white;
        // RectTransform imageRect = imageGO.GetComponent<RectTransform>();
        // Vector2 size = imageRect.rect.size;
        // DragDropFiles dragdrop = obj.transform.parent.GetComponent<DragDropFiles>();
        // dragdrop.SetNativeSize(obj, size, imageRect, image, null);
        // GameObject icon = obj.GetComponent<FolderModel>().Icon.gameObject;
        // dragdrop.SetIcon(path, managerJson, obj, icon, sprite);
    }

    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            return sprite;
        }

        return null;
    }

    public void WorkWithPrefabChildTypes(GameObject _gameObject, JsonUnity json)
    {
        _gameObject.GetComponent<ShowableArea>().CanDropInMe.Clear();
        foreach (var VARIABLE in json.MYTJson.Find(x => x.MYTName == StructJson.Data.ToString()).MYTValue
                     .Find(x => x.MYTName == SwitcherValues.MYTPrefabChildTypes.ToString())
                     .MYTValue)
        {
            _gameObject.GetComponent<ShowableArea>().CanDropInMe
                .Add((Prefab)Enum.Parse(typeof(Prefab), VARIABLE.MYTName));
        }
    }

    public void WorkWithName(GameObject _gameObject, IdNameValue json)
    {
        try
        {
            // _gameObject.GetComponent<FolderModel>().DefaultName =
            //     json.MYTValue[0].MYTName;
        }
        catch (Exception e)
        {
            Debug.Log("Ошибка присвоения имени из json");
        }
    }

    public void WorkWithChild(GameObject _gameObject, IdNameValue json)
    {
        return;
        DelCreate delCreate = FindDelCreateChild(_gameObject, json);
        foreach (var VARIABLE in delCreate.Del)
        {
            _gameObject.GetComponent<ParentChild>().RemoveChild(VARIABLE, true);
        }

        foreach (var VARIABLE in delCreate.Create)
        {
            _gameObject.GetComponent<ParentChild>().AddChild(VARIABLE);
        }


//        var a = _gameObject.GetComponent<FolderModel>();
        try
        {
            var icon = _gameObject.GetComponent<FolderModel>().Icon.gameObject;
            icon.GetComponent<IconModel>().SetGridCellNumber(icon);
        }
        catch (Exception e)
        {
            Debug.Log(_gameObject.name + "\n" + e);
        }
    }

    public DelCreate FindDelCreateChild(GameObject _gameObject, IdNameValue json)
    {
        DelCreate delCreate = new DelCreate();


        delCreate.Del = _gameObject.GetComponent<ParentChild>().Child;
        try
        {
            foreach (var VARIABLE in json.MYTValue)
            {
                if (_gameObject.GetComponent<ParentChild>().Child.Find(x => x.name == VARIABLE.MYTId) == null)
                {
                    delCreate.Create.Add(VARIABLE.MYTName);
                }
                else
                {
                    delCreate.Del.Remove(delCreate.Del.Find(x => x.name == VARIABLE.MYTName));
                    (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.CreateChild(VARIABLE.MYTName,
                        _gameObject.GetComponent<Parent>());
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // throw;
        }


        return delCreate;
    }

    #endregion

    #region CONVERT_TYPE

    public void SwitchTypeconverter(String value, PropertyInfo propertyInfo, Object obj)
    {
        if (propertyInfo.PropertyType == typeof(Vector3))
        {
            propertyInfo.SetValue(obj, StringToVector3(value));
        }
        else if (propertyInfo.PropertyType == typeof(Color))
        {
            propertyInfo.SetValue(obj, StringToColor(value));
        }
        else if (propertyInfo.PropertyType == typeof(Vector2))
        {
            propertyInfo.SetValue(obj, StringToVector2(value));
        }
        else if (propertyInfo.PropertyType == typeof(Enum))
        {
            propertyInfo.SetValue(obj, Enum.Parse(propertyInfo.PropertyType, value));
        }
        else if (propertyInfo.PropertyType == typeof(Quaternion))
        {
            propertyInfo.SetValue(obj, StringToQuaternion(value));
        }

        else
        {
            try
            {
                propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType));
            }
            catch (Exception e)
            {
//                Debug.LogError("Не удалось присвоить параметр " + value + " в " + propertyInfo.PropertyType);
            }
        }
    }

    ///overide
//    public void SwitchTypeconverter(String value, PropertyInfo propertyInfo, Shape.UserProps obj)
//    {
//        if (propertyInfo.PropertyType == typeof(Vector3))
//        {
//            propertyInfo.SetValue(obj, StringToVector3(value));
//        }
//        else if (propertyInfo.PropertyType == typeof(Color))
//        {
//            propertyInfo.SetValue(obj, StringToColor(value));
//        }
//        else if (propertyInfo.PropertyType == typeof(Vector2))
//        {
//            propertyInfo.SetValue(obj, StringToVector2(value));
//        }
//        else if (propertyInfo.PropertyType == typeof(Enum))
//        {
//            propertyInfo.SetValue(obj, Enum.Parse(propertyInfo.PropertyType, value));
//        }
//        else if (propertyInfo.PropertyType == typeof(Quaternion))
//        {
//            propertyInfo.SetValue(obj, StringToQuaternion(value));
//        }
//        else
//        {
//            propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType));
//        }
//    }
    public static Vector3 StringToVector3(string sVector, char sep = ';')
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        if (sep == ';')
        {
            sVector = sVector.Replace(',', '.');
        }

        // split the items
        String[] sArray = sVector.Split(sep);

        // store as a Vector3
        Vector3 result = new Vector3(float.Parse(sArray[0], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[1], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[2], CultureInfo.InvariantCulture.NumberFormat));

        return result;
    }

    public static Quaternion StringToQuaternion(string sVector, char sep = ';')
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        if (sep == ';')
        {
            sVector = sVector.Replace(',', '.');
        }


        // split the items
        String[] sArray = sVector.Split(sep);

        // store as a Vector3
        Quaternion result = new Quaternion(float.Parse(sArray[0], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[1], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[2], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[3], CultureInfo.InvariantCulture.NumberFormat));

        return result;
    }

    public static Vector2 StringToVector2(string sVector, char sep = ';')
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        if (sep == ';')
        {
            sVector = sVector.Replace(',', '.');
        }


        // split the items
        String[] sArray = sVector.Split(sep);

        // store as a Vector3
        Vector2 result = new Vector2(float.Parse(sArray[0], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[1], CultureInfo.InvariantCulture.NumberFormat));

        return result;
    }

    public static Color StringToColor(string sColor, char sep = ';')
    {
        // Remove the parentheses
        if (sColor.StartsWith("RGBA"))
        {
            sColor = sColor.Remove(0, 4);
        }

        if (sColor.StartsWith("(") && sColor.EndsWith(")"))
        {
            sColor = sColor.Substring(1, sColor.Length - 2);
        }

        if (sep == ';')
        {
            sColor = sColor.Replace(',', '.');
        }
        // split the items

        String[] sArray = sColor.Split(sep);
        // store as a Vector3

        Color result = new Color(float.Parse(sArray[0], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[1], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[2], CultureInfo.InvariantCulture.NumberFormat),
            float.Parse(sArray[3], CultureInfo.InvariantCulture.NumberFormat));

        return result;
    }

    #endregion
}

[Serializable]
public class DelCreate
{
    private List<GameObject> del;
    private List<String> create;

    public List<GameObject> Del
    {
        get => del;
        set => del = value;
    }

    public List<String> Create
    {
        get => create;
        set => create = value;
    }

    public DelCreate()
    {
        del = new List<GameObject>();
        create = new List<String>();
    }
}