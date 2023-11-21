using UnityEngine;

public class PopupBalance : MonoBehaviour
{
    [SerializeField] private GameObject information;
    [SerializeField] private Transform parent;
     
    public void Set(GameObject information)
    {
        this.information = Instantiate(information,parent);
    }


}
