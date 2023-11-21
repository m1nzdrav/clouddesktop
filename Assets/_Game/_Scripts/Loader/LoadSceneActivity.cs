using UnityEngine;

public class LoadSceneActivity : Activity
{
    [SerializeField] private string sceneToLoad;

    public override void StartActivity()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(InitLoader)) as InitLoader)?.LoadScene(sceneToLoad);
    }

    public override void EndActivity()
    {
        throw new System.NotImplementedException();
    }
}