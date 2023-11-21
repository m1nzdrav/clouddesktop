using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioModel : FolderModel
{
    [SerializeField] private TextToName textToName;
    [SerializeField] private AudioSource audioSource;
    
    public new void SetFromIcon()
    {
        Debug.Log("setting from icon");
        //textToName.Icon = Icon;
        TopPanelSetParametrs.SetParametrs(gameObject, Icon.gameObject);

        Debug.Log(Icon.SourcePath);
        
        if (Icon.SourcePath != "")
        {
            Debug.Log("start sound loading");
            StartCoroutine(GetAudioClip());
        }
    }
    
    IEnumerator GetAudioClip()
    {
        //AudioType.
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(Icon.SourcePath, AudioType.UNKNOWN))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.LogError("error");
            }
            else if(www.isDone)
            {
                Debug.Log("sound loaded");
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
            }
        }
    }
}
