using _Game._Scripts.Button.Rotate;
using UnityEngine;
using UnityEngine.UI;

public class HeadSelected : MonoBehaviour, IHeadSelected
{ 

    public string NameComponent
    {
        get => name;
    }
    public static IHeadSelected instance;

    [SerializeField] private SelectedWindow currentSelectedWindow;

    public SelectedWindow CurrentSelectedWindow
    {
        get => currentSelectedWindow;
        set => currentSelectedWindow = value;
    }

    public Transform CurrentSelectedWindowTransform => currentSelectedWindow.transform;

    private GameObject _oldGlow;


    public void CheckRegister()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void CheckSelectedObj(SelectedWindow newWindow)
    {
        SetCurrentSelectedWindow(newWindow);
    }

    public void SetCurrentSelectedWindow(SelectedWindow newWindow)
    {
        if (!(currentSelectedWindow is null))
        {
            currentSelectedWindow.SetMainSelected(false);
            var oldBorder = currentSelectedWindow.GetComponent<MytPrefabColorTypes>().ParentBorder;
            oldBorder.GetComponent<ActiveFolderAnimation>().StopAnimation();
        }

        Debug.LogError(newWindow);
        if (newWindow is null)
        {
// закрытие кнопки поворота
            // OpenManager._instance.CurrentArea?.TopPanelSetParametrs.BtnRotate.GetComponent<ButtonVideoRotate>()
            //     .HideWithVhangePosition();


            currentSelectedWindow?.SetMainSelected(false);
            _oldGlow?.SetActive(false);
            currentSelectedWindow = null;
            _oldGlow = null;
            return;
        }

        SelectedWindow oldWindow = currentSelectedWindow;

        //if (oldWindow != null)
        //{
        //    var oldBorder = oldWindow.GetComponent<MytPrefabColorTypes>().ParentBorder;
        //    oldBorder.GetComponent<ActiveFolderAnimation>().StopAnimation();
        //}

        if (oldWindow == newWindow)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.CurrentArea.TopPanelSetParametrs.BtnRotate
                .GetComponent<ButtonVideoRotate>()
                .HideWithVhangePosition();
            currentSelectedWindow.SetMainSelected(_oldGlow.activeSelf);
            _oldGlow.SetActive(false);
            currentSelectedWindow = null;
            _oldGlow = null;
            return;
        }
        else
        {
            (RegisterSingleton._instance.GetSingleton(typeof(OpenManager)) as OpenManager)?.CurrentArea.TopPanelSetParametrs.BtnRotate
                .GetComponent<ButtonVideoRotate>().Show();
            currentSelectedWindow = newWindow;
            currentSelectedWindow.SetMainSelected(true);
        }

        (RegisterSingleton._instance.GetSingleton(typeof(HeadRotate)) as HeadRotate)?.ChangePrefabSettingButton();

        Image border = newWindow.GetComponent<MytPrefabColorTypes>().ParentBorder;
        GameObject glow = border.GetComponent<RotateGlow>().Glow;


        if (glow != null)
        {
            glow.SetActive(true);

            if (_oldGlow != null && glow != _oldGlow)
            {
                _oldGlow.SetActive(false);
            }

            _oldGlow = glow;
        }
        else if (_oldGlow != null)
        {
            _oldGlow.SetActive(false);
            _oldGlow = null;
        }

        border.GetComponent<ActiveFolderAnimation>().StartAnimation();
    }
}