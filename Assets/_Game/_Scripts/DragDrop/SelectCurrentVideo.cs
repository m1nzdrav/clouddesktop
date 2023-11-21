using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using UnityEngine.Video;

public class SelectCurrentVideo : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayerComponent;
    [SerializeField] private GameObject _texture;

    public void SetVideo(string path)
    {
        SetTexture();
        _videoPlayerComponent.url = "file://" + path;
    }

    private void SetTexture()
    {
        var texture = new RenderTexture(1920, 1080, 1, GraphicsFormat.B8G8R8A8_UNorm);
        _videoPlayerComponent.targetTexture = texture;
        _texture.GetComponent<RawImage>().texture = texture;
    }
}