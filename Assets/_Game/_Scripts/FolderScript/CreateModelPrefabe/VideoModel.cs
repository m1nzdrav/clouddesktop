using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoModel : FolderModel
{
    #region поля, св-ва и т.п.
    
    [SerializeField] TextToName textToName;

    //[SerializeField] private TopPanelSetParametrs topPanelSetParametrs;
    //public TopPanelSetParametrs TopPanelSetParametrs_Inheritor { get => topPanelSetParametrs; set => topPanelSetParametrs = value; }
    //public IconModel Icon { get; set; }

    [SerializeField] private RawImage rawImage;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoController videoController;
    [SerializeField] private Text BuildConsole;

    #endregion
    

    public new void SetFromIcon()
    {
        rawImage.texture = Icon.IconImage_Texture;
        textToName.Icon = Icon;
        TopPanelSetParametrs.SetParametrs(gameObject, Icon.gameObject);
        BuildConsole.text = Icon.SourcePath;
        if (Icon.SourcePath != "")
        {
            /*videoPlayer.url = Icon.SourcePath;
            RenderTexture renderTexture = new RenderTexture(256, 256, 1);
            rawImage.texture = renderTexture;
            videoPlayer.targetTexture = renderTexture;
            videoPlayer.Play();
            BuildConsole.text = "Play";
            //videoController.Play();*/
            
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = Icon.SourcePath;
            BuildConsole.text = videoPlayer.url;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            RenderTexture renderTexture = new RenderTexture(256, 256, 1);
            videoPlayer.targetTexture = renderTexture;
            rawImage.texture = renderTexture;
            videoPlayer.Play();
        }
    }

    public override void SpawnChild(GameObject child)
    {
        throw new System.NotImplementedException();
    }

    public void SwitchOnOff_VideoPlayer()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else
        {
            videoPlayer.Play();
        }
    }
}
