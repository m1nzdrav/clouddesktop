using System;
using UnityEngine;


public class IconCreateAfterDragDrop : MonoBehaviour
{
    [SerializeField]
    private DragDropRegister dragDropRegister;
    [SerializeField] private WorkspaceEntityMember block;
    [SerializeField] private WorkspaceEntityMember whatEntity;
    [SerializeField] private Transform parentEntity;

    public void Start()
    {
        dragDropRegister.RegisterOnDrop(CreateIcon);
    }

    private void CreateIcon(string nameFile, ContentType contentType, byte[] data)
    {
        switch (contentType)
        {
            default:
            {
                string imageStr = Convert.ToBase64String(data);
                Texture2D text = new Texture2D(1, 1);
                text.LoadImage(Convert.FromBase64String(imageStr));

                Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
                
                WorkspaceEntityMember temp = Instantiate(block, parentEntity);
                temp.Set(nameFile, nameFile, whatEntity);
                break;
            }
        }
    }
}