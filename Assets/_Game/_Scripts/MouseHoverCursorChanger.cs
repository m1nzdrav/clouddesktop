using UnityEngine;

public class MouseHoverCursorChanger : MonoBehaviour
{
    [SerializeField]
    private Texture2D hoverCursor;

    private Cursor defaultCursor;

    public void HoverEnter()        
    {
        Cursor.SetCursor(hoverCursor, new Vector2(16, 16), CursorMode.Auto);
    }

    public void HoverExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
