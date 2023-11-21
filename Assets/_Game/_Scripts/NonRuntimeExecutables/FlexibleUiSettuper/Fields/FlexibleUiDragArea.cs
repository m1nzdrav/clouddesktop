
using UnityEngine;

public class FlexibleUiDragArea : MonoBehaviour
{
    [SerializeField] private RectTransform dragArea;

    private static RectTransform s_dragArea;

    public static RectTransform S_DragArea { get => s_dragArea; }

    private void Awake()
    {
        if(dragArea == null)
            s_dragArea = GetComponent<RectTransform>();
    }
}
