using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageModel : FolderModel //Parent, ISetter, IFolderBone
{
    #region поля, св-ва и т.п.

    [SerializeField] TextToName textToName;

    //[SerializeField] private TopPanelSetParametrs _topPanelSetParametrs;
    //public TopPanelSetParametrs TopPanelSetParametrs { get => _topPanelSetParametrs; set => _topPanelSetParametrs = value; }
    //public IconModel Icon { get; set; }

    [SerializeField] private RawImage rawImage;
    [SerializeField] private Image _image;
    [SerializeField] private AspectRatioFitter aspectRatioFitter;

    [SerializeField] private RectTransform rectToRebuild;

    private RectTransform _thisRectTr;

    #endregion

    private void Start()
    {
        _thisRectTr = this.GetComponent<RectTransform>();
    }

    public new void SetFromIcon()
    {
        Debug.Log(Icon.IconImage_Texture.width + ", " + Icon.IconImage_Texture.height);

        float x = Icon.IconImage_Texture.width;
        float y = Icon.IconImage_Texture.height;
        Debug.Log(x / y);

        Debug.LogError(Icon.IconImage_Texture.width / Icon.IconImage_Texture.height);


        // aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.None;
        aspectRatioFitter.aspectRatio = x / y;
        // this.GetComponent<RectTransform>().sizeDelta = new Vector2
        //     (x, y + 75);


        //aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        
        // rawImage.texture = Icon.IconImage_Texture;
        _image.sprite = Sprite.Create((Texture2D) Icon.IconImage_Texture,
            new Rect(0.0f, 0.0f, Icon.IconImage_Texture.width, Icon.IconImage_Texture.height), new Vector2(0.5f, 0.5f),
            100.0f);

        // /*GameObject parent = rawImage.gameObject.transform.parent.gameObject;
        // parent.GetComponent<AspectRatioFitter>().aspectRatio = x / y;
        // RectTransform parentRectTr = parent.GetComponent<RectTransform>();
        // rawImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2
        //     (parentRectTr.sizeDelta.x, parentRectTr.sizeDelta.y);*/
        _image.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = x / y;
        //aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;

        textToName.Icon = Icon;
        TopPanelSetParametrs.SetParametrs(gameObject, Icon.gameObject);
    }

    public override void SpawnChild(GameObject child)
    {
        throw new System.NotImplementedException();
    }

    float FindY(float x)
    {
        //x/y=x1/y1
        float y = (Icon.IconImage_Texture.height * Icon.IconImage_Texture.width / x);
        return y;
    }

    public void OnResize()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_thisRectTr);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectToRebuild);
    }
}