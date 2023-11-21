using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using _Game._Scripts.Panel;
using UnityEngine;
using B83.Win32;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.Video;
using _Game._Scripts;
using _Game._Scripts.JsonScript;

public enum ContentType
{
    Video,
    Image,
    Audio,
    Nothing
}

public class DragDropFiles : MonoBehaviour
{
    //[SerializeField] private GameObject _prefabVideo;

    private string _extension;

    private bool _isVideo;
    private bool _isImage;

    //Texture2D[] textures = new Texture2D[1];
    DropInfo dropInfo = null;

    private ContentType _contentType = ContentType.Nothing;

    [SerializeField] private Transform LoadedObjectsList;
    [SerializeField] private GameObject IconPref;

    [SerializeField] private Text BuildConsole;
    [SerializeField] private VideoPlayer videoPlayer;

    private List<string> _videoFormats = new List<string>
    {
        //video
        ".flv",
        ".f4v",
        ".f4p",
        ".f4a",
        ".f4b",
        ".nsv",
        ".roq",
        ".mxf",
        ".3g2",
        ".3gp",
        ".svi",
        ".m4v",
        ".mpg",
        ".mpeg",
        ".m2v",
        ".mpg",
        //".mp2",
        ".mpeg",
        ".mpe",
        ".mpv",
        ".mp4",
        ".m4p",
        ".m4v",
        ".amv",
        ".asf",
        ".viv",
        ".rmvb",
        ".rm",
        ".yuv",
        ".wmv",
        ".mov",
        ".qt",
        ".MTS",
        ".M2TS",
        ".TS",
        ".avi",
        ".mng",
        ".gifv",
        ".gif",
        ".drc",
        ".ogv",
        ".ogg",
        ".vob",
        ".flv",
        ".mkv",
        ".webm"
    };

    private List<string> _imageFormats = new List<string>
    {
        //image
        ".pdf",
        ".eps",
        ".ai",
        ".svg",
        ".svgz",
        ".jp2",
        ".j2k",
        ".jpf",
        ".jpx",
        ".jpm",
        ".mj2",
        ".ind",
        ".indd",
        ".indt",
        ".heif",
        ".heic",
        ".bmp",
        ".dib",
        ".raw",
        ".arw",
        ".cr2",
        ".nrw",
        ".k25",
        ".psd",
        ".tiff",
        ".tif",
        ".webp",
        ".gif",
        ".png",
        ".jpg",
        ".jpeg",
        ".jpe",
        ".jif",
        ".jfif",
        ".jfi",
    };

    private List<string> _otherFormats = new List<string>
    {
        //ms office
        ".xps",
        ".pub",
        ".one",
        ".ldb",
        ".mde",
        ".mdf",
        ".mdt",
        ".mdn",
        ".mda",
        ".cdb",
        ".mdb",
        ".adp",
        ".ade",
        ".laccdb",
        ".maf",
        ".mat",
        ".mar",
        ".maq",
        ".mam",
        ".accde",
        ".mdw",
        ".accda",
        ".accdt",
        ".accdr",
        ".accdb",
        ".adn",
        ".sldm",
        ".sldx",
        ".ppsm",
        ".ppsx",
        ".ppam",
        ".potm",
        ".potx",
        ".pptm",
        ".pptx",
        ".pps",
        ".pot",
        ".ppt",
        ".xlw",
        ".xll",
        ".xlam",
        ".xla",
        ".xlsb",
        ".xltm",
        ".xltx",
        ".xlsm",
        ".xlsx",
        ".xlm",
        ".xlt",
        ".xls",
        ".docb",
        ".dotm",
        ".dotx",
        ".docm",
        ".docx",
        ".wbk",
        ".dot",
        ".doc",

        //other
        ".exe",
        ".txt"
    };

    private string[] _audioFormats = new string[45]
    {
        //audio
        ".mp2",
        ".cda",
        ".8svx",
        ".webm",
        ".wv",
        ".wma",
        ".wav",
        ".vox",
        ".voc",
        ".tta",
        ".sln",
        ".rf64",
        ".raw",
        ".ra",
        ".rm",
        ".opus",
        ".ogg",
        ".oga",
        ".mogg",
        ".nmf",
        ".msv",
        ".mpc",
        ".mp3",
        ".mmf",
        ".m4p",
        ".m4b",
        ".m4a",
        ".ivs",
        ".iklax",
        ".gsm",
        ".flac",
        ".dvf",
        ".dss",
        ".dct",
        ".awb",
        ".au",
        ".ape",
        ".amr",
        ".alac",
        ".aiff",
        ".act",
        ".aax",
        ".aac",
        ".aa",
        ".3gp"
    };

