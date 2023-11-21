using UnityEngine;

public class BalanceMain : MonoBehaviour
{
    [SerializeField] private PopupBalance prefabPopup;
    [SerializeField] private Transform parentPopup;

    public void SetupPopup(GameObject information)
    {
        PopupBalance newPrefabPopup = Instantiate(prefabPopup, parentPopup);
        newPrefabPopup.Set(information);
    }
}