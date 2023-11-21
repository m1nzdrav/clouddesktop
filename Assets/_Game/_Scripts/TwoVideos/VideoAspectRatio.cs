using UnityEngine;
using UnityEngine.UI;

public class VideoAspectRatio : MonoBehaviour
{
    [SerializeField] private RectTransform _rectVideo;
    [SerializeField] private RawImage _imageVideo;
    [SerializeField] private RectTransform _rectObject;
    [SerializeField] private AspectRatioFitter _aspectRatio;

    private const float _div = 3;

    private void Start()
    {
        SetAspectRatio();
    }

    private void SetAspectRatio()
    {
        // _imageVideo.SetNativeSize();

        var rect = _rectVideo.rect;

        var width = rect.width;
        var height = rect.height;

        // _rectObject.sizeDelta = new Vector2(width / _div, height / _div);//todo Выявить константу уменньшения видео
        //
        // _rectVideo.anchorMax = new Vector2(0.5f, 0.5f);
        // _rectVideo.anchorMin = new Vector2(0.5f, 0.5f);
        // _rectVideo.anchoredPosition = Vector2.zero;
        //
        // _rectVideo.anchorMax = Vector2.one;
        // _rectVideo.anchorMin = Vector2.zero;
        // _rectVideo.offsetMin = Vector2.zero;
        // _rectVideo.offsetMax = Vector2.zero;

        _aspectRatio.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
    }
}