    class DropInfo
    {
        public string file;
        public Vector2 pos;
    }

    void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;
    }

    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }

    void OnFiles(List<string> aFiles, POINT aPos)
    {
        string file = "";
        var fi = new FileInfo(aFiles[0]);
        var ext = fi.Extension.ToLower();
        _extension = ext;

        for (int i = 0; i < _videoFormats.Count; i++)
        {
            if (ext != _videoFormats[i].ToLower()) continue;

            file = aFiles[0];
            _contentType = ContentType.Video;
            break;
        }

        if (_contentType != ContentType.Video)
        {
            for (int i = 0; i < _imageFormats.Count; i++)
            {
                if (ext != _imageFormats[i].ToLower()) continue;
                
                file = aFiles[0];
                _contentType = ContentType.Image;
                break;
            }
        }

        Debug.Log(_contentType);

        if (_contentType != ContentType.Video && _contentType != ContentType.Image)
        {
            for (int i = 0; i < _audioFormats.Length; i++)
            {
                if (ext == _audioFormats[i].ToLower())
                {
                    Debug.Log("audioType");
                    file = aFiles[0];
                    _contentType = ContentType.Audio;
                    break;
                }
            }
        }

        if (file != "")
        {
            var info = new DropInfo
            {
                file = file,
                pos = new Vector2(aPos.x, aPos.y)
            };
            dropInfo = info;
        }
    }

    void LoadFile(DropInfo aInfo)
    {
        if (aInfo == null)
        {
            return;
        }


        //var path = SaveFile(data);
        //var managerJson = FindObjectOfType<ManagerJson>();
        //var pathForName = aInfo.file;

        GameObject prefab;
        GameObject icon;
        IdNameValue chapter;
        IdNameValue id;

        switch (_contentType)
        {
            case ContentType.Video:
                Video_fileManager videoFileManager = new Video_fileManager(videoPlayer);
                videoFileManager.LoadFileData(aInfo.file);
                break;
            case ContentType.Image:
                Image_fileManager imageFileManager = new Image_fileManager();
                imageFileManager.LoadFileData(aInfo.file);
                break;
            case ContentType.Audio:
                Audio_fileManager audioFileManager = new Audio_fileManager();
                audioFileManager.LoadFileData(aInfo.file);
                break;
        }

        _contentType = ContentType.Nothing;
    }

    /*void LoadImage(int aIndex, DropInfo aInfo)
    {
        if (aInfo == null)
        {
            return;
        }

        var rect = GUILayoutUtility.GetLastRect();
        if (rect.Contains(aInfo.pos))
        {
            var data = File.ReadAllBytes(aInfo.file);

            //data - ��� ����

            var path = SaveFile(data);
            var managerJson = FindObjectOfType<ManagerJson>();
            var pathForName = aInfo.file;

            GameObject prefab;
            GameObject icon;
            IdNameValue chapter;
            IdNameValue id;


            if (_isVideo)
            {
                //Debug.LogError("This is Video");

                chapter = new IdNameValue(StructJson.Where.ToString());
                id = new IdNameValue(SwitcherValues.MYTVideo.ToString());
                id.MYTValue.Add(new IdNameValue(path));
                chapter.MYTValue.Add(id);
                icon = GetComponent<CreateModelPrefab>().Create(null, Prefab.MYTVideo);
                prefab = icon.GetComponent<IconModel>().Folder.gameObject;
            }
            else if (_isImage)
            {
                //Debug.LogError("This is Image");

                chapter = new IdNameValue(StructJson.Where.ToString());
                id = new IdNameValue(SwitcherValues.MYTImage.ToString());
                id.MYTValue.Add(new IdNameValue(path));
                chapter.MYTValue.Add(id);
                icon = GetComponent<CreateModelPrefab>().Create(null, Prefab.MYTImage);
                prefab = icon.GetComponent<IconModel>().Folder.gameObject;
            }
            else
            {
                //Debug.LogError("This is Other");

                icon = GetComponent<CreateModelPrefab>().Create(null, Prefab.MYTFolder);
                prefab = icon.GetComponent<IconModel>().Folder.gameObject;
                return;
            }



            StartCoroutine(Wait(managerJson, chapter, prefab, path, pathForName, icon));
        }
    }*/

    [Button]
    void TestMetgod()
    {
        GameObject prefab;
        GameObject icon;
        IdNameValue chapter;
        IdNameValue id;

        var managerJson = FindObjectOfType<ManagerJson>();
        var path = @"D:\Downloads\Strawberries.jpg";

        _isImage = true;

        chapter = new IdNameValue(StructJson.Where.ToString());
        id = new IdNameValue(SwitcherValues.MYTImage.ToString());
        id.MYTValue.Add(new IdNameValue(path));
        chapter.MYTValue.Add(id);
        icon = GetComponent<CreateModelPrefab>().Create(null, Prefab.MYTImage);
        prefab = icon.GetComponent<IconModel>().Folder.gameObject;

        StartCoroutine(Wait(managerJson, chapter, prefab, path, "123", icon));
    }

    IEnumerator Wait(ManagerJson managerJson, IdNameValue chapter, GameObject prefab, string path, string pathForName,
        GameObject icon)
    {
        yield return new WaitForSeconds(1f);
        //Debug.LogError(JsonUtility.ToJson(chapter));

        managerJson.UpdateNewChapter(chapter, prefab);

        /*Texture2D texture2D = new Texture2D();
        texture2D.LoadImage(path);
        
        testImage.texture = */

        if (_isVideo)
        {
            var videoPlayer = prefab.GetComponent<GetVideoPlayer>().VideoManager;
            var image = videoPlayer.transform.GetChild(0)
                .GetComponent<RawImage>(); //костыль (сделать получение, как видео)
            var rectImage = videoPlayer.transform.GetChild(0).GetComponent<RectTransform>();
            var parentSize = rectImage.rect.size;
            SetVideo(videoPlayer, path);
            SetNativeSize(prefab, parentSize, rectImage, null, image);
            _isVideo = false;
        }
        else if (_isImage)
        {
            BuildConsole.text = "isImageInCor";
            var image = prefab.transform.GetChild(0).GetChild(prefab.transform.GetChild(0).childCount - 2)
                .GetComponent<Image>(); //костыль (сделать получение, как видео)
            var sprite = LoadSprite(path);
            var rectImage = image.GetComponent<RectTransform>();
            var imageSize = rectImage.rect.size;
            image.sprite = sprite;
            SetNativeSize(prefab, imageSize, rectImage, image, null);
            SetIcon(path, managerJson, prefab, icon, sprite);
            _isImage = false;
            BuildConsole.text = "imageInCor_end";
        }

        var filename = GetFilename(pathForName);
        SetFilename(filename, managerJson, prefab);
    }

    void SetVideo(VideoPlayer video, string path)
    {
        video.url = "file://" + path;
    }

    //void SetImage(Image image, string path)
    //{
    //    Texture2D tex;
    //    tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
    //    WWW www = new WWW(path);
    //    www.LoadImageIntoTexture(tex);
    //    image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    //}

    public void SetNativeSize(GameObject prefab, Vector2 parentSize, RectTransform rectImage, Image image,
        RawImage rawImage)
    {
        AspectRatioFitter imageAspectRatioFitter;

        if (image != null)
        {
            image.SetNativeSize();
            imageAspectRatioFitter = image.GetComponent<AspectRatioFitter>();
        }
        else if (rawImage != null)
        {
            rawImage.SetNativeSize();
            imageAspectRatioFitter = rawImage.GetComponent<AspectRatioFitter>();
        }
        else
        {
            //Debug.Log("Внутренний Image передан неверно");
            return;
        }

        var imageSize = rectImage.rect.size;
        var maxScale = Mathf.Max(imageSize.x, imageSize.y);
        var aspectRatioFitter = prefab.GetComponent<AspectRatioFitter>();
        //var aspectRatioFitterRotated = prefab.transform.GetChild(0).GetComponent<AspectRatioFitter>();
        float div;

        if (maxScale == imageSize.x)
        {
            div = maxScale / parentSize.x;
        }
        else
        {
            div = maxScale / parentSize.y;
        }

        var maxSize = new Vector2(imageSize.x / div, imageSize.y / div);
        var nativeSize = new Vector2(maxSize.x, maxSize.y);

        rectImage.sizeDelta = nativeSize;
        RectTransform rectBorder;

        if (_isVideo)
        {
            rectImage.anchorMin = new Vector2(0f, 0f);
            rectImage.anchorMax = new Vector2(1f, 1f);

            rectBorder = null;
        }
        else
        {
            var topPanel = prefab.transform.GetChild(0).GetChild(prefab.transform.GetChild(0).childCount - 1);
            var topPanelRect = topPanel.GetComponent<RectTransform>();
            topPanelRect.anchorMin = new Vector2(-0.1f, 0f);
            topPanelRect.anchorMax = new Vector2(1.1f, 1f);

            rectImage.anchorMin = new Vector2(0.01f, 0f);
            rectImage.anchorMax = new Vector2(0.99f, 1f);

            rectBorder = image.transform.parent.GetChild(0).GetComponent<RectTransform>();
            rectBorder.anchorMin = new Vector2(-0.1f, 0f);
            rectBorder.anchorMax = new Vector2(1.1f, 1f);
        }

        rectImage.offsetMin = new Vector2(0, rectImage.offsetMin.y);
        rectImage.offsetMax = new Vector2(0, rectImage.offsetMax.y);
        rectImage.localPosition = Vector3.zero;

        var ratio = imageSize.x / imageSize.y;
        //imageAspectRatioFitter.aspectRatio = ratio;
        aspectRatioFitter.aspectRatio = ratio;
        //aspectRatioFitterRotated.aspectRatio = ratio;

        //imageAspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        //aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
        //aspectRatioFitterRotated.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;

        //if (rectBorder != null)
        //{
        //    var wBorder = rectBorder.rect.size.x;
        //    var hImage = rectImage.rect.size.y;
        //    rectImage.sizeDelta = new Vector2(wBorder, hImage);

        //    aspectRatioFitterRotated.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
        //}

        var sizeBorder = rectBorder.rect.size;

        Debug.Log("sizeBorder" + sizeBorder);

        image.gameObject.SetActive(true);
    }

    //public void SetForFullParam()
    //{
    //    if (rectBorder != null)
    //    {
    //        var wBorder = rectBorder.rect.size.x;
    //        var hImage = rectImage.rect.size.y;
    //        rectImage.sizeDelta = new Vector2(wBorder, hImage);

    //        aspectRatioFitterRotated.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
    //    }
    //}

    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            return sprite;
        }

        return null;
    }

    public void SetIcon(string path, ManagerJson managerJson, GameObject prefab, GameObject icon, Sprite sprite)
    {
        var imageIcon = icon.GetComponent<Image>();
        imageIcon.sprite = sprite;
        imageIcon.color = Color.white;

        var chapter = new IdNameValue(StructJson.What.ToString());
        var id = new IdNameValue(SwitcherValues.MYTCover.ToString());
        id.MYTValue.Add(new IdNameValue(path));
        chapter.MYTValue.Add(id);

        managerJson.UpdateNewChapter(chapter, prefab);
    }

    private void SetFilename(string filename, ManagerJson managerJson, GameObject prefab)
    {
        var chapter = new IdNameValue(StructJson.Data.ToString());
        var defaultName = new IdNameValue(SwitcherValues.MYTDefaultName.ToString());
        defaultName.MYTValue.Add(new IdNameValue(filename));
        chapter.MYTValue.Add(defaultName);

        managerJson.UpdateNewChapter(chapter, prefab);
    }


    private string GetFilename(string path)
    {
        var startIndex = path.LastIndexOf(@"\") + 1;

        if (startIndex == 0)
        {
            return "ErrName";
        }

        var endIndex = path.Length;
        string name = null;

        for (int i = startIndex; i < endIndex; i++)
        {
            name += path[i];
        }

        return name;
    }

    private string SaveFile(byte[] texture)
    {
        byte[] bytes = texture;
        var dirPath = Application.dataPath + "/StreamingAssets/SavedFiles/";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllBytes(dirPath + texture.GetHashCode() + _extension, bytes);

        return dirPath + texture.GetHashCode() + _extension;
    }

    private void OnGUI()
    {
        DropInfo tmp = null;
        if (Event.current.type == EventType.Repaint && dropInfo != null)
        {
            tmp = dropInfo;
            dropInfo = null;
            BuildConsole.text = "imageLoading";
        }

        //LoadImage(0, tmp);

        LoadFile(tmp);
    }
}