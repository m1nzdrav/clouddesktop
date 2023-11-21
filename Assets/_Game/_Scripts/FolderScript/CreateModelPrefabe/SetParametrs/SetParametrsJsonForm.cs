using System;
using UnityEngine;
using UnityEngine.UI;

public class SetParametrsJsonForm : MonoBehaviour
{
    [SerializeField] private HideMe btnHideMe;
    
    [SerializeField] private Text name;
    
    [SerializeField] private Transform rotatedBtn;

    [SerializeField] private ModelForm _modelForm;

    [SerializeField] private Border _border;

    public String Name
    {
        get => name.text;
        set => name.text = value;
    }
    public void SetParametrs(GameObject _btn,GameObject _folder)
    {
        btnHideMe.Button = _btn.GetComponent<Button>();
        _modelForm.Folder = _folder;
        
        rotatedBtn.parent = transform;
        // Debug.LogError(_folder.GetComponent<SetParametrs>().TopPanelSetParametrs.FolderChildForm.GetComponent<UserJson>());
        _modelForm.UserJson = _folder.GetComponent<SetParametrs>().TopPanelSetParametrs.FolderChildForm.GetComponent<UserJson>();
        
        
        _border.NumberHierarchyText.text = _folder.GetComponent<ParentChild>().FolderNumberHierarchy.ToString();
//        Name = _folder.GetComponent<FolderModel>().DefaultName;
    }
}
