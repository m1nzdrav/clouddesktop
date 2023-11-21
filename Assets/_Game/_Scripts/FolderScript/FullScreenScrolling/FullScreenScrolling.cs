using UnityEngine;

public class FullScreenScrolling : MonoBehaviour
{
    private Vector2 _currentOffset;
    [SerializeField] private bool isFullScreen = true;

    public void ChangeOffset()
    {
        if (isFullScreen)

            SmallScreenScroll();

        else
            FullScreenScroll();
    }

    private void FullScreenScroll()
    {
        isFullScreen = true;
        _currentOffset = GetComponent<RectTransform>().offsetMax;
        Debug.LogError(_currentOffset);
        GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    private void SmallScreenScroll()
    {
        isFullScreen = false;
        Debug.LogError(_currentOffset);
        GetComponent<RectTransform>().offsetMax = _currentOffset;
    }
}