using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;

public class Image_fileManager : FileManager
{
    private Texture2D _texture2D; //= new Texture2D(1, 1);
    
    protected override void AddAsIcon(string path = null, byte[] data = null)
    {
        /*Texture2D texture2D = new Texture2D(1, 1);
        texture2D.LoadImage(data);*/
        path = "file:///" + path;
        
        //Texture2D texture2D 
        (RegisterSingleton._instance.GetSingleton(typeof(TextureLoader)) as TextureLoader).OnDownloadedEvent += OnDownloaded;
        (RegisterSingleton._instance.GetSingleton(typeof(TextureLoader)) as TextureLoader).DownloadTexture(_texture2D, path);
    }

    void OnDownloaded(Texture2D resultTexture2D)
    {
        Debug.Log(resultTexture2D.width + ", " + resultTexture2D.height);
        
        Parent parent = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab[0].GetComponent<Parent>();
        DataForObject dataForObject = new DataForObject();
        dataForObject.AddNewFields(texture: resultTexture2D, name: "image");
        
        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetNew(Prefab.MYTImage, parent, dataForObject);

        (RegisterSingleton._instance.GetSingleton(typeof(TextureLoader)) as TextureLoader).OnDownloadedEvent -= OnDownloaded;
    }
}