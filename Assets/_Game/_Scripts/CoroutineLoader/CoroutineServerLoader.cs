using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CoroutineServerLoader : MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        
        transform.SetParent(RegisterSingleton._instance.transform);
    }
    public void StartDownload(LoadPaket loadPaket)
    {
        StartCoroutine(Download(loadPaket));
    }

    private IEnumerator Download(LoadPaket loadPaket)
    {
        UnityWebRequest Query = UnityWebRequest(loadPaket);

        yield return Query.SendWebRequest();

        SaveResult(loadPaket, Query);
        Query.Dispose();
    }

    private static void SaveResult(LoadPaket loadPaket, UnityWebRequest Query)
    {
        if (Query.isDone)
        {
            loadPaket.answer = Query.downloadHandler.text;
            loadPaket.finitLoad.Invoke();
        }
        else
            Debug.LogError(Query.error);

        loadPaket.isDone = true;
    }

    private UnityWebRequest UnityWebRequest(LoadPaket loadPaket)
    {
        return loadPaket.dataForm == null
            ? UnityEngine.Networking.UnityWebRequest.Get(loadPaket.url)
            : UnityEngine.Networking.UnityWebRequest.Post(loadPaket.url, loadPaket.dataForm);
    }
}