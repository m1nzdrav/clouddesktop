using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(RawImage))]
public class RawWebImageLoader : MonoBehaviour
{
    public FilterMode filterMode;
    public TextureFormat textureFormat = TextureFormat.BGRA32;
    public bool mipChain;


    public string Url {
        set {
            StopAllCoroutines();
            StartCoroutine(LoadAndApplyTexture(value));
        }
    }


    private IEnumerator LoadAndApplyTexture(string url) {
        using(UnityWebRequest www = UnityWebRequest.Get(url)) {
            yield return www.SendWebRequest();
            if(!www.isNetworkError && !www.isHttpError) {
                Texture2D texture = new Texture2D(0, 0, textureFormat, mipChain);
                texture.filterMode = filterMode;

                texture.LoadImage(www.downloadHandler.data);
                GetComponent<RawImage>().texture = texture;
            }
        }
    }
}
