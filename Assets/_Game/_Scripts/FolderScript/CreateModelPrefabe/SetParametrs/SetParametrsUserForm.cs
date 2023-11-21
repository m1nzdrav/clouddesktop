using System;
using UnityEngine;
using UnityEngine.UI;

public class SetParametrsUserForm : MonoBehaviour
{
    [SerializeField] private HideMe btnHideMe;
    
    [SerializeField] private Text name;
    
    [SerializeField] private Transform rotatedBtn;

    [SerializeField] private Button btnFullScreen;
    [SerializeField] private Border _border;

    public String Name
    {
        get => name.text;
        set => name.text = value;
    }
    public void SetParametrs(GameObject _btn,GameObject _folder)
    {
        btnHideMe.Button = _btn.GetComponent<Button>();
        // GetComponent<ModelForm>().Folder = _folder;
        
        rotatedBtn.parent = transform;

        FullSmallSize fullSmallSize = GetComponent<FullSmallSize>();
        if (fullSmallSize!= null)
        {
            btnFullScreen.onClick.AddListener(fullSmallSize.ChangeFullSmallSize);
        }

        GetComponent<UserJson>().Folder = _folder;
        
        _border.NumberHierarchyText.text = _folder.GetComponent<ParentChild>().FolderNumberHierarchy.ToString();
//        Name = _folder.GetComponent<FolderModel>().DefaultName;
    }
}
