using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using _Game._Scripts;
using _Game._Scripts.Panel;
using Shapes2D;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public enum SwitcherValues
{
    MYTPrefab,
    MYTParentChild,
    MYTParent,
    MYTChild,
    MYTDefaultName,
    MYTPrefabChildTypes,
    Folder,
    Border,
    MYTIcon,
    MYTCover,
    MYTUnityVideoAddress,
    MYTUnityImageAddress,
    ImportantAreas,
    MYTLinks,
    MYTImage,
    MYTVideo,
    MYTColor,
    JsonData,
    JsonUnity,
    JsonParentChild,
    MYTTransform,
    MYTContent,
    MYTText,
    MYTBase
}


public class CreateJsonFromObj
{
    public List<string> UnNeededInJson = new List<string>()
    {
        "useGUILayout", "runInEditMode", "enabled",
        "tag", "name", "hideFlags", "hasChanged",
        "hierarchyCapacity", "parent", "material", "onCullStateChanged",
        "pathSegments", "pathThickness", "polyVertices", "ObjWhoNeedCreateJson",
        "Parent", "Child", "delegates", "triggers", "ParentBorder", "CantDropInMe", "CanDropInMe",
        "ObjectOnFirstView", "additionalShaderChannels", "worldCamera", "PathToJson", "DefaultName", "ManagerJson",
        "NeedCreateJson", "ChildArea", "AreaToShow", "sprite", "overrideSprite", "targetGraphic",
        "animationTriggers", "spiteState", "colors", "navigation", "image", "spriteState", "onValueChanged",
        "onValueChange", "onEndEdit", "textComponent", "AccessColor", "LocalParent", "Prefab", "ObjectWithPositions",
        "onClick",
    };

    public List<string> TypeToSplit = new List<string>()
    {
        "Vector3", "Vector2", "Color", "Quaternion"
    };


    private string _objectId;
    private string _objectName;
    private string _jsonString;
    private string _stringJsonFile;
    private bool _needProperty = true;
    private bool _needFields = false;
    private IConfiguration transformConfiguration = new TransformConfiguration();
    private IConfiguration baseConfiguration = new BaseConfiguration();
    private IConfiguration contentConfiguration = new ContentConfiguration();
    private IConfiguration textConfiguration = new TextConfiguration();

    public string ObjectId
    {
        get => _objectId;
        set => _objectId = value;
    }

    public string ObjectName
    {
        get => _objectName;
        set => _objectName = value;
    }

    // [Button]
    // public List<Desktop> CreateAllJson(GameObject obj)
    // {
    //     List<Desktop> allJson = new List<Desktop>();
    //
    //     allJson.Add(CreateUnityJson(obj));
    //
    //     // allJson.Add(CreateUserJson(objects));
    //
    //     return allJson;
    // }

    public JsonUnity CreateUnityJson(GameObject obj, JsonUnity jsonFile)
    {
        // jsonFile.MYTJson.Find(x => x.MYTName == StructJson.Main.ToString()).MYTValue = Zero(obj);
        // jsonFile.MYTJson.Find(x => x.MYTName == StructJson.Where.ToString()).MYTValue = Three(obj);
        // jsonFile.MYTJson.Find(x => x.MYTName == StructJson.Two.ToString()).MYTValue = Two(objects);
        jsonFile = SixUnity(obj, jsonFile);

        return jsonFile;
    }
//todo переделать!! изменился состав раздела zero

    public List<IdNameValue> Zero(GameObject obj)
    {
        List<IdNameValue> idNameValue = new List<IdNameValue>();
        idNameValue.Add(new IdNameValue(obj.name));

        return idNameValue;
    }

