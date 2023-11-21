using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    public Image image;
    public bool active;

    public void Set(Image image, bool active)
    {
        this.image = image;
        this.active = active;
    }
}