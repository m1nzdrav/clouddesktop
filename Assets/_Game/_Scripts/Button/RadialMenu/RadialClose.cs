using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using UnityEngine;
using UnityEngine.UI;

public class RadialClose : MonoBehaviour
{
    public void ChangeClose()
    {
        PrefabName currentWindow =
            (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.GetLastPrefab().GetComponent<PrefabName>();

        switch (currentWindow.Prefab)
        {
            case Prefab.Button:
            {
                // RegisterSingleton._instance.ApplicationManager.Quit();
                break;
            }
            case Prefab.MYTPromo1:
            {
                currentWindow.GetComponent<PlatformButton>().Return();
                break;
            }
            case Prefab.TwoVideos1:
            {
                currentWindow.GetComponent<MultiPrefabElement>().MultiPrefab.Icon.GetComponent<Button>().onClick
                    .Invoke();
                // currentWindow.GetComponent<PlatformButton>().Return();
                break;
            }
            default:
            {
                currentWindow.GetComponent<FolderModel>().Icon.GetComponent<Button>().onClick
                    .Invoke();
                break;
            }
        }
    }
}