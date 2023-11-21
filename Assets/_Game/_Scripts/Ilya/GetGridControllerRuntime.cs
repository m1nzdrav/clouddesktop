using UnityEngine;

public class GetGridControllerRuntime : MonoBehaviour
{
    private Transform _gridControllerRuntime;

    void Awake()
    {
        _gridControllerRuntime = transform.parent.GetChild(transform.parent.childCount - 1);

    }


    /// <summary>
    /// Костыль
    /// </summary>
    public void OpenClose()
    {
        if (_gridControllerRuntime.name == "GridController")
        {
            if(_gridControllerRuntime.gameObject.activeSelf)
            {
                _gridControllerRuntime.gameObject.SetActive(false);
            }
            else
            {
                _gridControllerRuntime.gameObject.SetActive(true);
            }
        }
    }
}
