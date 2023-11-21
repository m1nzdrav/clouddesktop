using System;
using System.Collections.Generic;
using _Game._Scripts.FolderScript.ShowHide.Folder;
using UnityEngine;
using UnityEngine.UI;

public class TopPanelSetParametrs : MonoBehaviour, ILock
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private HideMe btnHideMe;
    [SerializeField] private BtnHideChild btnHideChild;
    [SerializeField] private Button btnFullScreen;
    [SerializeField] private GameObject btnAction, btnHierarchy, btnForm, btnRotate;

    [SerializeField] private GameObject _folderAction, _folderHierarchy, _folderForm, _rotateVideo, _folderChildForm;

    public GameObject BtnAction => btnAction;

    public GameObject BtnHierarchy => btnHierarchy;

    public GameObject BtnForm => btnForm;

    public GameObject FolderAction => _folderAction;

    public GameObject FolderHierarchy => _folderHierarchy;

    public GameObject FolderForm => _folderForm;

    public GameObject FolderChildForm => _folderChildForm;

    [SerializeField] private Lock lockBtn;
    [SerializeField] private GameObject _mainFolder;

    public GameObject MainFolder => _mainFolder;

    public Action OnClickFullButton;

    public GameObject BtnRotate => btnRotate;

    [SerializeField] private RotateButton rotateButtonSlider;

    private CreateActionHierarchy thisActionHierarchy;
    Transform parentForWindow;

    private void Awake()
    {
        // _mainFolder = transform.parent.gameObject;
        thisActionHierarchy = GetComponent<CreateActionHierarchy>();
        //SetFullScript();
//        SetVideoRotateButton();
    }

    private void OnDestroy()
    {
        if (_mainFolder != null)
        {
            if (OnClickFullButton != null)
            {
                OnClickFullButton -= _mainFolder.GetComponent<FullSmallSize>().ChangeFullSmallSize;
            }
        }
    }

    public void SetParametrs(GameObject _folder, GameObject _icon)
    {
        // Debug.LogError("SetParametrs "+ _folder);
        _mainFolder = _folder;
        IFolderBone folderModel = _mainFolder.GetComponent<IFolderBone>();

        if (GetComponent<FlexibleDraggableObject>() != null)
        {
            GetComponent<FlexibleDraggableObject>().Target = _folder;
        }

        if (folderModel.RotatedArea != null)
        {
            parentForWindow = folderModel.RotatedArea;
        }
        else
            parentForWindow = _folder.transform;

        btnHideMe.Button = _icon.GetComponent<Button>();
        btnHideChild.Folder = _folder.GetComponent<ParentChild>();

        // if (folderModel.DefaultName == "")
        // {
        //     btnRotate.SetActive(true);
        // }


        // rotateButtonSlider.transform.SetParent(_mainFolder.transform);


        SetFullScript();
    }

    public void SetNameActionHierarhy(String name)
    {
        _folderHierarchy.GetComponent<SetParametrsHierarchy>().Name = name;
        _folderForm.GetComponent<SetParametrsJsonForm>().Name = name;
    }


    public void SetLock(bool value)
    {
        if (lockBtn.IsLock != value)
        {
            lockBtn.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void SetParametrsAction()
    {
        if (!btnAction.GetComponent<ShowHideObjectsScaleAndMove>().IsShowed)
        {
            GetComponent<ShowOn>()?.ChangeValueBlocker(typeof(ActionModel).ToString(), true);


            _folderAction = thisActionHierarchy.CreateAction();
            _folderAction.transform.SetParent(parentForWindow);

            _folderAction.transform.localRotation = Quaternion.identity;

            btnAction.GetComponent<ShowHideObjectsScaleAndMove>().ObjectWithPositions = new List<ObjectWithPosition>()
            {
                new ObjectWithPosition()
                    { obj = _folderAction, currentPosition = offset }
            };

            _folderAction.GetComponent<SetParametrsAction>().SetParametrs(btnAction, _mainFolder);

            _folderAction.GetComponent<MytPrefabColorTypes>().ParentBorder.color =
                _mainFolder.GetComponent<MytPrefabColorTypes>().ParentBorder.color;
            _folderAction.GetComponent<MytPrefabColorTypes>().SetColor();

            //todo присвоение имени
        }
        else
        {
            _folderAction = null;
            GetComponent<ShowOn>()?.ChangeValueBlocker(typeof(ActionModel).ToString(), false);
        }
    }

    public void SetParametrsHierarchy()
    {
        if (!btnHierarchy.GetComponent<ShowHideObjectsScaleAndMove>().IsShowed)
        {
            GetComponent<ShowOn>()?.ChangeValueBlocker(typeof(HierarchyModel).ToString(), true);


            _folderHierarchy = thisActionHierarchy.CreateHierarchy();
            _folderHierarchy.transform.SetParent(parentForWindow);

            _folderHierarchy.transform.localRotation = Quaternion.identity;

            btnHierarchy.GetComponent<ShowHideObjectsScaleAndMove>().ObjectWithPositions =
                new List<ObjectWithPosition>()
                    { new ObjectWithPosition() { obj = _folderHierarchy, currentPosition = offset * 2 } };


            _folderHierarchy.GetComponent<SetParametrsHierarchy>().SetParametrs(btnHierarchy, _mainFolder);

            _folderHierarchy.GetComponent<MytPrefabColorTypes>().ParentBorder.color =
                _mainFolder.GetComponent<MytPrefabColorTypes>().ParentBorder.color;
            _folderHierarchy.GetComponent<MytPrefabColorTypes>().SetColor();

            //todo присвоение имени
        }
        else
        {
            _folderHierarchy = null;
            GetComponent<ShowOn>()?.ChangeValueBlocker(typeof(HierarchyModel).ToString(), false);
        }
    }

    public void SetParametrsForm()
    {
        if (!btnForm.GetComponent<ShowHideObjectsScaleAndMove>().IsShowed)
        {
            GetComponent<ShowOn>()?.ChangeValueBlocker(typeof(ModelForm).ToString(), true);


            _folderChildForm = thisActionHierarchy.CreateChildForm();
            _folderChildForm.transform.SetParent(parentForWindow);

            _folderChildForm.transform.localRotation = Quaternion.identity;

            _folderForm = thisActionHierarchy.CreateForm();
            _folderForm.transform.SetParent(parentForWindow);

            _folderForm.transform.localRotation = Quaternion.identity;

            btnForm.GetComponent<ShowHideObjectsScaleAndMove>().ObjectWithPositions = new List<ObjectWithPosition>()
            {
                new ObjectWithPosition() { obj = _folderForm, currentPosition = offset * 3 },
                new ObjectWithPosition() { obj = _folderChildForm, currentPosition = offset * 4 }
            };

            _folderForm.GetComponent<SetParametrsJsonForm>().SetParametrs(btnForm, _mainFolder);
            _folderChildForm.GetComponent<SetParametrsUserForm>().SetParametrs(btnForm, _mainFolder);

            // _folderChildForm.GetComponent<UserJson>().Json = _mainFolder.GetComponent<ModifyFromToJson>().UserJson;

            _folderForm.GetComponent<MytPrefabColorTypes>().ParentBorder.color =
                _mainFolder.GetComponent<MytPrefabColorTypes>().ParentBorder.color;
            _folderForm.GetComponent<MytPrefabColorTypes>().SetColor();

            _folderChildForm.GetComponent<MytPrefabColorTypes>().ParentBorder.color =
                _mainFolder.GetComponent<MytPrefabColorTypes>().ParentBorder.color;
            _folderChildForm.GetComponent<MytPrefabColorTypes>().SetColor();

            //todo присвоение имени
        }
        else
        {
            GetComponent<ShowOn>()?.ChangeValueBlocker(typeof(ModelForm).ToString(), false);

            _folderChildForm = null;
            _folderForm = null;
        }
    }

    public void DeleteActionWindow()
    {
        if (_folderAction != null)
        {
            _folderAction.SetActive(false);
            _folderAction.transform.SetParent(
                (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.transform);
        }

        if (_folderHierarchy != null)
        {
            _folderHierarchy.SetActive(false);
            _folderHierarchy.transform.SetParent(
                (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.transform);
        }

        if (_folderForm != null)
        {
            _folderForm.SetActive(false);
            _folderForm.transform.SetParent(
                (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.transform);
        }

        if (_folderChildForm != null)
        {
            _folderChildForm.SetActive(false);
            _folderChildForm.transform.SetParent(
                (RegisterSingleton._instance.GetSingleton(typeof(ObjectPoolManager)) as ObjectPoolManager)?.transform);
        }
    }

    //#region FullSmallButton

    private void SetFullScript()
    {
        if (_mainFolder != null && _mainFolder.GetComponent<FullSmallSize>() != null)
            btnFullScreen.onClick.AddListener(_mainFolder.GetComponent<FullSmallSize>().ChangeFullSmallSize);
    }


    #region Locker

    public bool IsLock { get; set; }

    public void Lock()
    {
        if (_folderAction.activeSelf)
        {
            btnAction.GetComponent<ILock>().Unlock();
        }
        else
        {
            btnAction.GetComponent<CircleMenuButtonController>().Lock();
        }
    }

    public void Unlock()
    {
    }

    #endregion
}