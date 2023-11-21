using System;
using UnityEngine;
using UnityEngine.UI;

public class SetParametrsHierarchy : MonoBehaviour
{
    [SerializeField] private HideMe btnHideMe;
    [SerializeField] private Text name;
    [SerializeField] private HierarchyModel _model;
    [SerializeField] private Transform rotatedBtn;

    [SerializeField] private Border _border;

    public String Name
    {
        get => name.text;
        set => name.text = value;
    }

    public void SetParametrs(GameObject _btn, GameObject _folder)
    {
        Debug.LogError("setParam");
        btnHideMe.Button = _btn.GetComponent<Button>();
        FolderModel folderModel = _folder.GetComponent<FolderModel>();
        if (folderModel != null)
        {
            // Name = folderModel.DefaultName;
            _model.folderParentChild = folderModel;
        }

        _model.Folder = folderModel;
        rotatedBtn.parent = transform;

        _border.NumberHierarchyText.text = _folder.GetComponent<ParentChild>().FolderNumberHierarchy.ToString();
    }
}