    // public List<IdNameValue> Two(List<GameObject> objects)
    // {
    //     List<IdNameValue> idNameValue = new List<IdNameValue>();
    //
    //     idNameValue.Add(SetNameAndValue("MYTCover", "---"));
    //     idNameValue.Add(SetNameAndValue("MYTCoverAddress", "---"));
    //
    //     idNameValue.Add(SetNameAndValue("MYTCoverHover", "---"));
    //     idNameValue.Add(SetNameAndValue("MYTCoverHoverAddress", "---"));
    //
    //     idNameValue.Add(SetNameAndValue("MYTCoverPressed", "---"));
    //     idNameValue.Add(SetNameAndValue("MYTCoverPressedAddress", "---"));
    //
    //     idNameValue.Add(SetNameAndValue("MYTCoverSelected", "---"));
    //     idNameValue.Add(SetNameAndValue("MYTCoverSelectedAddress", "---"));
    //
    //     idNameValue.Add(SetNameAndValue("MYTCoverDisabled", "---"));
    //     idNameValue.Add(SetNameAndValue("MYTCoverDisabledAddress", "---"));
    //
    //     return idNameValue;
    // }

    public List<IdNameValue> Three(GameObject objects)
    {
        List<IdNameValue> idNameValue = new List<IdNameValue>();

        if (objects.GetComponent<PrefabName>().Prefab == Prefab.MYTVideo)
        {
            idNameValue.Add(SetNameAndValue("MYTVideo", "---"));
        }

        if (objects.GetComponent<PrefabName>().Prefab == Prefab.MYTImage)
        {
            idNameValue.Add(SetNameAndValue("MYTImage", "---"));
        }

        return idNameValue;
    }

    public JsonUnity SixUnity(GameObject obj, JsonUnity mainJson)
    {
        // IdNameValue idNameValue = SetMyt(obj, mainJson.MYTJson.Find(x => x.MYTName == "MYTPrefab"));
        // mainJson.MYTJson.Find(x => x.MYTName == "MYTPrefab")
        //     .MYTValue = idNameValue.MYTValue;
        mainJson = GetBaseParameters(obj, mainJson);
        return mainJson;
    }

    public IdNameValue SetMyt(ShowableArea objects, SwitcherValues values)
    {
        IdNameValue _comp = new IdNameValue();
        _comp.MYTID = values.ToString();
        _comp.MYTName = values.ToString();
        foreach (var VARIABLE in objects.CanDropInMe)
        {
            _comp.MYTValue.Add(new IdNameValue() { MYTID = VARIABLE.ToString(), MYTName = VARIABLE.ToString() });
        }

        return _comp;
    }

    // /// <summary>
    // /// Меняем конкретный компонент в разделе data
    // /// </summary>
    // /// <param name="objects">Объект у кого берем параметры</param>
    // /// <param name="values"> </param>
    // /// <param name="mainJson"> сомпонент начинающийся с MYTPrefab</param>
    // /// <returns></returns>
    // public JsonUnity SetMyt(GameObject objects, JsonUnity mainJson)
    // {
    //     // IdNameValue newComp = GetComponentFromObj(objects, objects.GetComponent<PrefabName>().Prefab.ToString());
    //
    //     IdNameValue newComp = GetBaseParameters(objects, objects.GetComponent<PrefabName>().Prefab.ToString());
    //     mainJson.MYTValue.Find(x => x.MYTName == objects.GetComponent<PrefabName>().Prefab.ToString()).MYTValue =
    //         newComp.MYTValue;
    //
    //     return mainJson;
    // }

