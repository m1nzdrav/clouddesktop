using UnityEngine;
using UnityEngine.UI;

public class BtnHideChild : MonoBehaviour
{
    public ParentChild Folder
    {
        get => folder;
        set => folder = value;
    }

    [SerializeField] private ParentChild folder;



    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClickOnButtonHidingMe);
//        GetComponent<Button>().onClick
    }

    public void ClickOnButtonHidingMe()
    {

        foreach (var VARIABLE in folder.Child)
        {
            var icon = VARIABLE.GetComponent<FolderModel>().Icon;
            IShowHide showHide = icon.GetComponent<IShowHide>();
            if (showHide.IsShowed)
            {
                //                icon.NeedShow = true;
                showHide.ShowHideObject	();
                //icon.GetComponent<IconGlowController>().TurnOff();
            }
        }
    }
}
