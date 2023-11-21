
using System.Collections;
using UnityEngine;

public class TextureLoader: MonoBehaviour, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public delegate void OnDownloaded(Texture2D resultTexture2D);

    public OnDownloaded OnDownloadedEvent;
    
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    //[SerializeField] private RawImage rawImage;
    //private Texture2D _texture2D;

    public IEnumerator DownloadTextureCor(Texture2D texture2D, string url)
    {
        WWW www = new WWW(url);
        while (!www.isDone && string.IsNullOrEmpty(www.error)) {
            yield return null;
        }
        if (string.IsNullOrEmpty(www.error)) {
            texture2D = www.textureNonReadable;
            OnDownloadedEvent.Invoke(texture2D);
        } else {
            Debug.LogWarning("Not found: " + url);
        }
    }

    public void DownloadTexture(Texture2D texture2D, string url)
    {
        StartCoroutine(DownloadTextureCor(texture2D, url));
        /*WWW www = new WWW(url);
        if (string.IsNullOrEmpty(www.error)) {
            texture2D = www.textureNonReadable;
            OnDownloadedEvent.Invoke();
            //Debug.Log(texture2D.height + ", " + texture2D.width);
        } else {
            Debug.LogWarning("Not found: " + url);
        }*/
    }

    /*private void Start()
    {
        string path = "file:///C:/Users/lapoc/Desktop/Папич/photoжop_done/9vcOxGFDBUg.png";
        DownloadTexture(_texture2D, path);
    }*/
    public void CheckRegister()
    {
        
    }
}
