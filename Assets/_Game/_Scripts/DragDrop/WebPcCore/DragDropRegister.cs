using System;
using UnityEngine;
using UnityEngine.UI;


public class DragDropRegister : MonoBehaviour
{
    private FileChecker fileChecker;
    [SerializeField] private DragDropController dragDropController;
    [SerializeField] private Image image;

    private Action<string, ContentType, byte[]> onDropData;

    private void Awake()
    {
        fileChecker = new FileChecker();
        if (!dragDropController.Register())
        {
            Debug.Log("Failed to register drag & drop.");
        }

        // dragDropController.OnDropped += OnDrop;
        // dragDropController.OnDragEnter += OnDragEnter;
        // dragDropController.OnDragMove += OnDragMove;
        // dragDropController.OnDragExit += OnDragExit;
        dragDropController.OnDroppedData += OnDropData;
    }

    private void OnDestroy()
    {
        if (dragDropController == null)
        {
            return;
        }

        dragDropController.OnDropped = null;
        dragDropController.OnDragEnter = null;
        dragDropController.OnDragMove = null;
        dragDropController.OnDragExit = null;
        dragDropController.OnDroppedData = null;
        dragDropController = null;
    }

    private void OnDragEnter(string fileName, int x, int y)
    {
        Debug.Log("OnDragEnter - File name: " + fileName);
        Debug.Log("X: " + x + ", Y: " + y);
    }

    private void OnDrop(string fileName, int x, int y)
    {
        Debug.Log("OnDrop - File name: " + fileName);
        Debug.Log("X: " + x + ", Y: " + y);
    }

    private void OnDragMove(string fileName, int x, int y)
    {
        Debug.Log("OnDragMove - File name: " + fileName);
        Debug.Log("X: " + x + ", Y: " + y);
    }

    private void OnDragExit(string fileName, int x, int y)
    {
        Debug.Log("OnDragExit - File name: " + fileName);
        Debug.Log("X: " + x + ", Y: " + y);
    }

    private void OnDropData(string fileName, int x, int y, byte[] data)
    {
        Debug.Log("OnDropData - File name: " + fileName);
        Debug.Log("X: " + x + ", Y: " + y);


        onDropData.Invoke(fileName, fileChecker.CheckFile(fileName), data);

        // string imageStr = Convert.ToBase64String(data);
        // Texture2D text = new Texture2D(1, 1);
        // text.LoadImage(Convert.FromBase64String(imageStr));
        //
        // Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
        // image.sprite = sprite;
        
        // if (Path.GetExtension(fileName).Equals(".txt", StringComparison.OrdinalIgnoreCase))
        // {
        // string textData = Encoding.UTF8.GetString(data);
        // FileContentsLabel.text = textData;
        // }
    }

    public void RegisterOnDrop(Action<string, ContentType, byte[]> action)
    {
        onDropData += action;
    }

    public void UnRegisterOnDrop(Action<string, ContentType, byte[]> action)
    {
        onDropData -= action;
    }
}