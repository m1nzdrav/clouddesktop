using System;
using _Game._Scripts;
using _Game._Scripts.Panel;
using UnityEngine;
using UnityEngine.UI;

public class SetParametrsAction : MonoBehaviour
{
    [SerializeField] private HideMe btnHideMe;

    [SerializeField] private Button createNewFolder,
        createNewVideo,
        createNewAudio,
        createNewImage,
        createNew3dHall,
        deleteBtn,
        createNewPrezentation,
        createNewPromo1,
        createNewPromo2;

    [SerializeField] private Transform rotatedBtn;
    [SerializeField] private Text name;
    [SerializeField] private GameObject folder;
    [SerializeField] private Border _border;

    public String Name
    {
        get => name.text;
        set => name.text = value;
    }

    public void SetParametrs(GameObject _btn, GameObject _folder)
    {
        folder = _folder;
        btnHideMe.Button = _btn.GetComponent<Button>();
//        createNewFolder.onClick.AddListener(CreateNewFolder);
        createNewVideo.onClick.AddListener(CreateNewVideo);
        createNewAudio.onClick.AddListener(CreateNewAudio);
        createNewImage.onClick.AddListener(CreateNewImage);
        createNew3dHall.onClick.AddListener(CreateNew3dHall);
        createNewPrezentation.onClick.AddListener(CreateNewTwoVideos);
        createNewPromo1.onClick.AddListener(CreateNewPromo1);
        createNewPromo2.onClick.AddListener(CreateNewPromo2);

        deleteBtn.onClick.AddListener(DeleteBtn);

        rotatedBtn.parent = transform;
        _border.NumberHierarchyText.text = _folder.GetComponent<ParentChild>().FolderNumberHierarchy.ToString();
//        Name = _folder.GetComponent<FolderModel>().DefaultName;
    }

    public void CreateNewFolder()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetNew(Prefab.MYTFolderNewDesign, 
            folder.GetComponent<Parent>());
        // folder.GetComponent<FolderModel>().CreateBtn(Prefab.MYTFolderNewDesign);
    }

    public void CreateColoredFolder(Color testColor)
    {
        folder.GetComponent<MytPrefabColorTypes>().LastSelectedColor = testColor;

        //var folderGridController = folder.GetComponent<GridController>();
        //var cellGlow = folderGridController.Cell.GetChild(0).GetComponent<Image>();
        //var saveColor = testColor;
        //saveColor.a = 0.3921569f;
        //cellGlow.color = saveColor;

        CreateNewFolder();
    }

    public void CreateNewVideo()
    {
        folder.GetComponent<CreateModelPrefab>().CreateBtn(Prefab.MYTVideo);
    }

    public void CreateNewAudio()
    {
        folder.GetComponent<CreateModelPrefab>().CreateBtn(Prefab.MYTAudio);
    }

    public void CreateNewImage()
    {
        folder.GetComponent<CreateModelPrefab>().CreateBtn(Prefab.MYTImage);
    }

    public void CreateNew3dHall()
    {
        folder.GetComponent<CreateModelPrefab>().CreateBtn(Prefab.MYT3dHall);
    }

    public void CreateNewTwoVideos()
    {
        folder.GetComponent<CreateModelPrefab>().CreateBtn(Prefab.MYTPrezentation);
    }

    public void CreateNewPromo2()
    {
        folder.GetComponent<CreateModelPrefab>().CreateBtn(Prefab.TwoVideos1);
    }

    public void CreateNewPromo1()
    {
        folder.GetComponent<CreateModelPrefab>().CreateBtn(Prefab.MYTPromo1);
    }

    public void DeleteBtn()
    {
        folder.GetComponent<ParentChild>().Parent.GetComponent<DeleteModelPrefab>().DeletePrefabe(folder.name);
    }
    void OnNameChanged_FolderAction(string newName) 
    {
        Name = newName;
    }
}