using UnityEngine;
using UnityEngine.UI;

public class SetImageSize : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _rectImage;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private RectTransform _rectParent;
    [SerializeField] private AspectRatioFitter _aspectRatioFitterRotated;

    public void SetNativeSize()
    {
        var parentSize = _rectParent.rect.size;
        _image.sprite = _sprite;
        _image.SetNativeSize();

        var imageSize = _rectImage.rect.size;
        var maxScale = Mathf.Max(imageSize.x, imageSize.y);

        if (maxScale == imageSize.x)
        {
            var div = maxScale / parentSize.x;
            var nativeSize = new Vector2(imageSize.x / div, imageSize.y / div);

            _rectImage.sizeDelta = nativeSize;
        }
        else
        {
            var div = maxScale / parentSize.y;
            var maxSize = new Vector2(imageSize.x / div, imageSize.y / div);
            var nativeSize = new Vector2(maxSize.x - (maxSize.x * 10 / 100), maxSize.x - (maxSize.y * 10 / 100));

            _rectImage.sizeDelta = nativeSize;
        }


        _rectImage.anchorMin = new Vector2(0.1f, 0f);
        _rectImage.anchorMax = new Vector2(0.9f, 1f);

        _rectImage.offsetMin = new Vector2(0, _rectImage.offsetMin.y);
        _rectImage.offsetMax = new Vector2(0, _rectImage.offsetMax.y);
        _rectImage.localPosition = Vector3.zero;

        var aspectRatioFitter = _image.GetComponent<AspectRatioFitter>();
        var ratio = imageSize.x / imageSize.y;
//        aspectRatioFitter.aspectRatio = ratio;
//        _aspectRatioFitterRotated.aspectRatio = ratio;
//        aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        _aspectRatioFitterRotated.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;

    }
}