    public JsonUnity GetBaseParameters(GameObject _gameObject, JsonUnity jsonUnity)
    {
        // IdNameValue folder = new IdNameValue();
        // folder.MYTID = _gameObject.name.GetHashCode().ToString();
        // folder.MYTName = prefab.ToString();
        // IdNameValue scaleValue = new IdNameValue(SwitcherValues.MYTTransform.ToString());
        // IdNameValue baseValue = new IdNameValue(SwitcherValues.MYTBase.ToString());
        // IdNameValue contentValue = new IdNameValue(SwitcherValues.MYTContent.ToString());
        // IdNameValue textValue = new IdNameValue(SwitcherValues.MYTText.ToString());
        //
        // folder.MYTValue.Add(baseConfiguration.Get(_gameObject, jsonUnity));
        // folder.MYTValue.Add(transformConfiguration.Get(_gameObject, jsonUnity));
        // folder.MYTValue.Add(contentConfiguration.Get(_gameObject, jsonUnity));
        // folder.MYTValue.Add(textConfiguration.Get(_gameObject, jsonUnity));

        baseConfiguration.Get(_gameObject, jsonUnity);
        transformConfiguration.Get(_gameObject, jsonUnity);
        contentConfiguration.Get(_gameObject, jsonUnity);
        textConfiguration.Get(_gameObject, jsonUnity);
            
        return jsonUnity;
    }

    public IdNameValue GetComponentFromObj(GameObject _gameObject, String prefab, String nameComponent = null)
    {
        IdNameValue folder = new IdNameValue();
        folder.MYTID = _gameObject.name.GetHashCode().ToString();
        folder.MYTName = prefab;
        Component[] myComponents = _gameObject.GetComponents<Component>();

        if (String.IsNullOrEmpty(nameComponent))
        {
            foreach (Component myComp in myComponents)
            {
                if (myComp != null)
                {
                    if (myComp.GetType().Name == "Shape")
                    {
                        Shape.UserProps test = _gameObject.GetComponent<Shape>().settings;
                        folder.MYTValue.Add(GetPropertiesFromComponent(test));
                    }

                    //for properties
                    if (_needProperty)
                    {
                        IdNameValue prop = new IdNameValue();
                        prop = GetPropertiesFromComponent(myComp);
                        if (prop.MYTValue.Count != 0)
                            folder.MYTValue.Add(prop);
                    }
                }
                else
                {
                    Debug.LogError(myComp);
                }
            }

            if (_gameObject.GetComponent<ShowableArea>() != null)
            {
                folder.MYTValue.Add(
                    SetMyt(_gameObject.GetComponent<ShowableArea>(), SwitcherValues.MYTPrefabChildTypes));
            }
        }
        else
        {
            IdNameValue prop = new IdNameValue();
            Debug.LogError(nameComponent);
            prop = GetPropertiesFromComponent(
                myComponents.First(component => component.GetType().Name == nameComponent));
            if (prop.MYTValue.Count != 0)
                folder.MYTValue.Add(prop);
        }

        if (folder.MYTValue.Count == 0)
            return null;

        return folder;
    }


    public IdNameValue GetPropertiesFromComponent(Object component)
    {
        IdNameValue idNameValues = new IdNameValue();
        idNameValues.SetIdNameValue(component);
        foreach (var thisVar in component.GetType().GetProperties())
        {
            try
            {
                if (thisVar.CanWrite && !UnNeededInJson.Contains(thisVar.Name))
                {
                    if (TypeToSplit.Find(x => x == thisVar.PropertyType.Name) != null)
                        idNameValues.MYTValue.Add(SplitValue(thisVar, component));
                    else
                    {
                        IdNameValue _idNameValue = new IdNameValue() { MYTID = thisVar.Name, MYTName = thisVar.Name };
                        _idNameValue.MYTValue.Add(new IdNameValue()
                        {
                            MYTID = thisVar.GetValue(component).ToString(),
                            MYTName = thisVar.GetValue(component).ToString()
                        });
                        idNameValues.MYTValue.Add(_idNameValue);
                    }
                }
            }
            catch (Exception e)
            {
                //                Debug.LogError(e);
            }
        }


        return idNameValues;
    }

