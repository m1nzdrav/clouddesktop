
using _Game._Scripts.Events.StartEnd;
using UnityEngine;

public class CursorMoveChecker : FirstSecondEvent, ISingleton
{ 

    public string NameComponent
    {
        get => name;
    }
    public void CheckRegister()
    {
        InstantiateEvent();
    }
    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse X") == 0 && (Input.GetAxis("Mouse Y") == 0))
        {
            SecondEvent.Raise();
        }
        else
        {
            FirstEvent.Raise();
        }
    }
}