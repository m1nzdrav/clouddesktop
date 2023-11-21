using UnityEngine;

public interface IHeadSelected : ISingleton
{
    SelectedWindow CurrentSelectedWindow { get; set; }
    Transform CurrentSelectedWindowTransform { get; }
     void CheckSelectedObj(SelectedWindow newWindow);
     void SetCurrentSelectedWindow(SelectedWindow newWindow);
}