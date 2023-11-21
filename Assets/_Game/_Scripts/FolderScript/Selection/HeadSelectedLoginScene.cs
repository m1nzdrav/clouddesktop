using _Game._Scripts.Button.RadialMenu;
using _Game._Scripts.Button.Rotate;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using UnityEngine;
using UnityEngine.UI;

public class HeadSelectedLoginScene : MonoBehaviour, IHeadSelected
{
    public string NameComponent
    {
        get => name;
    }

    [SerializeField] private SelectedWindow currentSelectedWindow;

    public SelectedWindow CurrentSelectedWindow
    {
        get => currentSelectedWindow;
        set => currentSelectedWindow = value;
    }

    public Transform CurrentSelectedWindowTransform => currentSelectedWindow?.transform;

    [SerializeField] private GameObject _oldGlow;

    [SerializeField] private RadialMenu _buttonVideoRotate;

    public RadialMenu ButtonVideoRotate
    {
        get => _buttonVideoRotate;
        set => _buttonVideoRotate = value;
    }

    public void CheckRegister()
    {
    }

    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        else
        {
            transform.SetParent(RegisterSingleton._instance.transform);
            CheckRegister();
        }
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

            if (currentSelectedWindow != null && currentSelectedWindow.GetComponent<MytPrefabColorTypes>() != null)
            {
                Image oldBorder = currentSelectedWindow.GetComponent<MytPrefabColorTypes>().ParentBorder;
                oldBorder?.GetComponent<ActiveFolderAnimation>()?.StopAnimation();
            }
        }
        
        if (newWindow is null)
        {
            // закрытие кнопки поворота
            // OpenManager._instance.CurrentArea?.TopPanelSetParametrs.BtnRotate.GetComponent<ButtonVideoRotate>()
            //     .HideWithVhangePosition();
            _buttonVideoRotate.RemoveSelectedState();

            currentSelectedWindow?.SetMainSelected(false);
            if (_oldGlow != null)
            {
                _oldGlow.SetActive(false);
            }

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
            // закрытие кнопки поворота
            // OpenManager._instance.CurrentArea.TopPanelSetParametrs.BtnRotate.GetComponent<ButtonVideoRotate>()
            //     .HideWithVhangePosition();
            // RegisterSingleton._instance.OpenManager.CurrentArea.TopPanelSetParametrs.BtnRotate
            //     .GetComponent<ButtonVideoRotate>().HideWithVhangePosition();
            EqualsArea();
            return;
        }

        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.RemoveOpenedPrefab(
            newWindow.GetComponent<PrefabName>());
        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.SetOpenedPrefab(
            newWindow.GetComponent<PrefabName>());

        currentSelectedWindow = newWindow;
        currentSelectedWindow.SetMainSelected(true);
        _buttonVideoRotate.SetSelectedState();

        (RegisterSingleton._instance.GetSingleton(typeof(HeadRotate)) as HeadRotate)?.ChangePrefabSettingButton();

        if (newWindow.GetComponent<MytPrefabColorTypes>() != null)
        {
            Image border = newWindow?.GetComponent<MytPrefabColorTypes>().ParentBorder;
            GameObject glow = border?.GetComponent<RotateGlow>()?.Glow;

            if (_oldGlow != null)
            {
                _oldGlow.SetActive(false);
                _oldGlow = null;
            }

            if (glow != null)
            {
                glow.SetActive(true);

                _oldGlow = glow;
            }

            border?.GetComponent<ActiveFolderAnimation>()?.StartAnimation();
        }
        else
            Debug.LogError("нет border");
    }

    public void RemoveSelectedWindow(SelectedWindow window)
    {
        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.RemoveOpenedPrefab(
            window.GetComponent<PrefabName>());
    }

    /// <summary>
    /// При равной oldWindow и newWindow
    /// </summary>
    private void EqualsArea()
    {
        _buttonVideoRotate.RemoveSelectedState();
        if (_oldGlow != null)
        {
            _oldGlow.SetActive(!_oldGlow.activeSelf);
            // currentSelectedWindow.SetMainSelected(_oldGlow.activeSelf);
        }

        // RegisterSingleton._instance.OpenedStock.RemoveOpenedPrefab(currentSelectedWindow.GetComponent<PrefabName>());
        // RegisterSingleton._instance.OpenedStock.SetOpenedPrefab(newWindow.GetComponent<PrefabName>());

        // RegisterSingleton._instance?.HeadRotate.ChangeColorRotateButton();
        currentSelectedWindow = null;
        _oldGlow = null;
    }
}