
using UnityEngine;

public class LocalDownloadBundle : MonoBehaviour
{
    public void DownloadAllScene()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(LanguageBundleSaver)) as LanguageBundleSaver)?.AllScene();
    }
}