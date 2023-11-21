using System.Collections;
using System.Collections.Generic;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using UnityEngine;
using UnityEngine.Video;

public class Video_fileManager : FileManager
{
    VideoPlayer videoPlayer;

    public Video_fileManager(VideoPlayer videoPlayer)
    {
        this.videoPlayer = videoPlayer;
    }
    
    protected override void AddAsIcon(string path = null, byte[] data = null)
    {
        data = null;//TODO: you should to delete this string if you wanna use data
        
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = "file://" + path;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        RenderTexture renderTexture = new RenderTexture(256, 256, 1);
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.Play();
        videoPlayer.time = 1;

        Parent parent = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab[0].GetComponent<Parent>();
        DataForObject dataForObject = new DataForObject();
        dataForObject.AddNewFields(texture: videoPlayer.targetTexture, name:"video", sourcePath: path);
        (RegisterSingleton._instance.GetSingleton(typeof(ManagerJson)) as ManagerJson)?.SetNew(Prefab.MYTVideo, parent, dataForObject);
            
        videoPlayer.Pause();
    }
}