    public IdNameValue SplitValue(PropertyInfo prop, Object component)
    {
        IdNameValue _idNameValue = new IdNameValue() { MYTID = prop.Name, MYTName = prop.Name };

        //        List<String> split = prop.GetValue(component).ToString().Split(',').ToList();
        //Debug.LogError("зашел в разбиение компонентов с компонентом "+ prop.PropertyType.Name);
        switch (prop.PropertyType.Name)
        {
            case "Vector3":
            {
                Vector3 vector3 = CreateObjFromJson.StringToVector3(prop.GetValue(component).ToString(), ',');
                _idNameValue.MYTValue.Add(SetNameAndValue("x", vector3.x.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("y", vector3.y.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("z", vector3.z.ToString()));
                break;
            }

            case "Vector2":
            {
                //                Debug.LogError(prop.GetValue(component));
                Vector2 vector2 = CreateObjFromJson.StringToVector2(prop.GetValue(component).ToString(), ',');

                _idNameValue.MYTValue.Add(SetNameAndValue("x", vector2.x));
                _idNameValue.MYTValue.Add(SetNameAndValue("y", vector2.y));

                break;
            }

            case "Quaternion":
            {
                Quaternion quaternion = CreateObjFromJson.StringToQuaternion(prop.GetValue(component).ToString(), ',');
                _idNameValue.MYTValue.Add(SetNameAndValue("x", quaternion.x.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("y", quaternion.y.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("z", quaternion.z.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("w", quaternion.w.ToString()));

                break;
            }

            case "Color":
            {
                Color color = CreateObjFromJson.StringToColor(prop.GetValue(component).ToString(), ',');
                _idNameValue.MYTValue.Add(SetNameAndValue("r", color.r.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("g", color.g.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("b", color.b.ToString()));
                _idNameValue.MYTValue.Add(SetNameAndValue("a", color.a.ToString()));

                break;
            }
        }

        return _idNameValue;
    }

    public IdNameValue SetNameAndValue(String Name, Object Value)
    {
        // Config.IdNameValueName.MYTValue.Clear();
        // Config.IdNameValueValue.MYTValue.Clear();
        //
        // Config.IdNameValueName.MYTID = Name;
        // Config.IdNameValueName.MYTName = Name;
        //
        // Config.IdNameValueValue.MYTID = Value.ToString();
        // Config.IdNameValueValue.MYTName = Value.ToString();
        //
        // Config.IdNameValueName.MYTValue.Add(Config.IdNameValueValue);
        // var test = Config.IdNameValueName;
        IdNameValue _idNameValue = new IdNameValue() { MYTID = Name, MYTName = Name };
        _idNameValue.MYTValue.Add(new IdNameValue() { MYTID = Value.ToString(), MYTName = Value.ToString() });

        return _idNameValue;
    }

    public JsonUnity Update(JsonUnity json, IdNameValue chapter)
    {
        for (int i = 0; i < json.MYTJson.Find(x => x.MYTName == chapter.MYTName).MYTValue.Count; i++)
        {
            if (json.MYTJson.Find(x => x.MYTName == chapter.MYTName).MYTValue[i].MYTName == chapter.MYTValue[0].MYTName)
            {
                json.MYTJson.Find(x => x.MYTName == chapter.MYTName).MYTValue[i] = ChangeChapter(
                    json.MYTJson.Find(x => x.MYTName == chapter.MYTName).MYTValue[i], chapter.MYTValue[0]);
                break;
            }
        }

        return json;
    }

    private IdNameValue ChangeChapter(IdNameValue oldChapter, IdNameValue newChapter)
    {
        if (oldChapter.MYTValue[0].MYTValue.Count == 0)
        {
            oldChapter = newChapter;
        }
        else
        {
            for (int i = 0; i < oldChapter.MYTValue.Count; i++)
            {
                if (oldChapter.MYTValue[i].MYTName == newChapter.MYTValue[0].MYTName)
                {
                    oldChapter.MYTValue[i] =
                        ChangeChapter(oldChapter.MYTValue.Find(x => x.MYTName == newChapter.MYTValue[0].MYTName),
                            newChapter.MYTValue[0]);
                    break;
                }
            }
        }

        return oldChapter;
    }
}