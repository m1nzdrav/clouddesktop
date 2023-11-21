using System;

using UnityEngine;
using UnityEngine.UI;

public class UserFormModel : FolderModel
{
//     public GameObject CreateFromPool(Transform _parent, GameObject whoCalls, Prefab prefab, GameObject _icon = null)
//     {
//         Debug.LogError("Enter");
// //        GameObject folder = Instantiate(Prefab, _parent,true);
//         GameObject folder = ObjectPoolManager.instanse.ChildFormComponentWindowPool.GetObject();
//         folder.transform.SetParent(_parent);
//         
//         ParentChild _folderParentChild = folder.GetComponent<ParentChild>();
//
//         if (_icon != null)
//         {
// //            Debug.LogError( folder.GetComponent<PrefabName>().Prefab);
//             FolderModel _folderModel = folder.GetComponent<FolderModel>();
//             MytPrefabColorTypes folderMytPrefabColorTypes = folder.GetComponent<MytPrefabColorTypes>();
//             ModifyFromToJson _folderModifyFromToJson = folder.GetComponent<ModifyFromToJson>();
//             ShowableArea _folderShowableArea = folder.GetComponent<ShowableArea>();
//
//             _folderModel.Icon = _icon.GetComponent<IconModel>();
//
//             _folderShowableArea.CantDropInMe.Add(_icon.GetComponent<ShowableArea>());
//
//             //SetBorder from parent
//             folderMytPrefabColorTypes.ParentBorder.color =
//                 whoCalls.GetComponent<MytPrefabColorTypes>().LastSelectedColor;
//
//             folderMytPrefabColorTypes.SetColor();
//
//             _folderModel.Icon.GetComponent<Image>().color = folderMytPrefabColorTypes.ParentBorder.color;
//
//             //GetGridControllerRuntime Name
//
//             _folderModel.DefaultName = Config.DEFAULT_NAME_FOLDER;
//
// //            Debug.LogError("folder.name " + folder.name);
// //            Debug.LogError("NeedCreateJson " + NeedCreateJson);
//
//             if (NeedCreateJson != null)
//             {
//                 folder.name = NeedCreateJson;
//             }
//
//             _folderModifyFromToJson.ObjWhoNeedCreateJson.Add(folder);
//             _folderModifyFromToJson.ObjWhoNeedCreateJson.Add(_icon);
//
//             _folderParentChild.Parent = whoCalls;
//
//             whoCalls.GetComponent<ParentChild>().AddChild(null, folder);
//
//
//             ManagerJson.instance.AddNeedCreateJson(_folderModifyFromToJson);
//
//
//             _folderModel.TopPanelSetParametrs.SetParametrs(folder, _icon);
//             folder.transform.position = _folderModel.Icon.transform.position;
//             try
//             {
//                 SetUnSettableParam(whoCalls.GetComponent<ShowableArea>(), _folderShowableArea,
//                     _icon.GetComponent<ShowableArea>(), _folderModel.AllAreas);
//             }
//             catch (Exception e)
//             {
//             }
//
//             if (NeedCreateJson != null)
//             {
//                 ManagerJson.instance.CreateJson(folder.name, NeedCreateJson);
//             }
//             else
//             {
//                 ManagerJson.instance.CreateJson(folder.name);
//             }
//         }
//
//
//         folder.SetActive(false);
//         try
//         {
//             SetJson(_folderParentChild.Parent);
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("Нет указанного объекта " + _folderParentChild.Parent.name);
//         }
//
//         return folder;
//     }
}