using UnityEngine;

public interface IFolderBone
{
    TopPanelSetParametrs TopPanelSetParametrs { get; set; }
    Transform RotatedArea { get; set; }
    IconModel Icon { get; set; }
    void SetFromIcon();
}